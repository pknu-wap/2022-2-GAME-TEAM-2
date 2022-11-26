using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObj : MovingObject
{
    private EventManager theEvent;
    private AudioManager theAudio;
    
    private bool isCollision;

    public string pushSound;

    public ArtRoomEvent are;
    void Start()
    {
        theEvent = EventManager.instance;
        theAudio = AudioManager.instance;
    }

    void Update()
    {
        if (!isCollision || !CanPlayerInteract() || theEvent.isEventIng) return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            vector = PlayerController.instance.GetVector();
            StartCoroutine(MoveCo());
        }
        
    }
    
    IEnumerator MoveCo()
    {
        theEvent.isEventIng = true;
        PlayerController.instance.IsPause = true;
        bool checkCollisionFlag = CheckCollision();
        if (!checkCollisionFlag)
        {
            theAudio.PlaySFX(pushSound);
            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * speed, vector.y * speed, 0);

                currentWalkCount++;
           
                yield return new WaitForSeconds(0.005f);
            }
        }
        are.CheckResult();
        theEvent.isEventIng = false;
        PlayerController.instance.IsPause = false;
        currentWalkCount = 0;
    }

    
    bool CanPlayerInteract()
    {
        Vector2 vector = PlayerController.instance.GetVector();

        Vector2 start = PlayerController.instance.transform.position;
        Vector2 end = start + new Vector2(vector.x, vector.y);

        RaycastHit2D hit;

        hit = Physics2D.Linecast(start, end, layerMask);

        if (!hit)
            return false;

        if (hit.collider.gameObject == this.gameObject)
            return true;
        
        return false;
    }
    
    protected void OnTriggerEnter2D(Collider2D col)
    {
        isCollision = true;
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        isCollision = false;
    }

}
