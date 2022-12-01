using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalOfficeDoor : Door
{
    public GameObject[] ClosedDoor;
    public GameObject crack;

    protected override void Start()
    {
        DoorOpen();

        if (EventManager.instance.switches[(int)SwitchType.TeacherOfficeFileEvent])
        {
            crack.SetActive(true);
            lockDial += " ���� ���� ���ִ�.";
        }

        if (EventManager.instance.switches[(int)SwitchType.ChaseEvent1F])
        {
            crack.SetActive(false);
        }
    }

    public void DoorOpen()
    {
        doorOpened = EventManager.instance.switches[(int)SwitchType.PoDoorOpened];

        foreach (var t in ClosedDoor)
            t.SetActive(false);
        crack.SetActive(false);

        if (!doorOpened)
        {
            foreach (var t in ClosedDoor)
                t.SetActive(true);
        }
    }
}
