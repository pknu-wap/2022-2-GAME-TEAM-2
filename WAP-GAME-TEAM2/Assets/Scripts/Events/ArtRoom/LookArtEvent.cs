using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookArtEvent : MonoBehaviour
{
    private bool state;

    private GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        ShowImage();
        image = GameObject.Find("ArtImage");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowImage()
    {
        image.SetActive(true);
    }

    void HideImage()
    {
        image.SetActive(false);
    }
}
