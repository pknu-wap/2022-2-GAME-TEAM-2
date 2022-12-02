using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainDiary : MonoBehaviour
{
    protected EventManager theEvent;
    protected DialogueManager theDial;
    protected AudioManager theAudio;

    public GameObject spriteObj;

    public bool isExtraEvent;

    public LayerMask layerMask;

    public string getSound = "Detect";
    public string[] getDial;

    public GameObject diaryObj;
    public int diaryNum;
    public string bookSound;

    public SwitchType DiarySwitch;

    private bool isCoroutine;
    protected virtual IEnumerator DiaryEventCo() { yield break; }

    protected void Start()
    {
        theEvent = EventManager.instance;
        theDial = DialogueManager.instance;
        theAudio = AudioManager.instance;
        SwitchCheck();
    }

    void Update()
    {
        if (isCoroutine) return;
        isCoroutine = true;
        StartCoroutine(ObtainDiaryCo());
    }

    protected virtual IEnumerator ObtainDiaryCo()
    {

        if (DialogueManager.instance.talking || theEvent.switches[(int)DiarySwitch]
                                             || theEvent.isEventIng 
                                             || theEvent.isWorking)
        {
            isCoroutine = false;
            yield break;
        }
        
        if (!CanPlayerInteract())
        {
            isCoroutine = false;
            yield break;
        }


        if (!Input.GetKeyDown(KeyCode.Z))
        {
            isCoroutine = false;
            yield break;
        }
        
        int len = getDial.Length;
        for (int i = 0; i < len - 1; i++)
        {
            DialogueManager.instance.ShowText(getDial[i]);
            yield return new WaitUntil(() => theDial.nextDialogue);
        }

        theEvent.SetEvent(true);
        AudioManager.instance.PlaySFX(getSound); 
        DialogueManager.instance.ShowText(getDial[len - 1]);
        theEvent.diaryObtained[diaryNum] = true;
        SetSwitch();
        yield return new WaitUntil(() => theDial.nextDialogue);
        yield return new WaitForSeconds(0.1f);
        theAudio.PlaySFX(bookSound);
        diaryObj.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        diaryObj.SetActive(false);
        theEvent.SetEvent(false);

        if (isExtraEvent)
        {
            theEvent.SetEvent(true);
            StartCoroutine(DiaryEventCo());
        }
        if (spriteObj != null)
            spriteObj.gameObject.SetActive(false);
        
        yield return new WaitUntil(() => !theEvent.isEventIng);
        gameObject.SetActive(false);
    }

    protected virtual void SetSwitch()
    {
        EventManager.instance.switches[(int)DiarySwitch] = true;
    }

    protected virtual void SwitchCheck()
    {
        if (theEvent.switches[(int)DiarySwitch])
        {
            if (spriteObj != null)
                spriteObj.gameObject.SetActive(false);
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
