using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AudiovisualRoomLightEvent : DefaultEvent
{
    private PlayerController thePlayer;
    private Light2D playerLight;

    [SerializeField] private Light2D light;
    
    [SerializeField] private string lightOn;
    [SerializeField] private string scream;

    [SerializeField] private AIController chaser;

    [SerializeField] private ItemEvent itemEvent;

    protected override void SwitchCheck()
    {
        if (EventManager.instance.switches[(int)SwitchType.AudiovisualRoomLightEvent])
        {
            gameObject.SetActive(false);
        }
    }

    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);

        thePlayer = PlayerController.instance;
        playerLight = thePlayer.flashLight;

        theAudio = AudioManager.instance;

        theEvent.isEventIng = true;
        thePlayer.IsPause = true;

        theAudio.PlaySFX(lightOn);

        playerLight.intensity = 1f;
        playerLight.pointLightOuterRadius = 3f;
        playerLight.gameObject.SetActive(false);

        light.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        theAudio.PlaySFX(scream);

        itemEvent.gameObject.SetActive(true);
        itemEvent.transform.position = chaser.transform.position;
        itemEvent.spriteObj.gameObject.SetActive(true);
        itemEvent.spriteObj.transform.position = chaser.transform.position;

        chaser.chase = false;
        chaser.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        theEvent.isEventIng = false;
        thePlayer.IsPause = false;

        yield break;
    }
}
