using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;


public class combatManager : MonoBehaviour
{
    public static combatManager Instance;
    public GameObject itemDrop;
    public int enemyCount;
    public GameObject playerObject;

    public Animator playerAnim;
    public CharacterLocomotion playerMove;
    public CharacterAiming playerAim;
    public WeaponManager playerGun;
    public MeleeAttack playerSword;
    CameraManager cameraManager;
    public bool waveCleared = false;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject gun;

    [SerializeField] public List<EnemyCount> enemiesToSpawn;
    private List<Transform> spawners;

    void Start()
    {

        waveCleared = false;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        playerAnim = playerObject.GetComponent<Animator>();

        playerMove = playerObject.GetComponent<CharacterLocomotion>();

        playerAim = playerObject.GetComponent<CharacterAiming>();

        playerGun = playerObject.GetComponentInChildren<WeaponManager>();

        playerSword = playerObject.GetComponentInChildren<MeleeAttack>();

        cameraManager = FindObjectOfType<CameraManager>();

        spawners = new List<Transform>();
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawnerObjects)
        {
            spawners.Add(spawner.transform);
        }

        SpawnEnemies();
    }

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
    }

    public void SpawnEnemies()
    {
        foreach (Transform spawner in spawners)
        {
            foreach (EnemyCount enemyCount in enemiesToSpawn)
            {
                StartCoroutine(SpawnEnemy(enemyCount, spawner));
            }
        }
    }

    IEnumerator SpawnEnemy(EnemyCount enemyCount, Transform spawnLocation)
    {
        for (int i = 0; i < enemyCount.numToSpawn; i++)
        {
            Vector3 position = spawnLocation.position;
            Instantiate(enemyCount.EnemyPrefab,
                new Vector3(position.x + Random.Range(-25, 25), position.y, position.z + Random.Range(-25, 25)),
                transform.rotation);
            //Instantiate(enemyCount.EnemyPrefab, new Vector3(spawnLocation.x + Random.Range(-25, 25), spawnLocation.y, spawnLocation.z + Random.Range(-25, 25)), Quaternion.identity);


            this.enemyCount++;
            yield return new WaitForSeconds(8);
        }
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;

        if (enemyCount <= 0 && !IsInvoking("SpawnEnemies"))
        {
            CombatCleared();
        }
    }

    public void CombatCleared()
    {   
        waveCleared = true;
        sword.SetActive(false);
        gun.SetActive(false);
        cameraManager.EnableWinCam();
        playerAnim.SetBool("clearedWave", true);
        
        StartCoroutine(DelayedCombatClear());
    }

    IEnumerator DelayedCombatClear()
    {
        ItemManager.instance.AddGold(100);
        yield return new WaitForSeconds(2f);

        
        Debug.Log("enemies all clear");
        //OverworldMapManager.Instance.TransitionBackToMap();
        

        playerMove.enabled = false;
        playerGun.enabled = false;
        playerSword.enabled = false;
        playerAim.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        yield return new WaitForSeconds(4f);
        Instantiate(itemDrop);
    }
}
