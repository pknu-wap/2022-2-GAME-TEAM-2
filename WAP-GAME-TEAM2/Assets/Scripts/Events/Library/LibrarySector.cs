using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrarySector : MonoBehaviour
{
    private EventManager theEvent;
    private DialogueManager theDial;
    private AudioManager theAudio;
    private ChoiceManager theChoice;
    
    private bool isInteracting;
    private bool isCollision;
    
    public LayerMask layerMask;

    [TextArea(1, 2)] 
    public string Dial;

    public string[] choices;
    public string choiceDial;
    
    public int sectorNum;

    public LibraryUI actionObj;
    void Start()
    {
        theEvent = EventManager.instance;
        theDial = DialogueManager.instance;
        theAudio = AudioManager.instance;
        theChoice = ChoiceManager.instance;
    }

    void Update()
    {
        if (!isCollision || DialogueManager.instance.talking || ChoiceManager.instance.choiceIng
            || theEvent.isEventIng || theEvent.isWorking) return;
        if (isInteracting && !DialogueManager.instance.talking)
        {
            isInteracting = false;
            return;
        }
        if (!CanPlayerInteract())
            return;
        
        if (!Input.GetKeyDown(KeyCode.Z)) return;

        isInteracting = true;
        theDial.ShowText(Dial);
        StartCoroutine(SectorCo());
    }

    private IEnumerator SectorCo()
    {
        theEvent.SetEvent(true);
        yield return new WaitUntil(() => theDial.nextDialogue);
        yield return null;
        theChoice.ShowChoice(choices, choiceDial);
        yield return new WaitUntil(() => !theChoice.choiceIng);
        yield return null;
        actionObj.actionNum = theChoice.SelectedNum;
        actionObj.sectorNum = sectorNum;
        actionObj.gameObject.SetActive(true);
    }
    

    void OnTriggerEnter2D(Collider2D col)
    {
        isCollision = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        isCollision = false;
    }

    private bool CanPlayerInteract()
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
