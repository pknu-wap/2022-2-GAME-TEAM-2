using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerRoomKeyEvent : ItemEvent
{
    protected override IEnumerator ItemEventCo()
    {
        theEvent.isEventIng = false;
        PlayerController.instance.IsPause = false;

        gameObject.SetActive(false);

        yield break;
    }
}
