using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookArtEvent : MonoBehaviour
{
    private bool state_standing_infront_image = false;
    private bool state_look_art = false;

    private GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        image = GameObject.Find("ArtImage");
        HideImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        state_standing_infront_image = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        state_standing_infront_image = false;
        HideImage();
    }

    public bool getStateStandingInfrontImage()
    {
        return state_standing_infront_image;
    }

    public bool getStateLookArt()
    {
        return state_look_art;
    }

    public void ShowImage()
    {
        state_look_art = true;
        image.SetActive(true);
    }

    public void HideImage()
    {
        image.SetActive(false);
        state_look_art = false;
    }
}
