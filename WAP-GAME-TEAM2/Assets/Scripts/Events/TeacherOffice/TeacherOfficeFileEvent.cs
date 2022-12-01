using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherOfficeFileEvent : DefaultEvent
{
    private PlayerController thePlayer;
    private AudioManager theAudio;

    [SerializeField] private string crashSound;

    protected override void SwitchCheck()
    {
        if (EventManager.instance.switches[(int)SwitchType.TeacherOfficeFileEvent])
        {
            gameObject.SetActive(false);
        }
    }

    public void StartEvent()
    {
        StartCoroutine(ExtraEventCo());
    }

    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);

        thePlayer = PlayerController.instance;
        theAudio = AudioManager.instance;

        theEvent.isEventIng = true;
        thePlayer.IsPause = true;

        yield return new WaitForSeconds(1f);

        theAudio.PlaySFX(crashSound);
        CameraManager.instance.Shake();

        yield return new WaitForSeconds(2f);

        DialogueManager.instance.ShowText(Dial);

        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);

        EventManager.instance.switches[(int)SwitchType.TeacherOfficeFileEvent] = true;

        theEvent.isEventIng = false;
        thePlayer.IsPause = false;

        gameObject.SetActive(false);

        yield break;
    }



}
