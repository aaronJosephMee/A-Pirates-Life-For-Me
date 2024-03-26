using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeaGameManager : MonoBehaviour
{
    public static SeaGameManager instance = null;

    public GameObject Player;

    public int enemyCount = 5;

    public int Killed = 0;

    public int Spawned = 0;

    public TextMeshProUGUI winningText;

    public bool stopCombat = false;
    public void Start()
    {
        winningText.gameObject.SetActive(false);
    }
    private void Awake()
    {
        if (instance == null )
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Killed == enemyCount && !winningText.gameObject.activeSelf)
        {
            StartCoroutine(EndGameRoutine());
        }
    }

    private IEnumerator EndGameRoutine()
    {
        winningText.gameObject.SetActive(true);
        SeaGameManager.instance.stopCombat = true;
        ItemManager.instance.AddGold(100);
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1.0f;
        OverworldMapManager.Instance.TransitionBackToMap();
    }

    public void AddEnemySpawned()
    {
        Spawned++;
    }
    public void AddPoint(int add)
    {
        Killed += add;
    }
}
