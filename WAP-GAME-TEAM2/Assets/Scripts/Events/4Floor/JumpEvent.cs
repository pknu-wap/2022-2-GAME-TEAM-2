using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEvent : DefaultEvent
{
    public string woodItem;
    [TextArea(1, 2)]
    public string[] eventDials;

    public string stepSound;
        
    public string[] choices;
    public string choiceDial;

    public float val;
    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => theDial.nextDialogue);
        theEvent.SetEvent(true);
        if (InventoryManager.instance.SearchItem(woodItem))
        {
            theDial.ShowText(eventDials);
            yield return new WaitUntil(() => theDial.nextDialogue);
            ChoiceManager.instance.ShowChoice(choices, choiceDial);
            yield return new WaitUntil(() => !ChoiceManager.instance.choiceIng);
            if (ChoiceManager.instance.SelectedNum == 0)
            {
                FadeManager.instance.FadeOut();
                yield return new WaitForSeconds(1.5f);
                PlayerController.instance.transform.position = new Vector2(
                    PlayerController.instance.transform.position.x,
                    PlayerController.instance.transform.position.y + val);
                FadeManager.instance.FadeIn();
                yield return new WaitForSeconds(1.5f);
            }
        }
        theEvent.SetEvent(false);
    }
}
