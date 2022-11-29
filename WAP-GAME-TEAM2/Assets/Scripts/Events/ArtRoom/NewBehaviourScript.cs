using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private LookArtEvent _lookArtEvent;
    // Start is called before the first frame update
    void Start()
    {
        _lookArtEvent = GameObject.Find("Art").GetComponent<LookArtEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_lookArtEvent.getStateStandingInfrontImage())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_lookArtEvent.getStateLookArt())
                {
                    _lookArtEvent.HideImage();
                }
                else
                {
                    _lookArtEvent.ShowImage();
                }
            }
        }

    }
}
