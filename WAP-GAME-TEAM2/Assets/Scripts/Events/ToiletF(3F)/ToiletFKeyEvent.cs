using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletFKeyEvent : ItemEvent
{
    protected override IEnumerator ItemEventCo()
    {
        theEvent.isEventIng = false;
        PlayerController.instance.IsPause = false;

        gameObject.SetActive(false);

        yield break;
    }
}
