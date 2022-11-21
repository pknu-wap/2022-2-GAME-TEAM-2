using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipePuzzleEvent : DefaultEvent
{
    public GameObject pipePuzzle;
    private DialogueManager theDial;

    protected override void SwitchCheck()
    {
        if (EventManager.instance.switches[(int)SwitchType.BcDoorOpened])
            gameObject.SetActive(false);
    }

    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        theEvent.isEventIng = true;
        pipePuzzle.SetActive(true);
        yield break;
    }
}