using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookEvent : MonoBehaviour
{
    protected EventManager theEvent;
    protected DialogueManager theDial;
    protected ChoiceManager theChoice;

    protected bool isCollision;

    public bool isExtraEvent;

    public LayerMask layerMask;

    public string itemName;
    public string getSound = "Detect";
    public string[] getDials;

    public string choiceDial;
    public string[] choices;

    public SwitchType ItemSwitch;
    public SwitchType EventSwitch;

    private bool isInteracting;
    protected virtual IEnumerator ItemEventCo() { yield break; }

    protected void Start()
    {
        theEvent = EventManager.instance;
        theDial = DialogueManager.instance;
        theChoice = ChoiceManager.instance;
        SwitchCheck();
    }
    protected virtual void Update()
    {
        if (!isCollision || theDial.talking || theChoice.choiceIng || theEvent.isEventIng
            || theEvent.isWorking || ChoiceManager.instance.choiceIng) return;
        if (isInteracting && !DialogueManager.instance.talking) // 대사 끝날때 z를 누른 프레임과 동일한 update 프레임에 z가 눌리는 일 방지.
        {
            isInteracting = false;
            return;
        }
        if (!CanPlayerInteract()) return;
        if (!Input.GetKeyDown(KeyCode.Z)) return;

        isInteracting = true;
        StartCoroutine(dialCo());
    }

    private IEnumerator dialCo()
    {
        theEvent.SetEvent(true);
        int len = getDials.Length;
        for (int i = 0; i < len - 1; i++)
        {
            theDial.ShowText(getDials[i]);
            yield return new WaitUntil(() => theDial.nextDialogue);
        }
        theChoice.ShowChoice(choices, choiceDial);
        yield return new WaitUntil(() => !theChoice.choiceIng);
        if (theChoice.SelectedNum == 0)
        {
            AudioManager.instance.PlaySFX(getSound);
            theDial.ShowText(getDials[len - 1]);
            InventoryManager.instance.GetItem(itemName);
            yield return new WaitUntil(() => theDial.nextDialogue);
            SetSwitch();
            theEvent.SetEvent(false);
            gameObject.SetActive(false);
            
        }
        theEvent.SetEvent(false);

    }

    protected virtual void SetSwitch()
    {
        EventManager.instance.switches[(int)ItemSwitch] = true;
    }

    protected virtual void SwitchCheck()
    {
        if (theEvent.switches[(int)ItemSwitch] || !theEvent.switches[(int)EventSwitch])
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual bool CanPlayerInteract()
    {
        Vector2 vector = PlayerController.instance.GetVector();

        Vector2 start = PlayerController.instance.transform.position;
        Vector2 end = start + new Vector2(vector.x, vector.y);

        RaycastHit2D hit;

        hit = Physics2D.Linecast(start, end, layerMask);

        if (!hit)
            return false;

        if (hit.collider.gameObject == this.gameObject)
            return true;

        return false;
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        isCollision = true;
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        isCollision = false;
    }
}
