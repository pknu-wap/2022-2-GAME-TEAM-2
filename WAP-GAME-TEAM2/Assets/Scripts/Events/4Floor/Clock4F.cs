using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock4F : DefaultEvent
{
    public string[] afterEventDial;

    public Vector2 afterEventPos;
    protected override void SwitchCheck()
    {
        if (theEvent.switches[(int)theSwitch])
        {
            Dial = afterEventDial;
            gameObject.transform.position = afterEventPos;
            transform.eulerAngles = new Vector3(0f, 0f, 270f);
        }
    }
}
