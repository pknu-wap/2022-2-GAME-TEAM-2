using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BroadCastDoor : Door
{
    public GameObject[] OpenedDoor;
    public GameObject[] ClosedDoor;

    public GameObject playerLight;
    protected override void Start()
    {
        DoorOpen();
    }
    
    public void DoorOpen()                                                                                                                                                                                                                             
    {
        doorOpened = EventManager.instance.switches[(int)SwitchType.StDoorOpened];
        foreach (var t in OpenedDoor)
            t.SetActive(false);
        foreach (var t in ClosedDoor)
            t.SetActive(false);
        // 문 스프라이트 변경
        if (doorOpened)
        {
            foreach (var t in OpenedDoor)
                t.SetActive(true);
        }
        else
        {
            playerLight = PlayerController.instance.flashLight.gameObject;
            foreach (var t in ClosedDoor)
                t.SetActive(true);
        }
    }
    
    protected override IEnumerator MoveCo()
    {
        PlayerController.instance.IsPause = true;
        AudioManager.instance.PlaySFX(moveSound);
        FadeManager.instance.FadeOut();
        yield return new WaitForSeconds(1.5f);
        PlayerController.instance.transform.position = toMove;
        PlayerController.instance.isSceneChange = true;

        if (SpawnManager.instance.chase == true)
            SpawnManager.instance.spawnTarget.chase = false;
        SpawnManager.instance.spawnPoint = toMove;
        SpawnManager.instance.sceneName = sceneName;

        if (!EventManager.instance.switches[(int)SwitchType.StFirstEntry])
        {
            playerLight.SetActive(false);
            EventManager.instance.switches[(int)SwitchType.StFirstEntry] = true;
        }
            ;
        SceneManager.LoadScene(sceneName);
    }
    
 

}
