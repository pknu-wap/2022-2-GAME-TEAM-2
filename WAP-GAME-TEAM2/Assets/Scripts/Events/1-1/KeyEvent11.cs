using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent11 : ItemEvent
{
    public GameObject go;

    protected override IEnumerator ItemEventCo()
    {
        go.SetActive(true);
        theEvent.isEventIng = false;
        PlayerController.instance.IsPause = false;
        yield break;
    }
}
