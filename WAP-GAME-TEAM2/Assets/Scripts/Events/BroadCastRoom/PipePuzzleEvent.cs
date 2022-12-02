using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipePuzzleEvent : DefaultEvent
{
    public GameObject pipePuzzle;

    protected override void SwitchCheck()
    {
        if (EventManager.instance.switches[(int)SwitchType.StDoorOpened])
            gameObject.SetActive(false);
    }

    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        theEvent.isEventIng = true;
        pipePuzzle.SetActive(true);
    }
}