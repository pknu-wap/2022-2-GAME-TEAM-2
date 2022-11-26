using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletFChaseEvent : DefaultEvent
{
    [SerializeField] private ItemEvent itemEvent;

    private PlayerController thePlayer;
    [SerializeField] private AIController chaser;

    public string[] Dials;
    public string heartBeatSound;
    public string surpriseSound;

    protected override void SwitchCheck()
    {
        if (EventManager.instance.switches[(int)SwitchType.ToiletFChaseEvent])
        {
            gameObject.SetActive(false);
        }
    }

    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);

        thePlayer = PlayerController.instance;

        theEvent.isEventIng = true;
        thePlayer.IsPause = true;

        AudioManager.instance.PlaySFX(surpriseSound);
        yield return new WaitForSeconds(1f);

        AudioManager.instance.PlaySFX(heartBeatSound);
        yield return new WaitForSeconds(1.5f);
        AudioManager.instance.PlaySFX(heartBeatSound);
        yield return new WaitForSeconds(1.5f);
        AudioManager.instance.PlaySFX(heartBeatSound);
        yield return new WaitForSeconds(1.5f);

        theEvent.isEventIng = false;
        thePlayer.IsPause = false;

        yield return new WaitForSeconds(1f);

        chaser.chase = true;

        itemEvent.gameObject.SetActive(true);
        itemEvent.spriteObj.gameObject.SetActive(true);

        gameObject.SetActive(false);

        yield break;
    }
}
