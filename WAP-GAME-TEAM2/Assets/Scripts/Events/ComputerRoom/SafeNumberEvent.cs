using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeNumberEvent : MonoBehaviour
{
    private NumberSystem theNumber;
    private EventManager theEvent;
    [SerializeField] private ComputerRoomKeyEvent theKeyEvent;

    [SerializeField] private int correctNumber;

    private bool isCollision;
    private bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        theEvent = EventManager.instance;
        if (theEvent.switches[(int)SwitchType.SafeNumberEvent])
        {
            gameObject.SetActive(false);
        }

        theNumber = FindObjectOfType<NumberSystem>();

        isCollision = false;
        isInteracting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollision = false;
        }
    }

    private void Update()
    {
        if (isCollision && !isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.Z) && PlayerController.instance.GetAnimator().GetFloat("DirY") == 1f)
            {
                StartCoroutine(NumberCoroutine());
            }
        }
    }

    IEnumerator NumberCoroutine()
    {
        PlayerController.instance.IsPause = true;
        isInteracting = true;

        yield return new WaitForSeconds(0.1f);

        theNumber.ShowNumber(correctNumber);        // 정답 넘겨주기 
        yield return new WaitUntil(() => !theNumber.activeated);

        if (theNumber.GetResult())
        {
            if (!theEvent.switches[(int)SwitchType.SafeNumberEvent])
                theEvent.switches[(int)SwitchType.SafeNumberEvent] = true;

            theKeyEvent.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        PlayerController.instance.IsPause = false;
        isInteracting = false;
    }
}
