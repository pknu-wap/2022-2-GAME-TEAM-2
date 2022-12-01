using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletFChaseEvent : DefaultEvent
{
    [SerializeField] private ItemEvent itemEvent;

    private PlayerController thePlayer;
    [SerializeField] private AIController chaser;

    public string[] Dials;
    public string bloodSound;
    public string surpriseSound;

    protected override void SwitchCheck()
    {
        if (EventManager.instance.switches[(int)SwitchType.ToiletFChaseEvent])
        {
            chaser.gameObject.SetActive(false);
            itemEvent.gameObject.SetActive(true);
            itemEvent.spriteObj.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);

        thePlayer = PlayerController.instance;

        theEvent.isEventIng = true;
        thePlayer.IsPause = true;

        SpawnManager.instance.StartChase(chaser);
        SpawnManager.instance.chaserNumber = 2;
        chaser.SetNodeArray();

        AudioManager.instance.PlaySFX(surpriseSound);
        thePlayer.SetBalloonAnim();
        yield return new WaitForSeconds(2f);

        AudioManager.instance.PlaySFX(bloodSound);
        yield return new WaitForSeconds(0.8f);
        AudioManager.instance.PlaySFX(bloodSound);
        yield return new WaitForSeconds(0.6f);
        AudioManager.instance.PlaySFX(bloodSound);
        yield return new WaitForSeconds(0.4f);
        AudioManager.instance.PlaySFX(bloodSound);
        yield return new WaitForSeconds(0.4f);
        AudioManager.instance.PlaySFX(bloodSound);
        yield return new WaitForSeconds(0.2f);
        AudioManager.instance.PlaySFX(bloodSound);
        yield return new WaitForSeconds(0.2f);
        AudioManager.instance.PlaySFX(bloodSound);
        yield return new WaitForSeconds(0.1f);
        AudioManager.instance.PlaySFX(bloodSound);
        yield return new WaitForSeconds(0.1f);
        AudioManager.instance.PlaySFX(bloodSound);
        yield return new WaitForSeconds(0.1f);
        AudioManager.instance.PlaySFX(bloodSound);
        yield return new WaitForSeconds(0.1f);
        AudioManager.instance.PlaySFX(bloodSound);
        yield return new WaitForSeconds(0.1f);

        theEvent.isEventIng = false;
        thePlayer.IsPause = false;

        yield return new WaitForSeconds(1f);

        chaser.chase = true;

        EventManager.instance.switches[(int)SwitchType.ToiletFChaseEvent] = true;

        itemEvent.gameObject.SetActive(true);
        itemEvent.spriteObj.gameObject.SetActive(true);

        gameObject.SetActive(false);

        yield break;
    }
}
