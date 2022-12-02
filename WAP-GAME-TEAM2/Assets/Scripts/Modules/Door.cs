using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    public string lockSound;
    public string moveSound;
    
    public Vector2 toMove;

    [TextArea(1, 2)] 
    public string lockDial;
    public string openDial;

    public string keyItemName;

    public string dir;
    public float val;

    public string sceneName;

    public SwitchType doorSwitch;
    public bool doorOpened;
    protected bool isInteracting;
    protected bool isCollision;

    protected virtual void Start()
    {
        if (EventManager.instance.switches[(int)doorSwitch])
            doorOpened = true;
        isCollision = false;
    }
    protected virtual void Update()
    {
        if (!isCollision || DialogueManager.instance.talking || Menu.instance.menuActivated 
            || Menu.instance.otherActivated || PlayerController.instance.isSceneChange) return;
        
        float dirValue = PlayerController.instance.GetPlayerDir(dir);
        if (dirValue != val) return;
        
        if (isInteracting && !DialogueManager.instance.talking) // 대사 끝날때 z를 누른 프레임과 동일한 update 프레임에 z가 눌리는 일 방지.
        {
            isInteracting = false;
            return;
        }
        
        if (!Input.GetKeyDown(KeyCode.Z)) return;
        if (doorOpened)
        {
            StartCoroutine(MoveCo());
        }
        else
        {
            // 키 체크
            if (InventoryManager.instance.SearchItem(keyItemName))
            {
                AudioManager.instance.PlaySFX(lockSound);
                EventManager.instance.switches[(int)doorSwitch] = true;
                DialogueManager.instance.ShowText(openDial);
                InventoryManager.instance.DeleteItem(keyItemName);
                StartCoroutine(MoveCo());
            }
            // 키가 없으면 잠겨있다.
            else
            {
                AudioManager.instance.PlaySFX(lockSound);
                DialogueManager.instance.ShowText(lockDial);
            }
        }
        isInteracting = true;
    }
    
    protected virtual IEnumerator MoveCo()
    {
        if (SpawnManager.instance.chase)
        {
            SpawnManager.instance.spawnTarget.chase = false;
        }

        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        PlayerController.instance.IsPause = true;
        AudioManager.instance.PlaySFX(moveSound);
        FadeManager.instance.FadeOut();
        PlayerController.instance.isSceneChange = true;
        yield return new WaitForSeconds(1.5f);
        PlayerController.instance.transform.position = toMove;

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
