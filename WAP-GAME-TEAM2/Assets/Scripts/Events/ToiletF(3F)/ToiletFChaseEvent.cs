using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletFChaseEvent : MonoBehaviour
{
    private EventManager theEvent;
    [SerializeField] private ToiletFKeyEvent theKeyEvent;

    private bool isCollision;
    private AIController chaser;
    
    // Start is called before the first frame update
    void Start()
    {
        theEvent = EventManager.instance;
        if (theEvent.switches[(int)SwitchType.ToiletFChaseEvent])
        {
            gameObject.SetActive(false);
        }

        isCollision = false;
        chaser = FindObjectOfType<AIController>();
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
        if (isCollision)
        {
            if (Input.GetKeyDown(KeyCode.Z) && PlayerController.instance.GetAnimator().GetFloat("DirX") == 1f)
                StartCoroutine(ChaseCoroutine());
        }
    }

    IEnumerator ChaseCoroutine()
    {
        yield return new WaitForSeconds(1f);

        if (!theEvent.switches[(int)SwitchType.ToiletFChaseEvent])
            theEvent.switches[(int)SwitchType.ToiletFChaseEvent] = true;

        theKeyEvent.gameObject.SetActive(true);

        chaser.chase = true;

        gameObject.SetActive(false);
    }
}
