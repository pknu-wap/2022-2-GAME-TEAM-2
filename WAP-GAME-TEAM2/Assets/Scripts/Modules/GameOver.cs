using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;

    private string sceneName = "Title";
    private bool isGameover;

    [SerializeField] private Animator gameoverAnim;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }


    private void Update()
    {
        if (!isGameover)
            return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.instance.PlayBGM("Title");
            
            SceneManager.LoadScene(sceneName);
        }
    }
    
    public void Gameover()
    {
        PlayerController.instance.flashLight.intensity = 0f;
        EventManager.instance.SetEvent(true);

        SpawnManager.instance.chase = false;

        AudioManager.instance.StopChaseBGM();
        AudioManager.instance.StopBGM();

        gameoverAnim.SetBool("gameover", true);
        isGameover = true;
    }
}
