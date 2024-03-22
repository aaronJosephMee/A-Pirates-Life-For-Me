using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int Ammo = 1000000;

    public float Health = 100.0f;

    void Update()
    {
        ShipMovement();
        shoot();
        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
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

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.5f);
        if (Ammo < 6)
        {
            Ammo++;
            cannonSound.PlayOneShot(ReloadSFX);
        }
        else
        {
            Debug.Log("Cannot reload over 6");
        }

    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;

        if (Health < 0) Invoke(nameof(DestroyShip), 1.0f);
    }

    private void DestroyShip()
    {
        Destroy(gameObject);
    }
}
