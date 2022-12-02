using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        FadeManager.instance.FadeIn();
        PlayerController.instance.isSceneChange = false;
        theEvent = EventManager.instance;
        theAudio = AudioManager.instance;
        theDial = DialogueManager.instance;

        theAudio.StopBGM();

        if (theEvent.switches[(int)trueEnd])
            StartCoroutine(TrueEndCoroutine());
        else
            StartCoroutine(BadEndCoroutine());

    }

    IEnumerator BadEndCoroutine()
    {
        theEvent.SetEvent(true);
        yield return new WaitForSeconds(4f);
        theDial.ShowText(BadEndDials);
        yield return new WaitUntil(() => theDial.nextDialogue);
        theAudio.PlaySFX(fallDownSound);
        yield return new WaitForSeconds(2f);
        theAudio.PlaySFX(bellSound);
        yield return new WaitForSeconds(7.5f);
        theDial.ShowText(afterBadEndingDials);
        yield return new WaitUntil(() => theDial.nextDialogue);
        yield return new WaitForSeconds(3f);
        theEvent.SetEvent(false);
        theAudio.PlayBGM("Title");
        SceneManager.LoadScene("Title");
    }
    
    IEnumerator TrueEndCoroutine()
    {
        theEvent.SetEvent(true);
        for (int i = 0; i < 2; i++)
        {
            theDial.ShowText(TrueEndDials[i]);
            yield return new WaitUntil(() => theDial.nextDialogue);
        }
        yield return new WaitForSeconds(1f);
        theAudio.PlaySFX(doorOpenSound);
        yield return new WaitForSeconds(2f);
        theAudio.PlayBGM(trueEndBGM);
        for (int i = 2; i < TrueEndDials.Length; i++)
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
        yield return new WaitForSeconds(3f);
        theEvent.SetEvent(false);
        SceneManager.LoadScene("Title");
    }
}
