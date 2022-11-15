using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwitchType
{
    StartEvent,
    
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
