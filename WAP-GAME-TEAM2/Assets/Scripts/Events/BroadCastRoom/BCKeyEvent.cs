using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BCKeyEvent : ItemEvent
{
    private PlayerController thePlayer;

    private Light2D playerLight;
    public AIController chaser;

    public string[] Dials;
    public string offSound;
    public string surpriseSound;
    public string misterySound;
    public string heartBeatSound;

    protected override IEnumerator ItemEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        yield return new WaitForSeconds(1f);
        thePlayer = PlayerController.instance;
        playerLight = thePlayer.flashLight;

        AudioManager.instance.PlaySFX(offSound);
        playerLight.gameObject.SetActive(true);
        playerLight.intensity = 0f;
        yield return new WaitForSeconds(1.5f);

        DialogueManager.instance.ShowText(Dials);
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        yield return new WaitForSeconds(1.5f);
        playerLight.intensity = 1f;
        AudioManager.instance.StopBGM();
        AudioManager.instance.PlaySFX(surpriseSound);
        chaser.gameObject.transform.position = new Vector2(thePlayer.transform.position.x - 2f
            , thePlayer.transform.position.y);
        chaser.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        thePlayer.SetPlayerDirAnim("DirX", -1f);
        yield return new WaitForSeconds(1f);
        AudioManager.instance.PlaySFX(misterySound);
        thePlayer.SetBalloonAnim();
        yield return new WaitForSeconds(2f);

        AudioManager.instance.PlaySFX(heartBeatSound);
        yield return new WaitForSeconds(1.5f);
        AudioManager.instance.PlaySFX(heartBeatSound);
        yield return new WaitForSeconds(1.5f);
        AudioManager.instance.PlaySFX(heartBeatSound);
        yield return new WaitForSeconds(1.5f);

        SpawnManager.instance.StartChase(chaser);
        SpawnManager.instance.chaserNumber = 1;
        chaser.SetNodeArray();
        chaser.chase = true;

        theEvent.isEventIng = false;
        thePlayer.IsPause = false;
    }

}