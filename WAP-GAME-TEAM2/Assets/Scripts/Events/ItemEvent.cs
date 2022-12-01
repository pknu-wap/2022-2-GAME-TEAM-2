using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEvent : MonoBehaviour
{  
    protected EventManager theEvent; 
    protected bool isInteraction;

    public GameObject spriteObj;
    
    public bool isExtraEvent;

    public LayerMask layerMask;

    public string itemName;
    public string getSound = "Detect";
    public string getDial;
    
    public SwitchType ItemSwitch;
    protected abstract IEnumerator ItemEventCo();

    protected void Start()
    {
        theEvent = EventManager.instance;
        SwitchCheck();
    }
    protected void Update()
    {
        if (DialogueManager.instance.talking || theEvent.isEventIng) return;
        if (isInteraction && !DialogueManager.instance.talking)
        {
            isInteraction = false;
            return;
        }

        if (!CanPlayerInteract())
            return;

        if (!Input.GetKeyDown(KeyCode.Z)) return;

        AudioManager.instance.PlaySFX(getSound); 
        DialogueManager.instance.ShowText(getDial);
        InventoryManager.instance.GetItem(itemName);

        SetSwitch();

        if (isExtraEvent)
        {
            isInteraction = true;
            theEvent.isEventIng = true;
            PlayerController.instance.IsPause = true;
            StartCoroutine(ItemEventCo());
        }

        spriteObj.gameObject.SetActive(false);
    }

    protected virtual void SetSwitch()
    {
        EventManager.instance.switches[(int)ItemSwitch] = true;
    }

    protected virtual void SwitchCheck()
    {
        if (theEvent.switches[(int)ItemSwitch])
        {
            spriteObj.SetActive(false);
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
   
}
