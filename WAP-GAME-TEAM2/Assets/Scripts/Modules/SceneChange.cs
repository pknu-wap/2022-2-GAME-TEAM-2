using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
   void Start()
   {
        if (PlayerController.instance.isSceneChange)
        {
            FadeManager.instance.FadeIn();
            PlayerController.instance.isSceneChange = false;
            PlayerController.instance.IsPause = false;

            SpawnManager.instance.StartSpawnCoroutine();
        }

    }
}
