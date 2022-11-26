using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwitchType
{
    StartEvent,
    EntryEventAt3F,         // 3층 처음에 피를 봤을 때의 이벤트
    SurpriseAt3F,           // 액자 연출,         
    CpDoorOpened,           // 컴퓨터실 문 열림
    BcDoorOpened,           // 방송실 문 열림
    StDoorOpened,           // 스튜디오 문 열림
    StFirstEntry,           // 스튜디오 첫 입장
    BcKeyEvent,             // 방송실 키 이벤트
    ToiletFChaseEvent,      // 3층 여자화장실 추격 이벤트
    ToiletFKeyEvent,        // 3층 여자화장실 키 획득 이벤트
    SafeNumberEvent,        // 3층 컴퓨터실 금고 이벤트
    ComputerRoomKeyEvent,   // 3층 컴퓨터실 금고 키 획득 이벤트
    Opened2F,               // 2층 문 열림 이벤트
    AudiovisualRoomChaseEvent,  // 시청각실 추격 이벤트
    AudiovisualRoomLightEvent,  // 시청각실 추격 종료 이벤트
    AudiovisualRoomKeyEvent,    // 시청각실 아이템 획득 이벤트
    TeacherOfficeFileEvent,     // 교무실 촌지 획득 이벤트
    ChaseEvent1F,           // 1층 추격 이벤트
    PoDoorOpend,            // 교장실 문 열림
    PrincipalOfficeNumberEvent, // 교장실 금고 이벤트
    PrincipalOfficeKeyEvent,    // 교장실 키 이벤트
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
    
}
