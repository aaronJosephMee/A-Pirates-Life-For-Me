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
    [SerializeField] public GameObject bullet;
    [SerializeField] public float bulletVelocity;
    [SerializeField] Transform barrelPos;
    [SerializeField] public int bulletsPerShot;
    public float damage = 25;
    public float critChance;
    public float critMultiplier;
    public int richochet;
    [SerializeField] public Vector3 bulletSize = Vector3.one;


    public AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;

    public ParticleSystem muzzleFlash;

    CharacterAiming aim;


    [SerializeField] Transform raycastOrigin;
    public Transform raycastDesination;
    Ray ray;
    RaycastHit hitInfo;

    public WeaponRecoil recoil;
    [System.NonSerialized] public bool enabld = true;

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

        ParticleSystem muzzleFlashInstance = Instantiate(muzzleFlash, raycastOrigin.position, raycastOrigin.rotation);
        muzzleFlashInstance.Play();

        int randomClip = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[randomClip];
        audioSource.Play();

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDesination.position - raycastOrigin.position;
        if(Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
        }

        for(int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            currentBullet.transform.localScale = bulletSize;

            Bullet bulletScript = currentBullet.GetComponent<Bullet>();
            bulletScript.weapon = this;

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }

        recoil.GenerateRecoil();
    }
}
