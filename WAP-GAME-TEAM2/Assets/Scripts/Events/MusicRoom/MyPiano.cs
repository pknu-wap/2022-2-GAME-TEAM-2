using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MyPiano : MonoBehaviour
{
    private int nowIndex;
    private int maxIndex;
    
    public string[] sheet;
    private string[] ans = new string[25];
    
    public MusicRoomEvent mre;
    private void OnEnable()
    {
        nowIndex = 0;
        maxIndex = 25;
        PlayerController.instance.IsPause = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerController.instance.IsPause = false;
            gameObject.SetActive(false);
        }
            
        
    }

    public void PlayMelody(string _melody)
    {
        if (EventManager.instance.isEventIng) return;
        AudioManager.instance.PlaySFX(_melody);
        
        if (EventManager.instance.switches[(int)SwitchType.MusicRoomEvent]) return;
        ans[nowIndex] = _melody;
        CheckAnswer();
    }

    private void CheckAnswer()
    {
        if (ans[nowIndex] == sheet[nowIndex])
            nowIndex++;
        else
            nowIndex = 0;

        if (nowIndex == maxIndex)
        {
            nowIndex = 0;
            mre.StartCoroutine(mre.MusicEventCo());
        }
            
    }

    
}
