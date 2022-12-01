using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Event22 : DefaultEvent
{
    [TextArea(1, 2)]
    public string[] quizDial;
    [TextArea(1, 2)]
    public string[] eventDial;
    public string[] choice;

    private SpriteRenderer theSR;
    
    protected override IEnumerator ExtraEventCo()
    {
        if (theEvent.switches[(int)SwitchType.Event22])
        {
            DialogueManager.instance.ShowText(quizDial);
        }
        else
        {
            DialogueManager.instance.ShowText(eventDial[0]);
            yield return new WaitUntil(() => DialogueManager.instance.talking == false);
            if (!InventoryManager.instance.SearchItem("과산화수소")) yield break;
            theEvent.isEventIng = true;
            DialogueManager.instance.ShowText(eventDial[1]);
            yield return new WaitUntil(() => DialogueManager.instance.talking == false);
            ChoiceManager.instance.ShowChoice(choice, eventDial[2]);
            yield return new WaitUntil(() =>   ChoiceManager.instance.choiceIng == false);

            if (ChoiceManager.instance.SelectedNum == 0)
            {
                theEvent.switches[(int)SwitchType.Event22] = true;
                FadeManager.instance.FadeOut();
                theSR.sprite = null;
                yield return new WaitForSeconds(1f);
                FadeManager.instance.FadeIn();
                yield return new WaitForSeconds(1f);
                DialogueManager.instance.ShowText(eventDial[3]);
                yield return new WaitUntil(() => DialogueManager.instance.talking == false);
            }

            PlayerController.instance.IsPause = false;
            theEvent.isEventIng = false;
        }
    }

    protected override void SwitchCheck()
    {
        theSR = GetComponent<SpriteRenderer>();
        if (theEvent.switches[(int)SwitchType.Event22])
            theSR.sprite = null;
    }
}
