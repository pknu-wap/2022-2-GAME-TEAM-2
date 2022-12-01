using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.AnalogGlitch;

public class MusicRoomEvent : DefaultEvent
{
    public GameObject key;
    public GameObject myPiano;
    public GameObject[] peoples;
    public MyGlitch myGlitch;
    
    public string applauseSound;
    public string ghostSound;
    public string[] glitchSounds;

    public float speed; // 카메라 이동 속도
    public float target_y; // 카메라가 움직일 목표 지점
    
    private Light2D playerLight;


    protected override void SwitchCheck()
    {
        playerLight = PlayerController.instance.flashLight;
    }

    protected override IEnumerator ExtraEventCo()
    {
        theEvent.isWorking = true;
        myPiano.SetActive(true);
        yield break;
    }
    

    public IEnumerator MusicEventCo()
    {
        EventManager.instance.switches[(int)SwitchType.MusicRoomEvent] = true;
        EventManager.instance.isEventIng = true;
        AudioManager.instance.PlaySFX(applauseSound);
        yield return new WaitForSeconds(6.5f);

        myPiano.SetActive(false);
        playerLight.gameObject.SetActive(false);
        StartCoroutine(myGlitch.myGlitchCo(0.5f, glitchSounds[0]));
        AudioManager.instance.PlaySFX(ghostSound);
        CameraManager.instance.isFollow = false;
        for (int i = 0; i < peoples.Length; i++)
            peoples[i].SetActive(true);

        StartCoroutine(myGlitch.MusicGlitchCo(glitchSounds));
        while (CameraManager.instance.transform.position.y >= target_y)
        {
            float camYPos = CameraManager.instance.transform.position.y - (speed * Time.deltaTime);
            CameraManager.instance.transform.position = new Vector3(CameraManager.instance.transform.position.x, 
                camYPos, CameraManager.instance.transform.position.z);
            yield return null;
        }

        CameraManager.instance.isFollow = true;
        yield return new WaitForSeconds(0.5f); 
        StartCoroutine(myGlitch.myGlitchCo(0.5f, glitchSounds[0]));
        playerLight.gameObject.SetActive(true);
        for (int i = 0; i < peoples.Length; i++)
            peoples[i].SetActive(false);
        AudioManager.instance.Stop(ghostSound);
        AudioManager.instance.PlayBGM("2Floor");
        EventManager.instance.isEventIng = false;
        PlayerController.instance.IsPause = false;
        theEvent.isWorking = false;
        key.SetActive(true);
    }
}
