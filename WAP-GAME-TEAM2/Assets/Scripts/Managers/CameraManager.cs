using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private GameObject target;
    [SerializeField] private float fFollowSpeed;
    public bool isFollow;

    private Vector3 v_targetPos;

    [SerializeField] private BoxCollider2D _bound;
    private Vector3 v_maxBound;
    private Vector3 v_minBound;

    private float f_halfHeight;
    private float f_halfWidth;

    private Camera theCamera;

    [SerializeField][Range(0.01f, 0.1f)] private float shakeRange = 0.05f;
    [SerializeField][Range(0.1f, 1f)] private float duration = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        theCamera = GetComponent<Camera>();
        v_minBound = _bound.bounds.min;
        v_maxBound = _bound.bounds.max;
        f_halfHeight = theCamera.orthographicSize;
        f_halfWidth = f_halfHeight * Screen.width / Screen.height;
        target = PlayerController.instance.gameObject;
    }
    
    void Update()
    {
        if (!isFollow) return;
        
        Vector2 targetPosition = target.transform.position;
        v_targetPos.Set(targetPosition.x, targetPosition.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, v_targetPos, fFollowSpeed * Time.deltaTime);

        float ClampedX = Mathf.Clamp(transform.position.x, v_minBound.x + f_halfWidth, v_maxBound.x - f_halfWidth);
        float ClampedY = Mathf.Clamp(transform.position.y, v_minBound.y + f_halfHeight, v_maxBound.y - f_halfHeight);
        transform.position = new Vector3(ClampedX, ClampedY, transform.position.z);

    }

    public void SetBound(BoxCollider2D _newBound)
    {
        _bound = _newBound;
        v_minBound = _bound.bounds.min;
        v_maxBound = _bound.bounds.max;
    }

    public void Shake()
    {
        isFollow = false;

        StartCoroutine(Shake(0.5f, 0.5f));
    }

    public IEnumerator Shake(float _amount, float _duration)
    {
        Vector3 originPos = transform.position;

        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)UnityEngine.Random.insideUnitCircle * _amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originPos;
        isFollow = true;
    }
}
