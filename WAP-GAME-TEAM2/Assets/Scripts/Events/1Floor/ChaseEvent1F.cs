using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEvent1F: DefaultEvent
{
    public PrincipalOfficeDoor door;
    public AIController chaser;
    private PlayerController thePlayer;
    private AudioManager theAudio;

    public string crashSound;

    protected override void SwitchCheck()
    {
        if (EventManager.instance.switches[(int)SwitchType.ChaseEvent1F])
        {
            gameObject.SetActive(false);
        }
    }

    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);

        if (EventManager.instance.switches[(int)SwitchType.TeacherOfficeFileEvent])
        {
            thePlayer = PlayerController.instance;
            theAudio = AudioManager.instance;

            theEvent.isEventIng = true;
            thePlayer.IsPause = true;

            yield return new WaitForSeconds(1f);

            theAudio.PlaySFX(crashSound);
            CameraManager.instance.Shake();
            yield return new WaitForSeconds(2f);
            theAudio.PlaySFX(crashSound);
            CameraManager.instance.Shake();
            yield return new WaitForSeconds(2f);
            theAudio.PlaySFX(crashSound);
            CameraManager.instance.Shake();

            SpawnManager.instance.StartChase(chaser);
            SpawnManager.instance.chaserNumber = 1;
            chaser.gameObject.SetActive(true);

            theEvent.switches[(int)SwitchType.PoDoorOpened] = true;
            door.DoorOpen();

            theEvent.isEventIng = false;
            thePlayer.IsPause = false;

            yield return new WaitForSeconds(1f);
            chaser.chase = true;

            EventManager.instance.switches[(int)SwitchType.ChaseEvent1F] = true;


            gameObject.SetActive(false);
        }

        yield break;
    }
}
