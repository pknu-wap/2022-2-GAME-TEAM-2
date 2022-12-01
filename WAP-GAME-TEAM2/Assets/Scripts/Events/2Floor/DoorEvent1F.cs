using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorEvent1F : MonoBehaviour
{
    
    private EventManager theEvent;
    private DialogueManager theDial;
    private AudioManager theAudio;
    private InventoryManager theInven;
    private ChoiceManager theChoice;
    
    public GameObject[] objSprite;
    public string openSound;

    public string[] choiceString;
    public string[] choiceText;

    public string knifeSound;

    private bool isInteracting;
    private bool isCollision;

    public LayerMask layerMask;

    [TextArea(1, 2)] 
    public string[] Dial;
    
    [TextArea(1, 2)]
    public string[] knifeEventDial;
    [TextArea(1, 2)]
    public string[] OpenEventDial;

    public SwitchType[] theSwitchs;
    
    protected virtual void Start()
    {
        theEvent = EventManager.instance;
        theDial = DialogueManager.instance;
        theAudio = AudioManager.instance;
        theInven = InventoryManager.instance;
        theChoice = ChoiceManager.instance;
        SwitchCheck();
    }
    
    protected virtual void Update()
    {
        if (!isCollision || DialogueManager.instance.talking || ChoiceManager.instance.choiceIng 
            ||theEvent.isEventIng || theEvent.isWorking) return;
        if (isInteracting && !DialogueManager.instance.talking)
        {
            isInteracting = false;
            return;
        }
        if (!CanPlayerInteract())
            return;
        
        if (!Input.GetKeyDown(KeyCode.Z)) return;
        if (!theEvent.switches[(int)theSwitchs[0]])
        {
            theDial.ShowText(Dial);
            StartCoroutine(KnifeEventCo());
        }
        else
        {
            StartCoroutine(OpenEventCo());
        }
        isInteracting = true;
        
    }

    protected virtual void SwitchCheck()
    {
        if (theEvent.switches[(int)theSwitchs[0]])
        {
            objSprite[0].SetActive(false);
            objSprite[1].SetActive(false);
        }
        
        if (theEvent.switches[(int)theSwitchs[1]])
            gameObject.SetActive(false);
    }
    protected void OnTriggerEnter2D(Collider2D col)
    {
        isCollision = true;
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        isCollision = false;
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

    private IEnumerator KnifeEventCo()
    {
        yield return new WaitUntil(() => theDial.nextDialogue);
        if (theInven.SearchItem("칼"))
        {
            theEvent.SetEvent(true);
            theDial.ShowText(knifeEventDial);
            yield return new WaitUntil(() => theDial.nextDialogue);
            yield return null;
            theChoice.ShowChoice(choiceText, choiceString[0]);
            yield return new WaitUntil(() => !theChoice.choiceIng);
            if (theChoice.SelectedNum == 0)
            {
                theAudio.PlaySFX(knifeSound);
                FadeManager.instance.FadeOut();
                yield return new WaitForSeconds(1f);
                objSprite[0].SetActive(false);
                objSprite[1].SetActive(false);
                theEvent.switches[(int)theSwitchs[0]] = true;
                FadeManager.instance.FadeIn();
                yield return new WaitForSeconds(1.5f);
            }
            theEvent.SetEvent(false);
        }
    }

    private IEnumerator OpenEventCo()
    {
        theEvent.isEventIng = true;
        PlayerController.instance.IsPause = true;
        DialogueManager.instance.ShowText(OpenEventDial);
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        ChoiceManager.instance.ShowChoice(choiceText, choiceString[1]);
        yield return new WaitUntil(() =>   ChoiceManager.instance.choiceIng == false);
        if (ChoiceManager.instance.SelectedNum == 0)
        {
            theEvent.switches[(int)theSwitchs[1]] = true;
            AudioManager.instance.PlaySFX(openSound);
            FadeManager.instance.FadeOut();
            yield return new WaitForSeconds(2.5f);
            FadeManager.instance.FadeIn();
            objSprite[2].SetActive(false);
            gameObject.SetActive(false);
        }
        PlayerController.instance.IsPause = false;
        theEvent.isEventIng = false;
    }

}
