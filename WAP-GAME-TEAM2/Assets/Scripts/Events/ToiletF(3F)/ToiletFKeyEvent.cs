using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletFKeyEvent : ItemEvent
{
    private PlayerController thePlayer;
    [SerializeField] private AIController chaser;

    public string[] Dials;
    public string bloodSound;
    public string surpriseSound;

    protected override IEnumerator ItemEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);

        thePlayer = PlayerController.instance;

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

        chaser.gameObject.SetActive(true);
        chaser.chase = true;
        theEvent.isEventIng = false;
        thePlayer.IsPause = false;

        EventManager.instance.switches[(int)SwitchType.ToiletFChaseEvent] = true;

        gameObject.SetActive(false);

        yield break;
    }
}
