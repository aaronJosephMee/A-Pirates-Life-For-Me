using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] public float fireRate;
    float fireRateTimer;
    [SerializeField] public float baseFireRate;
    [SerializeField] bool semiAuto;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletVelocity;
    [SerializeField] Transform barrelPos;
    [SerializeField] int bulletsPerShot;
    public float damage = 25;
    CharacterAiming aim;


    public bool isFiring = false;

    [SerializeField] Transform raycastOrigin;
    public Transform raycastDesination;
    Ray ray;
    RaycastHit hitInfo;

    public WeaponRecoil recoil;
    [System.NonSerialized] bool enabld = true;

    private void Awake()
    {
        recoil = GetComponent<WeaponRecoil>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        aim = GetComponentInParent<CharacterAiming>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire() && enabld) Fire();
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        Debug.Log("Fire");
        
        ray.origin = raycastOrigin.position;
        ray.direction = raycastDesination.position - raycastOrigin.position;
        if(Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
        }

        for(int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Bullet bulletScript = currentBullet.GetComponent<Bullet>();
            bulletScript.weapon = this;

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }

        recoil.GenerateRecoil();
    }
}
