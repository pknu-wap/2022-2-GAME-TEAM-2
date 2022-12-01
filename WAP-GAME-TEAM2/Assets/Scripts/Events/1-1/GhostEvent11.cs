using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEvent11 : MonoBehaviour
{
    public GameObject ghostSprite;
    public string scarySound;
    public string laughingSound;
    void Start()
    {
        if (EventManager.instance.switches[(int)SwitchType.GhostEvent12])
        {
            gameObject.SetActive(false);
        }

        EventManager.instance.switches[(int)SwitchType.KeyEvent12] = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (EventManager.instance.switches[(int)SwitchType.GhostEvent12]) return;
        EventManager.instance.switches[(int)SwitchType.GhostEvent12] = true;
        StartCoroutine(GhostEventCo());
    }

    private IEnumerator GhostEventCo()
    {
        EventManager.instance.isEventIng = true;
        PlayerController.instance.IsPause = true;
        AudioManager.instance.PlaySFX(scarySound);
        PlayerController.instance.SetBalloonAnim();
        ghostSprite.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        AudioManager.instance.PlaySFX(laughingSound);
        yield return new WaitForSeconds(1.5f);
        EventManager.instance.isEventIng = false;
        PlayerController.instance.IsPause = false;
        gameObject.SetActive(false);
    }
}
