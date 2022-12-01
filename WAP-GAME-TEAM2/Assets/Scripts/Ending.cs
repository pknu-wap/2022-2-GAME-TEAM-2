using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    private EventManager theEvent;
    private AudioManager theAudio;
    private DialogueManager theDial;

    [TextArea(1, 2)]
    public string[] TrueEndDials;
    [TextArea(1, 2)]
    public string[] BadEndDials;

    [TextArea(1, 2)]
    public string[] afterTrueEndingDials;
    [TextArea(1, 2)]
    public string[] afterBadEndingDials;

    public string bellSound;
    public string trueEndBGM;
    public string fallDownSound;
    public string doorOpenSound;
    
    public SwitchType trueEnd;
    void Start()
    {
        theEvent = EventManager.instance;
        theAudio = AudioManager.instance;
        theDial = DialogueManager.instance;

        if (theEvent.switches[(int)trueEnd])
            StartCoroutine(TrueEndCoroutine());
        else
            StartCoroutine(BadEndCoroutine());

    }

    IEnumerator BadEndCoroutine()
    {
        yield return new WaitForSeconds(2f);
        theDial.ShowText(BadEndDials);
        yield return new WaitUntil(() => theDial.nextDialogue);
        theAudio.PlaySFX(fallDownSound);
        yield return new WaitForSeconds(2f);
        theAudio.PlaySFX(bellSound);
        yield return new WaitForSeconds(7.5f);
        theDial.ShowText(afterBadEndingDials);
        yield return new WaitUntil(() => theDial.nextDialogue);
        yield return new WaitForSeconds(0.5f);
    }
    
    IEnumerator TrueEndCoroutine()
    {
        for (int i = 0; i < 2; i++)
        {
            theDial.ShowText(TrueEndDials[i]);
            yield return new WaitUntil(() => theDial.nextDialogue);
        }
        yield return new WaitForSeconds(0.5f);
        theAudio.PlaySFX(doorOpenSound);
        yield return new WaitForSeconds(2f);
        theAudio.PlayBGM(trueEndBGM);
        for (int i = 2; i < TrueEndDials[i].Length; i++)
        {
            theDial.ShowText(TrueEndDials[i]);
            yield return new WaitUntil(() => theDial.nextDialogue);
        }
        theAudio.StopBGM();
        yield return new WaitForSeconds(3f);
        theAudio.PlaySFX(bellSound);
        yield return new WaitForSeconds(7.5f);
        theDial.ShowText(afterTrueEndingDials);
        yield return new WaitUntil(() => theDial.nextDialogue);
        yield return new WaitForSeconds(0.5f);
    }
}
