using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet4FDoor : Door
{
    [TextArea(1, 2)]
    public string[] openDials;
    [TextArea(1, 2)]
    public string[] lockDials;
    public string[] choice;
    public string choiceDial;

    public string fireSound;
    
    protected override void Update()
    {
        if (!isCollision || DialogueManager.instance.talking || ChoiceManager.instance.choiceIng || EventManager.instance.isEventIng) return;
        
        float dirValue = PlayerController.instance.GetPlayerDir(dir);
        if (dirValue != val) return;
        
        if (isInteracting && !DialogueManager.instance.talking) // 대사 끝날때 z를 누른 프레임과 동일한 update 프레임에 z가 눌리는 일 방지.
        {
            isInteracting = false;
            return;
        }
        
        if (!Input.GetKeyDown(KeyCode.Z)) return;
        if (doorOpened)
        {
            StartCoroutine(MoveCo());
        }
        else
        {
            DialogueManager.instance.ShowText(lockDials);
            // 키 체크
            if (InventoryManager.instance.SearchItem(keyItemName))
            {
                EventManager.instance.SetEvent(true);
                StartCoroutine(dialCo());
            }
        }
        isInteracting = true;
    }

    private IEnumerator dialCo()
    {
        DialogueManager theDial = DialogueManager.instance;
        ChoiceManager theChoice = ChoiceManager.instance;
        AudioManager theAudio = AudioManager.instance;
        yield return new WaitUntil(() => theDial.nextDialogue == true);
        theDial.ShowText(openDials[0]);
        yield return new WaitUntil(() => theDial.nextDialogue == true);
        theChoice.ShowChoice(choice, choiceDial);
        yield return new WaitUntil(() => theChoice.choiceIng == false);
        if (theChoice.SelectedNum == 0)
        {
            yield return new WaitForSeconds(0.5f);
            theAudio.PlaySFX(fireSound);
            yield return new WaitForSeconds(1f);
            theDial.ShowText(openDials[1]);
            yield return new WaitUntil(() => theDial.nextDialogue == true);
            EventManager.instance.switches[(int)doorSwitch] = true;
            InventoryManager.instance.DeleteItem("알코올램프");
            EventManager.instance.SetEvent(false);
            StartCoroutine(MoveCo());
        }
        EventManager.instance.SetEvent(false);
    }
}
