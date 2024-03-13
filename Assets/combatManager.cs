using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatManager : MonoBehaviour
{
    public static combatManager Instance;

    public int enemyCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;

        if (enemyCount == 0)
        {
            CombatCleared();
        }
    }

    public void CombatCleared()
    {


        Debug.Log("enemies all clear");

        OverworldMapManager.Instance.TransitionBackToMap();
    }
}
