using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipePuzzle : MonoBehaviour
{
    public GameObject instance;
    public Transform[] tiles;
    public GameObject[] panels;

    public BroadCastDoor door;

    private float[] angle = { 0.0f, 90.0f, 180.0f, 270.0f };
    private int[] curAngleIdx = new int[16]; // 각도를 배열로 관리
    
    private float[] answer =
        { 180.0f, 180.0f, 0.0f, 0.0f, 0.0f, 0.0f, 90.0f, 180.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 90.0f, 0.0f, 90.0f };
    private int curSelectIdx;

    private EventManager theEvent;
    
    public string rotateSound;
    public string PowerOnSound;
    public string DoorOpenSound;

    public string[] Dial;
    void OnEnable()
    {
        theEvent = EventManager.instance;
        PlayerController.instance.gameObject.SetActive(false);
        for (int i = 0; i < tiles.Length; i++)
        {
            int randInt = Random.Range(0, 4);
            tiles[i].eulerAngles = new Vector3(0.0f, tiles[i].eulerAngles.y, angle[randInt]);
            curAngleIdx[i] = randInt;
        }
        curSelectIdx = 0;
        panels[curSelectIdx].SetActive(true);
    }

    void Update()
    {
        if (theEvent.switches[(int)SwitchType.BCDoorOpened]) return;
        
        #region KeyArrow
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            panels[curSelectIdx].SetActive(false);
            if (curSelectIdx - 4 < 0)
                curSelectIdx = (curSelectIdx + tiles.Length) - 4;
            else
                curSelectIdx -= 4;
            panels[curSelectIdx].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            panels[curSelectIdx].SetActive(false);
            if (curSelectIdx + 4 >= tiles.Length)
                curSelectIdx = (curSelectIdx - tiles.Length) + 4;
            else
                curSelectIdx += 4;
            panels[curSelectIdx].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            panels[curSelectIdx].SetActive(false);
            if ((curSelectIdx + 1) % 4 == 0)
                curSelectIdx = (curSelectIdx + 1) - 4;
            else
                curSelectIdx++;
            panels[curSelectIdx].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            panels[curSelectIdx].SetActive(false);
            if (curSelectIdx % 4 == 0)
                curSelectIdx = (curSelectIdx + 4) - 1;
            else
                curSelectIdx--;
            panels[curSelectIdx].SetActive(true);
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.instance.PlaySFX(rotateSound);
            curAngleIdx[curSelectIdx] = (curAngleIdx[curSelectIdx] + 1) % 4;
            tiles[curSelectIdx].eulerAngles = new Vector3(0.0f, tiles[curSelectIdx].eulerAngles.y, angle[curAngleIdx[curSelectIdx]]);
            if (true)//(CheckResult())
            {
                theEvent.isEventIng = true;
                theEvent.switches[(int)SwitchType.BCDoorOpened] = true;
                door.DoorOpen();
                StartCoroutine(ClearCoroutine());
            }
                
        }
        
        else if (Input.GetKeyDown(KeyCode.X))
        {
            panels[curSelectIdx].SetActive(false);
            PlayerController.instance.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

    }

    bool CheckResult()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (i == 3 || i == 9) continue;
            if (angle[curAngleIdx[i]] != answer[i])
                return false;
        }
        return true;
    }

    IEnumerator ClearCoroutine()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.instance.PlaySFX(PowerOnSound);
        yield return new WaitForSeconds(2f);
        PlayerController.instance.gameObject.SetActive(true);
        instance.SetActive(false);
        
        DialogueManager.instance.ShowText(Dial[0]);
        yield return new WaitUntil(() => DialogueManager.instance.nextDialogue == true);
        
        AudioManager.instance.PlaySFX(DoorOpenSound);
        yield return new WaitForSeconds(2f);
        
        DialogueManager.instance.ShowText(Dial[1]);
        theEvent.isEventIng = false;
        gameObject.SetActive(false);
    }
}
