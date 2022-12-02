using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveText : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(TextCoroutine());
    }

    IEnumerator TextCoroutine()
    {
        SLManager.instance.Save();
        AudioManager.instance.PlaySFX("Detect");
        yield return new WaitForSeconds(2f);
        Menu.instance.otherActivated = false;
        gameObject.SetActive(false);
    }
}
