using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent1F : MonoBehaviour
{
    
    private EventManager theEvent;
    private DialogueManager theDial;
    private AudioManager theAudio;
    private InventoryManager theInven;
    
    public GameObject[] objSprite;
    public string openSound;

    [TextArea(1, 2)]
    public string[] doorEventDial;

    public string choiceString;
    public string[] choiceText;
    
    public string 
    
    private bool isInteracting;
    private bool isCollision;

    public LayerMask layerMask;

    [TextArea(1, 2)] 
    public string[] Dial;

    public bool isExtraEvent;

    public SwitchType[] theSwitchs;
    
    protected virtual void Start()
    {
        theEvent = EventManager.instance;
        theDial = DialogueManager.instance;
        theAudio = AudioManager.instance;
        theInven = InventoryManager.instance;
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
        }
        else
        {
            
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
        if (theInven.SearchItem())
    }

}
