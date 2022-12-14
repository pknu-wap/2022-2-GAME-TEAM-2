using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemEvent : MonoBehaviour
{  
    protected EventManager theEvent; 
    protected bool isInteraction;
    protected bool isCollision;

    public GameObject spriteObj;
    
    public bool isExtraEvent;

    public LayerMask layerMask;

    public string itemName;
    public string getSound = "Detect";
    [TextArea(1, 2)]
    public string[] getDial;
    
    public SwitchType ItemSwitch;

    protected virtual IEnumerator ItemEventCo() { yield break;}


    protected void Start()
    {
        theEvent = EventManager.instance;
        SwitchCheck();
    }
    protected virtual void Update()
    {
        if (!isCollision || DialogueManager.instance.talking || theEvent.isEventIng
            || theEvent.isWorking || ChoiceManager.instance.choiceIng) return;
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
        isInteraction = true;
        if (isExtraEvent)
        {
            theEvent.isEventIng = true;
            PlayerController.instance.IsPause = true;
            StartCoroutine(ItemEventCo());
        }
        else
        {
            if (spriteObj != null)
                spriteObj.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        if (spriteObj != null)
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
            if (spriteObj != null)
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

    protected void OnTriggerEnter2D(Collider2D col)
    {
        isCollision = true;
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        isCollision = false;
    }

}
