using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeaGameManager : MonoBehaviour
{
    public static SeaGameManager instance = null;

    public GameObject Player;

    public int enemyCount = 10;

    public int Killed = 0;

    public int Spawned = 1;

    public TextMeshProUGUI winningText;

    public TextMeshProUGUI howToPlayText;

    public bool stopCombat;
    public void Start()
    {
        stopCombat = true;
        winningText.gameObject.SetActive(false);
        howToPlayText.gameObject.SetActive(true);
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            stopCombat = false;
            howToPlayText.gameObject.SetActive(false);
        }
        
        if (Killed == enemyCount && !winningText.gameObject.activeSelf)
        {
            StartCoroutine(EndGameRoutine());
        }
    }

    private IEnumerator EndGameRoutine()
    {
        winningText.gameObject.SetActive(true);
        stopCombat = true;
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
