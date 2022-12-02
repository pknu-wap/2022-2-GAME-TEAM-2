using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitKeyEvent : MonoBehaviour
{
    public GameObject ExitKey;
    void Start()
    {
        if (EventManager.instance.switches[(int)SwitchType.ExitKeyEvent]) return;
        if (EventManager.instance.switches[(int)SwitchType.BadEnding])
            ExitKey.SetActive(true);
    }

}
