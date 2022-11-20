using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwitchType
{
    StartEvent,
    ToiletFChaseEvent,      // 3층 여자화장실 추격 이벤트
    ToiletFKeyEvent,        // 3층 여자화장실 키 획득 이벤트
    SafeNumberEvent,        // 3층 컴퓨터실 금고 이벤트
    ComputerRoomKeyEvent,   // 3층 컴퓨터실 금고 키 획득 이벤트
}

// 게임에서 일어나는 이벤트를 관리해주는 클래스 (이벤트가 한번만 일어나게 하기 위함)
// 이벤트가 추가될때마다 enum 값 추가 할 것.
public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public bool[] switches;
    public bool isEventIng = false;
    

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    

    private void Start()
    {
        
    }
}
