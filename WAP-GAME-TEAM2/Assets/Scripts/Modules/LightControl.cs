using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    void Start()
    {
        PlayerController.instance.flashLight.intensity = 1f;
        PlayerController.instance.flashLight.gameObject.SetActive(true);
    }

 
}
