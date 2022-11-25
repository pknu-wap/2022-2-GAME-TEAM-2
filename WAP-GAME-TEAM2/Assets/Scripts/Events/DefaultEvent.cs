using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEvent : MonoBehaviour
{
    protected EventManager theEvent;
    protected bool isInteracting;
    protected bool isCollision;
    
    public string dir;
    public float val;
    
    [TextArea(1, 2)] 
    public string[] Dial;

    public bool isExtraEvent;
    
    protected virtual void Start()
    {
        theEvent = EventManager.instance;
        SwitchCheck();
    }
    
    protected virtual void Update()
    {
        if (!isCollision || DialogueManager.instance.talking || ChoiceManager.instance.choiceIng ||theEvent.isEventIng) return;
        if (isInteracting && !DialogueManager.instance.talking)
        {
            isInteracting = false;
            return;
        }
        
        float dirValue = PlayerController.instance.GetPlayerDir(dir);
        if (dirValue != val) return;
        
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
    

}
