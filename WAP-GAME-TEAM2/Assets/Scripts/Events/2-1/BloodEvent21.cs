using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEvent21 : MonoBehaviour
{
    public GameObject[] bloodObjects;
    public BoxCollider2D[] colliders;
    public string bloodSound;
    
    
    void Start()
    {
        if (EventManager.instance.switches[(int)SwitchType.BloodEvent21])
        {
            for (int i = 0; i < bloodObjects.Length; i++)
                bloodObjects[i].SetActive(true);
            this.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        AudioManager.instance.PlaySFX(bloodSound);
        if (bloodObjects[1].activeInHierarchy)
        {
            bloodObjects[2].SetActive(true);
            colliders[2].enabled = false;
            EventManager.instance.switches[(int)SwitchType.BloodEvent21] = true;
            
        }
        else if (bloodObjects[0].activeInHierarchy)
        {
            bloodObjects[1].SetActive(true);
            colliders[1].enabled = false;
        }
        else
        {
            bloodObjects[0].SetActive(true);
            colliders[0].enabled = false;
        }
            
            
    }
}
