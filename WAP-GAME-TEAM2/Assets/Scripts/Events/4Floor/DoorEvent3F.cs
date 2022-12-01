using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class DoorEvent3F : DefaultEvent
{
    public GameObject objSprite;
    public string openSound;

    [TextArea(1, 2)]
    public string[] doorEventDial;

    public string choiceString;
    public string[] choiceText;
    
    protected override void SwitchCheck()
    {
        if (theEvent.switches[(int)theSwitch])
        {
            objSprite.SetActive(false);
            gameObject.SetActive(false);
        }
    }
    protected override IEnumerator ExtraEventCo()
    {
        theEvent.isEventIng = true;
        PlayerController.instance.IsPause = true;
        DialogueManager.instance.ShowText(doorEventDial);
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        ChoiceManager.instance.ShowChoice(choiceText, choiceString);
        yield return new WaitUntil(() =>   ChoiceManager.instance.choiceIng == false);
        if (ChoiceManager.instance.SelectedNum == 0)
        {
            theEvent.switches[(int)theSwitch] = true;
            AudioManager.instance.PlaySFX(openSound);
            FadeManager.instance.FadeOut();
            yield return new WaitForSeconds(2.5f);
            FadeManager.instance.FadeIn();
            objSprite.SetActive(false);
            gameObject.SetActive(false);
        }
        PlayerController.instance.IsPause = false;
        theEvent.isEventIng = false;
    }
}