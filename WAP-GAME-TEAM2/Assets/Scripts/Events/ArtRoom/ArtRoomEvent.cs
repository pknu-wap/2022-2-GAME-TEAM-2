using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtRoomEvent : MonoBehaviour
{
    private AudioManager theAudio;
    private DialogueManager theDial;
    private EventManager theEvent;

    public Vector2[] targetPos;
    public GameObject[] pushObj;

    public string eventDial;
    public string dropSound;

    public SwitchType artRoomSwitch;

    public bool[] result;

    void Start()
    {
        theAudio = AudioManager.instance;
        theDial = DialogueManager.instance;
        theEvent = EventManager.instance;
    }

    public void CheckResult()
    {
        for (int i = 0; i < pushObj.Length; i++)
        {
            Vector2 objPos = pushObj[i].transform.position;
            for (int j = 0; j < targetPos.Length; j++)
            {
                float DisX = Mathf.Abs(objPos.x - targetPos[j].x);
                float DisY = Mathf.Abs(objPos.y - targetPos[j].y);
                if (DisX < 0.001 && DisY < 0.001)
                    result[j] = true;
            }
        }

        for (int i = 0; i < result.Length; i++)
        {
            if (!result[i]) break;
            if (i == (result.Length - 1) && result[i])
                StartCoroutine(ArtRoomCo());
        }
    }

    IEnumerator ArtRoomCo()
    {
        if (theEvent.switches[(int)artRoomSwitch]) yield break;
            
        theEvent.SetEvent(true);
        theEvent.switches[(int)artRoomSwitch] = true;
        yield return new WaitForSeconds(1f);
        theAudio.PlaySFX(dropSound);
        yield return new WaitForSeconds(1f);
        theDial.ShowText(eventDial);
        yield return new WaitUntil(() => theDial.nextDialogue);
        theEvent.SetEvent(false);
    }
}
