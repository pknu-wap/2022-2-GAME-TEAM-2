using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AudiovisualRoomChaseEvent : DefaultEvent
{
    public GameObject[] objSprite;

    private PlayerController thePlayer;
    private Light2D playerLight;

    [SerializeField] private AIController chaser;
    
    public string laughSound;

    protected override void SwitchCheck()
    {
        if (EventManager.instance.switches[(int)SwitchType.AudiovisualRoomChaseEvent])
        {
            for(int i = 0; i < objSprite.Length; i++)
                objSprite[i].SetActive(false);
            
            gameObject.SetActive(false);
        }
    }

    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);

        for (int i = 0; i < objSprite.Length; i++)
            objSprite[i].SetActive(false);

        thePlayer = PlayerController.instance;
        playerLight = thePlayer.flashLight;
        playerLight.intensity = 0.3f;
        playerLight.pointLightOuterRadius = 2f;

        theAudio = AudioManager.instance;
        theAudio.PlaySFX(laughSound);

        theEvent.isEventIng = true;
        thePlayer.IsPause = true;

        yield return new WaitForSeconds(3f);

        theEvent.isEventIng = false;
        thePlayer.IsPause = false;

        chaser.gameObject.SetActive(true);
        chaser.chase = true;

        yield break;
    }
}

