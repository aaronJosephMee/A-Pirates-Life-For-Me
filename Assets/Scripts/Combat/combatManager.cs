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
    public Player playerHealth;
    CameraManager cameraManager;
    public Rig bodyLayer;
    public bool waveCleared = false;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject gun;
    [SerializeField] public int spawnTimer = 8;
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
        playerHealth = playerObject.GetComponent<Player>();

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
            GameObject enemy = Instantiate(enemyCount.EnemyPrefab,
            new Vector3(position.x, position.y, position.z), Quaternion.identity);

            EnemyDamage enemyDamage = enemy.GetComponent<EnemyDamage>();
            if (enemyDamage)
            {
                enemyDamage.damageAmount *= enemyCount.scalingFactor;
            }

            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth)
            {
                enemyHealth.maxHealth *= enemyCount.scalingFactor;
                enemyHealth.currentHealth = enemyHealth.maxHealth; 
            }
            //maybe scalingfactor should be separate for health and damage

            this.enemyCount++;
            yield return new WaitForSeconds(spawnTimer); // enemy respawn timer
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
        playerSword.enabled = false;

        playerAnim.SetBool("clearedWave", true);
        
        StartCoroutine(DelayedCombatClear());
    }

    IEnumerator DelayedCombatClear()
    {
        ItemManager.instance.AddGold(100);
        yield return new WaitForSeconds(0.05f);

        
        Debug.Log("enemies all clear");
        //OverworldMapManager.Instance.TransitionBackToMap();

        
        playerMove.enabled = false;
        playerGun.enabled = false;
        bodyLayer.weight = 0;
        playerAim.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        yield return new WaitForSeconds(6f);
        playerHealth.UpdateHealth();
        if (GameManager.instance.HasPopup())
        {
            GameManager.instance.DisplayStoredPopUp();
        }
        else
        {
            Instantiate(itemDrop);
        }
    }
}
