using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEvent4F : MonoBehaviour
{
    public AIController chaser;

    private void Start()
    {
        if (EventManager.instance.switches[(int)SwitchType.ChaseEvent4F])
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (InventoryManager.instance.SearchItem("알코올램프") && InventoryManager.instance.SearchItem("과산화수소"))
            StartCoroutine(ChaseCoroutine());
    }

    IEnumerator ChaseCoroutine()
    {
        if (InventoryManager.instance.SearchItem("알코올램프") && InventoryManager.instance.SearchItem("과산화수소") && !EventManager.instance.switches[(int)SwitchType.ChaseEvent4F])
        {
            EventManager.instance.switches[(int)SwitchType.ChaseEvent4F] = true;
            SpawnManager.instance.StartChase(chaser);
            SpawnManager.instance.chaserNumber = 3;

            chaser.transform.position = SpawnManager.instance.spawnPoint;
            chaser.gameObject.SetActive(true);

            
            AudioManager.instance.PlaySFX("DoorClose");

            yield return new WaitForSeconds(0.5f);
            chaser.chase = true;
        }
    }

}
