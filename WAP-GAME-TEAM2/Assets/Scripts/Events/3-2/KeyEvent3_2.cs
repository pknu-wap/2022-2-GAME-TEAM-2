using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent3_2 : ItemEvent2
{
    [SerializeField] private AIController chaser;

    protected override IEnumerator ItemEventCo()
    {
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);

        SpawnManager.instance.sceneName = "3-2";
        SpawnManager.instance.chaserNumber = 3;
        SpawnManager.instance.StartChase(chaser);

        AudioManager.instance.PlaySFX("DoorClose");

        chaser.SetNodeArray();
        chaser.gameObject.SetActive(true);
        chaser.chase = true;

        theEvent.SetEvent(false);
        gameObject.SetActive(false);

        yield break;
    }
}
