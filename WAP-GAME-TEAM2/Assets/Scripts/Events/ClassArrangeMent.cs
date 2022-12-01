using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassArrangeMent : DefaultEvent
{
    public string paperSound;
    public GameObject paperObj;
    protected override IEnumerator ExtraEventCo()
    {
        theEvent.SetEvent(true);
        yield return new WaitUntil(() => !theDial.talking);
        yield return null;
        theAudio.PlaySFX(paperSound);
        paperObj.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        paperObj.SetActive(false);
        theEvent.SetEvent(false);
    }
}
