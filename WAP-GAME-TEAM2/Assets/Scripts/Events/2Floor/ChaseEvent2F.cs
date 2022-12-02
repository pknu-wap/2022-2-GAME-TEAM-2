using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEvent2F : MonoBehaviour
{
    public AIController chaser;

    private void Start()
    {
        if (EventManager.instance.switches[(int)SwitchType.ChaseEvent2F])
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EventManager.instance.switches[(int)SwitchType.MusicRoomEvent] && !EventManager.instance.switches[(int)SwitchType.ChaseEvent2F])
            StartCoroutine(ChaseCoroutine());
    }

    IEnumerator ChaseCoroutine()
    {
        if (EventManager.instance.switches[(int)SwitchType.MusicRoomEvent])
        {
            EventManager.instance.switches[(int)SwitchType.ChaseEvent2F] = true;
            SpawnManager.instance.sceneName = "2FloorRight";
            SpawnManager.instance.spawnPoint = new Vector2(-2.5f, -3.5f);
            SpawnManager.instance.StartChase(chaser);
            SpawnManager.instance.chaserNumber = 1;

            chaser.transform.position = SpawnManager.instance.spawnPoint;
            chaser.gameObject.SetActive(true);

            AudioManager.instance.PlaySFX("DoorClose");

            yield return new WaitForSeconds(0.5f);
            chaser.chase = true;
        }
    }
}
