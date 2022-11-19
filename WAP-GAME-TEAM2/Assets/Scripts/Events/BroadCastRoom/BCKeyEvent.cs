using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BCKeyEvent : ItemEvent
{
    private PlayerController thePlayer;
    
    public Light2D playerLight;
    public GameObject chaser;
    
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
        
        AudioManager.instance.PlaySFX(offSound);
        playerLight.gameObject.SetActive(true);
        playerLight.intensity = 0f;
        yield return new WaitForSeconds(1.5f);
        
        DialogueManager.instance.ShowText(Dials);
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        yield return new WaitForSeconds(1.5f);
        playerLight.intensity = 1f;
        AudioManager.instance.Stop();
        AudioManager.instance.PlaySFX(surpriseSound);
        chaser.gameObject.transform.position = new Vector2(thePlayer.transform.position.x - 1f
            , thePlayer.transform.position.y);
        chaser.SetActive(true);
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

        theEvent.isEventIng = false;
        thePlayer.IsPause = false;
    }

    protected override void SwitchCheck()
    {
        if (theEvent.switches[(int)SwitchType.BCKeyEvent])
        {
            gameObject.SetActive(false);
            spriteObj.SetActive(false);
        }
            
    }
    protected override void SetSwitch()
    {
        theEvent.switches[(int)SwitchType.BCKeyEvent] = true;
    }

   
}
