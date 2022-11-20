using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEvent : MonoBehaviour
{
    protected EventManager theEvent; 
    protected bool isCollision;
    protected bool isInteracting;
    
    public string dir;
    public float val;
    
    public string[] Dial;

    public bool isExtraEvent;
    
    void Update()
    {
        if (!isCollision || DialogueManager.instance.talking || theEvent.isEventIng) return;
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
    protected void OnTriggerEnter2D(Collider2D col)
    {
        isCollision = true;
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        isCollision = false;
    }
    

}
