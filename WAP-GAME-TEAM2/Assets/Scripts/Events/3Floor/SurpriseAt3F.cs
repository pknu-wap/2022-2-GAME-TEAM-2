using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseAt3F : MonoBehaviour
{
    public string surpriseSound;
    public GameObject dialEvent;
    public GameObject[] objSprites1;
    public GameObject[] objSprites2;
    void Start()
    {
        if (EventManager.instance.switches[(int)SwitchType.SurpriseAt3F])
        {
            foreach (var i in objSprites2)
                i.SetActive(true);
            dialEvent.SetActive(true);
        }
        else
        {
            foreach (var i in objSprites1)
                i.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (EventManager.instance.switches[(int)SwitchType.SurpriseAt3F]) return;
        AudioManager.instance.PlaySFX(surpriseSound);
        transform.eulerAngles = new Vector3(0f, 0f, 60f);
        EventManager.instance.switches[(int)SwitchType.SurpriseAt3F] = true;
    }
}
