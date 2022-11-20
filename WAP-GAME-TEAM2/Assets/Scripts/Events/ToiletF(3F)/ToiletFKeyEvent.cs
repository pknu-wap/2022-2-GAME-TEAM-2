using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletFKeyEvent : MonoBehaviour
{
    private EventManager theEvent;

    private bool isCollision;

    // Start is called before the first frame update
    void Start()
    {
        theEvent = EventManager.instance;
        if (theEvent.switches[(int)SwitchType.ToiletFKeyEvent])
        {
            gameObject.SetActive(false);
        }

        isCollision = false;
        gameObject.SetActive(false);
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
            if (Input.GetKeyDown(KeyCode.Z) /*&& PlayerController.instance.GetAnimator().GetFloat("DirX") == 1f*/)
            {
                if (!theEvent.switches[(int)SwitchType.ToiletFKeyEvent])
                    theEvent.switches[(int)SwitchType.ToiletFKeyEvent] = true;

                InventoryManager.instance.GetItem("2-1¹Ý");

                gameObject.SetActive(false);
            }
        }
    }
}
