using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stair : MonoBehaviour
{
    public string moveSound;
    
    public Vector2 toMove;

    public string dir;
    public float val;

    public string bgmToChange;
    public string sceneName;

    private bool isCollision;
    
    protected virtual void Update()
    {
        if (!isCollision || DialogueManager.instance.talking || Menu.instance.menuActivated
            || Menu.instance.otherActivated) return;
        
        float dirValue = PlayerController.instance.GetPlayerDir(dir);
        if (dirValue != val) return;
        
        if (!Input.GetKeyDown(KeyCode.Z)) return;
        StartCoroutine(MoveCo());
      
    }
    
    protected virtual IEnumerator MoveCo()
    {
        if (SpawnManager.instance.chase)
            SpawnManager.instance.spawnTarget.chase = false;
        
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        PlayerController.instance.IsPause = true;
        AudioManager.instance.PlaySFX(moveSound);
        FadeManager.instance.FadeOut();
        yield return new WaitForSeconds(1.5f);
        PlayerController.instance.transform.position = toMove;
        PlayerController.instance.isSceneChange = true;
        PlayerController.instance.SetPlayerDirAnim(dir, -val);
        if (SpawnManager.instance.chase)
        {
            AudioManager.instance.PlayBGM(bgmToChange);
            AudioManager.instance.StopBGM();
        }
        else 
        {
            AudioManager.instance.StopBGM();
            AudioManager.instance.PlayBGM(bgmToChange);
        }
        
        

        SpawnManager.instance.spawnPoint = toMove;
        SpawnManager.instance.sceneName = sceneName;

        SceneManager.LoadScene(sceneName);
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        isCollision = true;
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        isCollision = false;
    }
}
