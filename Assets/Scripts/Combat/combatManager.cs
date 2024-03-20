using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class combatManager : MonoBehaviour
{
    public static combatManager Instance;
    public GameObject itemDrop;
    private CharacterAiming characterAiming;
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
        //OverworldMapManager.Instance.TransitionBackToMap();
        //Time.timeScale = 0f;
        FindAnyObjectByType<Player>().aiming.enabled = false;
        FindAnyObjectByType<CharacterLocomotion>().enabld = false;
        FindAnyObjectByType<WeaponManager>().enabled = false;
        FindAnyObjectByType<MeleeAttack>().enabld = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Instantiate(itemDrop);
    }
}
