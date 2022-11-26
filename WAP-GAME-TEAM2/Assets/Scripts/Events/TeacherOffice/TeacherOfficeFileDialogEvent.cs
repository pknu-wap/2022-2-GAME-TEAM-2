using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherOfficeFileDialogEvent : DefaultEvent
{
    public TeacherOfficeFileEvent ToFileEvent;

    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);

        if (!EventManager.instance.switches[(int)SwitchType.TeacherOfficeFileEvent])
        {
            gameObject.SetActive(false);

            ToFileEvent.StartEvent();

            yield return new WaitForSeconds(1f);
            gameObject.SetActive(true);
        }


        yield break; 
    }
}
