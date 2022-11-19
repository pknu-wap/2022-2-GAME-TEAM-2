using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    public string lockSound;
    public string moveSound;
    
    public Vector2 toMove;
    
    public string lockDial = "열리지 않는다.";

    public string dir;
    public float val;

    public string sceneName;

    protected bool doorOpened;
    protected bool flag;
    protected bool isStartDial;
    
   
    
    protected void Update()
    {
        float dirValue = PlayerController.instance.GetPlayerDir(dir);
        if (dirValue != val) return;
        
        if (!flag || DialogueManager.instance.talking) return;
        if (isStartDial && !DialogueManager.instance.talking)
        {
            isStartDial = false;
            return;
        }
        
        if (!Input.GetKeyDown(KeyCode.Z)) return;
        if (doorOpened)
        {
            StartCoroutine(MoveCo());
        }
        else
        {
            AudioManager.instance.PlaySFX(lockSound);
            DialogueManager.instance.ShowText(lockDial);
            isStartDial = true;
        }
    }
    
    protected IEnumerator MoveCo()
    {
        PlayerController.instance.IsPause = true;
        AudioManager.instance.PlaySFX(moveSound);
        FadeManager.instance.FadeOut();
        yield return new WaitForSeconds(1.5f);
        PlayerController.instance.transform.position = toMove;
        PlayerController.instance.isSceneChange = true;
        SceneManager.LoadScene(sceneName);
    }

    
    protected void OnTriggerEnter2D(Collider2D col)
    {
        flag = true;
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        flag = false;
    }

}
