using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerRoomDialogEvent : MonoBehaviour
{
    private DialogueManager theDial;
    private PlayerController thePlayer;
    private EventManager theEvent;

    [TextArea(1, 2)] [SerializeField]
    private string[] dials;

    private bool isCollision;
    private bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        theDial = DialogueManager.instance;
        thePlayer = PlayerController.instance;
        theEvent = EventManager.instance;

        isCollision = false;
        isInteracting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isCollision = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollision && !isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.Z)) //&& PlayerController.instance.GetAnimator().GetFloat("DirY") == -1f)
                StartCoroutine(DialogueCoroutine());
        }
    }

    IEnumerator DialogueCoroutine()
    {
        isInteracting = true;
        PlayerController.instance.IsPause = true;
        theEvent.isEventIng = true;

        yield return new WaitForSeconds(0.1f);
        theDial.ShowText(dials[0]);
        yield return new WaitUntil(() => theDial.nextDialogue == true);
        yield return new WaitForSeconds(0.1f);
        theDial.ShowText(dials[1]);
        yield return new WaitUntil(() => theDial.nextDialogue == true);

        isInteracting = false;
        PlayerController.instance.IsPause = false;
        theEvent.isEventIng = false;
    }
}
