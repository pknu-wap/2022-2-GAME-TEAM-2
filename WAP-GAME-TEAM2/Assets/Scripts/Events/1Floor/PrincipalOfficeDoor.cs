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
            lockDial += " 문에 금이 가있다.";
        }

        if (EventManager.instance.switches[(int)SwitchType.ChaseEvent1F])
        {
            crack.SetActive(false);
        }
    }

    public void DoorOpen()
    {
        doorOpened = EventManager.instance.switches[(int)SwitchType.PoDoorOpend];

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
