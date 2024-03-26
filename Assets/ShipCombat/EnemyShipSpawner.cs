using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    public GameObject enemy;

    private float spawnTimer = 5;
    private bool spawnedEnemy = false;

    private void Update()
    {
        if (!spawnedEnemy)
        {
            StartCoroutine(EnemySpawn());
            Instantiate(enemy,transform.position,transform.rotation);
            SeaGameManager.instance.AddEnemySpawned();
            spawnedEnemy = true;
        }
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(spawnTimer);

        Instantiate(enemy,transform.position,transform.rotation);

        SeaGameManager.instance.AddEnemySpawned();
    }
}
