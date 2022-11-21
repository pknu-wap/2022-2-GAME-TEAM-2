using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEvent : MonoBehaviour
{  
    protected EventManager theEvent; 
    protected bool isCollision;
    protected bool isInteraction;

    public GameObject spriteObj;
    
    public bool isExtraEvent;

    public string dir;
    public float val;

    public string itemName;
    public string getSound = "Detect";
    public string getDial;
    
    public SwitchType ItemSwitch;
    protected abstract IEnumerator ItemEventCo();

    protected void Start()
    {
        theEvent = EventManager.instance;
        SwitchCheck();

        isCollision = false;
    }
    protected void Update()
    {
        if (!isCollision || DialogueManager.instance.talking || theEvent.isEventIng) return;
        if (isInteraction && !DialogueManager.instance.talking)
        {
            isInteraction = false;
            return;
        }
        
        float dirValue = PlayerController.instance.GetPlayerDir(dir);
        if (dirValue != val) return;
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isCollision = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollision = false;
    }
    
   
}
