using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerRoomKeyEvent : MonoBehaviour
{
    private EventManager theEvent;

    private bool isCollision;

    // Start is called before the first frame update
    void Start()
    {
        theEvent = EventManager.instance;
        if (theEvent.switches[(int)SwitchType.ComputerRoomKeyEvent])
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
            if (Input.GetKeyDown(KeyCode.Z)) //&& PlayerController.instance.GetAnimator().GetFloat("DirY") == 1f)
            {
                InventoryManager.instance.GetItem("2-2��");

                if (!theEvent.switches[(int)SwitchType.ComputerRoomKeyEvent])
                    theEvent.switches[(int)SwitchType.ComputerRoomKeyEvent] = true;

                gameObject.SetActive(false);
            }
        }
    }
}
