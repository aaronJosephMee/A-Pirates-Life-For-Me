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
    public bool waveCleared = false;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject gun;

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
        playerHealth.UpdateHealth();
        Instantiate(itemDrop);
    }
}
