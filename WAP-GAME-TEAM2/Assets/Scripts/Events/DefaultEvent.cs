using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEvent : MonoBehaviour
{
    protected EventManager theEvent;
    protected DialogueManager theDial;
    protected AudioManager theAudio;

    protected bool isInteracting;
    protected bool isCollision;

    public LayerMask layerMask;

    [TextArea(1, 2)] 
    public string[] Dial;

    public bool isExtraEvent;

    public SwitchType theSwitch;
    
    protected virtual void Start()
    {
        theEvent = EventManager.instance;
        theDial = DialogueManager.instance;
        theAudio = AudioManager.instance;
        SwitchCheck();
    }
    
    protected virtual void Update()
    {
        if (!CanPlayerInteract())
            return;
        
        if (!isCollision || DialogueManager.instance.talking || ChoiceManager.instance.choiceIng ||theEvent.isEventIng) return;
        if (isInteracting && !DialogueManager.instance.talking)
        {
            isInteracting = false;
            return;
        }
        
        if (!Input.GetKeyDown(KeyCode.Z)) return;
        
        DialogueManager.instance.ShowText(Dial);
        isInteracting = true;
        if (isExtraEvent)
        {
            StartCoroutine(ExtraEventCo());
        }
    }

    protected virtual IEnumerator ExtraEventCo() {yield break;}
    protected virtual void SwitchCheck() { return;}
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
    

}
