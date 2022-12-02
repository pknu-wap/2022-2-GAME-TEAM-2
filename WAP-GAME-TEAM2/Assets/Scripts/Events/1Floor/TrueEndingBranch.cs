using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueEndingBranch : MonoBehaviour
{
    AudioManager theAudio;
    EventManager theEvent;
    DialogueManager theDial;

    public Door bipoom;

    public string[] dials;
    void Start()
    {
        theEvent = EventManager.instance;
        if (theEvent.switches[(int)SwitchType.FurROpened])
        {
            bipoom.doorOpened = true;
            return;
        }
        
        theAudio = AudioManager.instance;    
        theDial = DialogueManager.instance;
        for (int i = 0; i < 8; i++)
        {
            if (!theEvent.diaryObtained[i]) return;
        }
        StartCoroutine(TEBCo());
    }

    IEnumerator TEBCo()
    {
        theEvent.SetEvent(true);
        yield return new WaitForSeconds(1f);
        theEvent.switches[(int)SwitchType.FurROpened] = true;
        theAudio.PlaySFX("Ãæµ¹");
        CameraManager.instance.Shake();
        PlayerController.instance.SetBalloonAnim();
        yield return new WaitForSeconds(1f);
        theDial.ShowText(dials);
        yield return new WaitUntil(() => theDial.nextDialogue);
        theEvent.SetEvent(false);
        bipoom.doorOpened = true;
    }
}
