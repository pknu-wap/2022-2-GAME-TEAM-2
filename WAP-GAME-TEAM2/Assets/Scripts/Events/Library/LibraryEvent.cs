using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryEvent : MonoBehaviour
{
    private EventManager theEvent;
    private AudioManager theAudio;

    public string[] answer;
    public string triggerSound;
    public string clearDial;

    public GameObject diary;

    public SwitchType badEnd;
    void Start()
    {
        theEvent = EventManager.instance;
        theAudio = AudioManager.instance;
    }
    
    public void CheckResult()
    {
        for (int i = 0; i < 9; i++)
        {
            if (answer[i] == "") continue;
            if (theEvent.libraryLists[i].Count == 0 || theEvent.libraryLists[i][0] != answer[i])
                return;
        }

        StartCoroutine(LibraryEventCo());
    }

    private IEnumerator LibraryEventCo()
    {
        theEvent.SetEvent(true);
        yield return new WaitForSeconds(1f);
        theAudio.PlaySFX(triggerSound);
        yield return new WaitForSeconds(1.5f);
        DialogueManager.instance.ShowText(clearDial);
        yield return new WaitUntil(() => !DialogueManager.instance.talking);
        diary.SetActive(true);
        theEvent.switches[(int)badEnd] = true;
        theEvent.SetEvent(false);
    }
}
