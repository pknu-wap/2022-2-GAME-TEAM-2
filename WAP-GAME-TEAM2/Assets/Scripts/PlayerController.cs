using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MovingObject
{
    public static PlayerController instance;

    public bool isSceneChange;
    public Light2D flashLight;

    private float _fApplyRunSpeed;
    private bool _bRunFlag;
    public bool IsPause { get; set; }

    [SerializeField] private Animator _baloonAnim;
    
    private AudioManager theAudio;
    [SerializeField] private string stepSound;
    
#region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion Singleton

    void Start()
    {
        theAudio = AudioManager.instance;
    }

    private void FixedUpdate()
    {
        if (IsPause || !canMove) return;
        
        if ((Input.GetAxisRaw("Horizontal") != 0|| Input.GetAxisRaw("Vertical") != 0))
        {
            canMove = false;
            StartCoroutine(PlayerMoveCoroutine());
        }
    }

    IEnumerator PlayerMoveCoroutine()
    {
        while (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _fApplyRunSpeed = speed;
                _bRunFlag = true;
            }
            else
            {
                _fApplyRunSpeed = 0f;
                _bRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (vector.x != 0)
                vector.y = 0;
            
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            bool checkCollisionFlag = CheckCollision();
            if (checkCollisionFlag)
                break;
            
            animator.SetBool("Walking", true);
            theAudio.PlaySFX(stepSound);
            
            while (currentWalkCount < walkCount)
            {
                transform.Translate(new Vector2(
                    vector.x * (speed + _fApplyRunSpeed), vector.y * (speed + _fApplyRunSpeed)));

                if (_bRunFlag)
                    ++currentWalkCount;
                ++currentWalkCount;
                yield return new WaitForSeconds(0.01f);
            }

            animator.SetBool("Walking", false);
            currentWalkCount = 0;
        }

        canMove = true;
    }
    public void SetBalloonAnim()
    {
        _baloonAnim.SetTrigger("Balloon");
    }

    public void SetPlayerDirAnim(string vector, float val)
    {
        if (vector == "DirX")
            animator.SetFloat("DirY", 0f);
        else
            animator.SetFloat("DirX", 0f);
        animator.SetFloat(vector, val);
    }
    
    public float GetPlayerDir(string _dir)
    {
        float f_vector = animator.GetFloat(_dir);
        return f_vector;
    }
}
