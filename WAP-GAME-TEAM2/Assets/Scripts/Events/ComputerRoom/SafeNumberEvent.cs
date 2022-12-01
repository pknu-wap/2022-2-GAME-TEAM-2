using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeNumberEvent : MonoBehaviour
{
    private NumberSystem theNumber;
    private EventManager theEvent;
    [SerializeField] private ItemEvent theKeyEvent;

    [SerializeField] private int correctNumber;

    public SwitchType numberEventSwitch;
    public SwitchType keyEventSwitch;

    public string dir;
    public float val;

    private bool isCollision;
    private bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        theEvent = EventManager.instance;
        if (EventManager.instance.switches[(int)numberEventSwitch])
        {
            gameObject.SetActive(false);

            if (!EventManager.instance.switches[(int)keyEventSwitch])
            {
                theKeyEvent.gameObject.SetActive(true);
                theKeyEvent.spriteObj.SetActive(true);
            }
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
            if (Input.GetKeyDown(KeyCode.Z) && PlayerController.instance.GetPlayerDir(dir) == val)
            {
                StartCoroutine(NumberCoroutine());
            }
        }
    }

    IEnumerator NumberCoroutine()
    {
        PlayerController.instance.IsPause = true;
        isInteracting = true;

        yield return new WaitForSeconds(0.2f);


        theNumber.ShowNumber(correctNumber);        // ���� �Ѱ��ֱ� 
        yield return new WaitUntil(() => !theNumber.activated);
        
        if (theNumber.GetResult())
        {
            if (!EventManager.instance.switches[(int)numberEventSwitch])
                EventManager.instance.switches[(int)numberEventSwitch] = true;

            theKeyEvent.gameObject.SetActive(true);
            theKeyEvent.spriteObj.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        PlayerController.instance.IsPause = false;  
        isInteracting = false;
    }
}
