using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class Title : MonoBehaviour
{
    private AudioManager theAudio;
    private FadeManager theFade;

    [SerializeField] private Animator newGameAnim;
    void Start()
    {
        theFade = FadeManager.instance;
        theAudio = AudioManager.instance;

        PlayerController.instance.transform.position = new Vector2(-10f, 1.5f);
        PlayerController.instance.flashLight.intensity = 1f;
        EventManager.instance.SetEvent(false);

        Screen.SetResolution(Screen.width, (Screen.width * 4) / 3, true);
    }

    public void NewGame()
    {
        newGameInit();
        StartCoroutine(NewGameCoroutine());
    }

    public void LoadGame()
    {
        string path = Application.dataPath + "/Save.json";
        if (File.Exists(path))
        {
            LoadGameInit();
            AudioManager.instance.StopBGM();
            SLManager.instance.Load();
            StartCoroutine(LoadCoroutine());
        }
        else
        {
            AudioManager.instance.PlaySFX("Wrong");
        }
        
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
        theAudio.StopBGM();
        PlayerController.instance.flashLight.intensity = 0f;
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

    private void newGameInit()
    {
        EventManager.instance.ClearLibraryLists();
        EventManager.instance.ClearSwitches();
        InventoryManager.instance.ClearItem();
        PlayerController.instance.flashLight.intensity = 0f;
    } 
    private void LoadGameInit()
    {
        EventManager.instance.ClearLibraryLists();
        EventManager.instance.ClearSwitches();
        InventoryManager.instance.ClearItem();
        PlayerController.instance.flashLight.intensity = 1f;
    }
}
