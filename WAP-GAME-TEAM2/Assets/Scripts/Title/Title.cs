using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private AudioManager theAudio;
    private FadeManager theFade;

    [SerializeField] private Animator newGameAnim;
    void Start()
    {
        theFade = FadeManager.instance;
        theAudio = AudioManager.instance;
    }

    public void NewGame()
    {
        StartCoroutine(NewGameCoroutine());
    }

    public void LoadGame()
    {
        SLManager.instance.Load();
        StartCoroutine(LoadCoroutine());
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif
    }
    
    public void OnButton()
    {
        AudioManager.instance.PlaySFX("Cursor");
    }

    IEnumerator NewGameCoroutine()
    {
        newGameAnim.SetTrigger("Start");
        theAudio.PlaySFX("Cursor");
        theFade.FadeOut(0.05f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("3-1");
    }
    IEnumerator LoadCoroutine()
    {
        theAudio.PlaySFX("Cursor");
        PlayerController.instance.isSceneChange = true;
        theFade.FadeOut();
        yield return new WaitForSeconds(1f);
        AudioManager.instance.PlayBGM(AudioManager.instance.nowPlayBGM);
        SceneManager.LoadScene(SLManager.instance.sceneName);
    }
}
