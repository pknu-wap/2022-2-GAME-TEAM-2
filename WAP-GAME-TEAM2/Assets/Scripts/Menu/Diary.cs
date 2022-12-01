using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    private AudioManager theAudio;
    private EventManager theEvent;

    public Menu theMenu;
    public GameObject[] diary;
    public GameObject[] focusPanels;

    public string cancelSound;
    public string keySound;

    public int totalCount;
    private int curIndex;

    private bool isShowing;

    void Start()
    {
        theAudio = AudioManager.instance;
        theEvent = EventManager.instance;
        curIndex = 0;
        focusPanels[0].SetActive(true);
    }

    void Update()
    {
        if (isShowing)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                theAudio.PlaySFX(keySound);
                diary[curIndex].SetActive(false);
                isShowing = false;
            }

            return;
        }

        // 돌아가기
        if (Input.GetKeyDown(KeyCode.X))
        {
            theAudio.PlaySFX(keySound);
            theMenu.otherActivated = false;
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            theAudio.PlaySFX(keySound);
            focusPanels[curIndex].SetActive(false);
            if (curIndex == 0)
                curIndex = totalCount - 1;
            else
                curIndex--;
            focusPanels[curIndex].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            theAudio.PlaySFX(keySound);
            focusPanels[curIndex].SetActive(false);
            if (curIndex == totalCount - 1)
                curIndex = 0;
            else
                curIndex++;
            focusPanels[curIndex].SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!theEvent.diaryObtained[curIndex])
            {
                theAudio.PlaySFX(cancelSound);
                return;
            }

            theAudio.PlaySFX(keySound);
            diary[curIndex].SetActive(true);
            isShowing = true;
        }
    }
}
