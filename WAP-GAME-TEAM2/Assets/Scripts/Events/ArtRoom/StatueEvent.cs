using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;


public class StatueEvent : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    private void OnTriggerExit2D(Collider2D other)
    {
        Filp();
    }

    void Filp()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
}
