using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    public GameObject enemy;

    private float spawnTimer = 10;
    private float worldTimer;

    private void Start()
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }

    private void Update()
    {
        if (!SeaGameManager.instance.stopCombat && SeaGameManager.instance.Spawned < SeaGameManager.instance.enemyCount)
        {
            worldTimer += Time.deltaTime;

            if (worldTimer >= spawnTimer)
            {
                worldTimer = 0;

                Instantiate(enemy, transform.position, transform.rotation);

                SeaGameManager.instance.AddEnemySpawned();
            }
        }
    }
}
