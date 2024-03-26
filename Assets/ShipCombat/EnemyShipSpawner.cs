using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    public GameObject enemy;

    private float spawnTimer = 5;

    private void Update()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(spawnTimer);

        Instantiate(enemy,transform.position,transform.rotation);

        SeaGameManager.instance.AddEnemySpawned();
    }
}
