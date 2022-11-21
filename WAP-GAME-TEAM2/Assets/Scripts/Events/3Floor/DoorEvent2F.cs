using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class DoorEvent2F : DefaultEvent
{
    public GameObject objSprite;
    public SwitchType switchType;
    public string openSound;
    public string keyName;

    [TextArea(1, 2)]
    public string[] doorEventDial;
    
    protected override void SwitchCheck()
    {
        if (theEvent.switches[(int)switchType])
        {
            objSprite.SetActive(false);
            gameObject.SetActive(false);
        }
    }
    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        if (!InventoryManager.instance.SearchItem(keyName)) yield break;

        theEvent.isEventIng = true;
        PlayerController.instance.IsPause = true;
        theEvent.switches[(int)switchType] = true;
        DialogueManager.instance.ShowText(doorEventDial);
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        
        AudioManager.instance.PlaySFX(openSound);
        FadeManager.instance.FadeOut();
        yield return new WaitForSeconds(2.5f);
        FadeManager.instance.FadeIn();
        objSprite.SetActive(false);
        PlayerController.instance.IsPause = false;
        theEvent.isEventIng = false;
        gameObject.SetActive(false);
    }
}
