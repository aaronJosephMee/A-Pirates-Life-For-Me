using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipCombat : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 25.0f;

    private Vector3 position;
    [SerializeField] private Vector3 rotate;

    [SerializeField] private float cannonForce = 25.0f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float fireTime;

    public GameObject cannonBall;
    public Transform cannon;

    public AudioSource cannonSound;
    public AudioClip cannonSFX;
    public AudioClip ReloadSFX;
    public GameObject cannonShotVFX;

    public GameObject fire;

    private int Ammo = 1000000;

    public float Health = 100.0f;

    public float MaxHealth = 100.0f;

    public bool PlayerAlive = true;

    public GameObject losingText;

    [SerializeField] Slider healthBar;

    private void Start()
    {
        fire.SetActive(false);
        losingText.gameObject.SetActive(false);
    }

    void Update()
    {
        healthBar.value = Health / MaxHealth;
        if (!SeaGameManager.instance.stopCombat)
        {
            ShipMovement();
            shoot();
        }
    }

    private void ShipMovement()
    {
        //movement
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // rotation
        if (Input.GetKey(KeyCode.A)) rotate = Vector3.down;

        else if (Input.GetKey(KeyCode.D)) rotate = Vector3.up;

        else rotate = Vector3.zero;


        transform.Rotate(rotate * rotateSpeed * Time.deltaTime);
    }

    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > fireTime && Ammo > 0)
        {
            var _cannonBall = Instantiate(cannonBall, cannon.position, cannon.rotation);
            _cannonBall.GetComponent<Rigidbody>().velocity = cannon.forward * cannonForce;

            GameObject effect = Instantiate(cannonShotVFX, cannon.position, cannon.rotation);
            Destroy(effect, 1.5f);

            cannonSound.PlayOneShot(cannonSFX);

            fireTime = Time.time + fireRate;
            Ammo--;
            Debug.Log("Ammo:" + Ammo + "/6");
        }
    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;

        if (Health < 0)
        {
            fire.SetActive(true);
            PlayerAlive = false;
            StartCoroutine(EndGameRoutine());
        }
    }

    private IEnumerator EndGameRoutine()
    {
        losingText.gameObject.SetActive(true);
        SeaGameManager.instance.stopCombat = true;
        ItemManager.instance.AddGold(-100);
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1.0f;
        OverworldMapManager.Instance.TransitionBackToMap();
    }
}
