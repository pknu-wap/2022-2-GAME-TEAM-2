using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwitchType
{
    DefaultEvent,           // 조건체크를 위한 디폴트 스위치
    StartEvent,             // 3-1 시작이벤트
    Diary0305,             // 3-1 시작이벤트
    ScrItemEvent1,          // 과학실 알코올램프 획득
    ScrItemEvent2,          // 과학실 과산화수소 획득
    OpenedToilet,           // 4층 남자화장실 문 열림
    Opened32,               // 3 - 2 문열림
    KeyEvent32,             // 3 - 2 키획득
    OpenedArtRoom,          // 미술실 문열림
    ArtRoomEvent,           // 미술실 이벤트
    Opened3F,               // 3층 문 열림
    EntryEventAt3F,         // 3층 처음에 피를 봤을 때의 이벤트
    SurpriseAt3F,           // 액자 연출,
    Opened21,               // 2 - 1 문 열림
    BloodEvent21,           // 2 - 1 연출 
    KeyEvent21,             // 2 - 1 키 이벤트
    CpDoorOpened,           // 컴퓨터실 문 열림
    Opened22,               // 2 - 2 문 열림
    Event22,                // 2 - 2 책상 이벤트
    SafeEvent22,            // 2 - 2 금고 이벤트
    BcDoorOpened,           // 방송실 문 열림
    StDoorOpened,           // 스튜디오 문 열림
    StFirstEntry,           // 스튜디오 첫 입장
    BcKeyEvent,             // 방송실 키 이벤트
    ToiletFChaseEvent,      // 3층 여자화장실 추격 이벤트
    ToiletFKeyEvent,        // 3층 여자화장실 키 획득 이벤트
    SafeNumberEvent,        // 3층 컴퓨터실 금고 이벤트
    ComputerRoomKeyEvent,   // 3층 컴퓨터실 금고 키 획득 이벤트
    Opened2F,               // 2층 문 열림 이벤트
    MusicRoomEvent,         // 2층 음악실 이벤트
    KeyEvent11,             // 1 - 1 키 이벤트
    DiaryEvent11,           // 1 - 1 일기장 획득
    GhostEvent11,           // 2층 유령 이벤트
    AudiovisualRoomChaseEvent,  // 시청각실 추격 이벤트
    AudiovisualRoomLightEvent,  // 시청각실 추격 종료 이벤트
    AudiovisualRoomKeyEvent,    // 시청각실 아이템 획득 이벤트
}

// 게임에서 일어나는 이벤트를 관리해주는 클래스 (이벤트가 한번만 일어나게 하기 위함)
// 이벤트가 추가될때마다 enum 값 추가 할 것.
public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public bool[] switches;
    public bool[] diaryObtained;
    public bool isEventIng = false;
    public bool isWorking;
    
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

    public void SetEvent(bool _b)
    {
        isEventIng = _b;
        PlayerController.instance.IsPause = _b;
    }
    
}
