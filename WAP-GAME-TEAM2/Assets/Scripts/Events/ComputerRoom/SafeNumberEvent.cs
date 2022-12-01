using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeNumberEvent : DefaultEvent
{

    public NumberSystem theNumber;
    public int correctNumber;

    public string[] anotherDials;
    protected override IEnumerator ExtraEventCo()
    {
        yield return new WaitUntil(() => theDial.talking == false);
        PlayerController.instance.IsPause = true;
        theEvent.isEventIng = true;

        theNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(() => !theNumber.activated);

        if (theNumber.GetResult())
        {
            theEvent.switches[(int)theSwitch] = true;
            yield return new WaitForSeconds(0.5f);

            theAudio.PlaySFX("금고");
            yield return new WaitForSeconds(0.5f);
            theDial.ShowText(anotherDials[0]);
            yield return new WaitUntil(() => theDial.nextDialogue == true);

            theDial.ShowText(anotherDials[1]);
            yield return new WaitUntil(() => theDial.nextDialogue == true);

            theAudio.PlaySFX("Detect");
            theDial.ShowText(anotherDials[2]);
            InventoryManager.instance.GetItem("2-2반 열쇠");

            yield return new WaitUntil(() => theDial.nextDialogue == true);
            gameObject.SetActive(false);
        }
        PlayerController.instance.IsPause = false;
        theEvent.isEventIng = false;
    }

    protected override void SwitchCheck()
    {
        if (theEvent.switches[(int)theSwitch])
            gameObject.SetActive(false);
    }


}