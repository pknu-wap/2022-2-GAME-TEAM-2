using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BroadCastDoor : Door
{
    public GameObject[] OpenedDoor;
    public GameObject[] ClosedDoor;
    void Start()
    {
        DoorOpen();
    }
    
    public void DoorOpen()
    {
        doorOpened = EventManager.instance.switches[(int)SwitchType.BCDoorOpened];
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
            foreach (var t in ClosedDoor)
                t.SetActive(true);
        }
    }
    
 

}
