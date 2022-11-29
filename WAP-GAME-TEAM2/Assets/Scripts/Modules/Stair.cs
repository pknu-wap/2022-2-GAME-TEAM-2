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
        if (!isCollision || DialogueManager.instance.talking) return;
        
        float dirValue = PlayerController.instance.GetPlayerDir(dir);
        if (dirValue != val) return;
        
        if (!Input.GetKeyDown(KeyCode.Z)) return;
        StartCoroutine(MoveCo());
      
    }
    
    protected virtual IEnumerator MoveCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        PlayerController.instance.IsPause = true;
        AudioManager.instance.PlaySFX(moveSound);
        FadeManager.instance.FadeOut();
        yield return new WaitForSeconds(1.5f);
        PlayerController.instance.transform.position = toMove;
        PlayerController.instance.isSceneChange = true;
        PlayerController.instance.SetPlayerDirAnim(dir, -val);
        AudioManager.instance.StopBGM();
        AudioManager.instance.PlayBGM(bgmToChange);
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
