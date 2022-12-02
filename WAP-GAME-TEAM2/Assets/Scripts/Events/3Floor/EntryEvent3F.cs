using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryEvent3F : MonoBehaviour
{
    private PlayerController thePlayer;
    public SwitchType switchType;
    public string[] dial;
    void Start()
    {
        if (EventManager.instance.switches[(int)switchType])
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(EntryEvent3FCo());
    }

    IEnumerator EntryEvent3FCo()
    {
        thePlayer = PlayerController.instance;
        EventManager.instance.isEventIng = true;
        thePlayer.IsPause = true;
        
        AudioManager.instance.PlaySFX("Mistery");
        thePlayer.SetBalloonAnim();
        yield return new WaitForSeconds(2f);
        DialogueManager.instance.ShowText(dial);

        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue);
        EventManager.instance.switches[(int)switchType] = true;
        EventManager.instance.isEventIng = false;
        thePlayer.IsPause = false;
        gameObject.SetActive(false);
    }
}
