using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch.Runtime.AnalogGlitch;

public class MyGlitch : MonoBehaviour
{
    private Volume vol;
    private AnalogGlitchVolume glitchVol;
    void Start()
    {
        vol = GetComponent<Volume>();
        glitchVol = vol.profile.components[3] as AnalogGlitchVolume;
    }
    

    public IEnumerator myGlitchCo(float _duration, string glitchSound)
    {
        AudioManager.instance.PlaySFX(glitchSound);
        glitchVol.colorDrift.value = 1f;
        glitchVol.scanLineJitter.value = 1f;
        yield return new WaitForSeconds(_duration);
        
        glitchVol.colorDrift.value = 0f;
        glitchVol.scanLineJitter.value = 0f;
    }

    public IEnumerator MusicGlitchCo(string[] glitchsounds)
    {
        StartCoroutine(myGlitchCo(1f, glitchsounds[1]));
        yield return new WaitForSeconds(2f);
        StartCoroutine(myGlitchCo(3.5f, glitchsounds[2]));
    }
    
}
