using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveText : MonoBehaviour
{
    public float speed;
    public Text text;
    void OnEnable()
    {
        StartCoroutine(TextCoroutine());
    }

    IEnumerator TextCoroutine()
    {
        SLManager.instance.Save();
        AudioManager.instance.PlaySFX("Detect");
        while (text.color.a > 0f)
        {
            float alpha = text.color.a;
            alpha -= speed * Time.deltaTime;
            if (alpha < 0) alpha = 0;
            text.color = new Color(text.color.r,text.color.g,text.color.b, alpha);
            yield return null;
        }
        text.color = new Color(text.color.r,text.color.g,text.color.b, 255f);
        Menu.instance.otherActivated = false;
        gameObject.SetActive(false);
    }
}
