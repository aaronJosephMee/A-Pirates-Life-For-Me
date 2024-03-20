using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] float currentHealth, maxHealth = 100f;

    Ragdoll ragdoll;
    public bool isDead;
    public CharacterAiming aiming;

    meleeHitbox meleeWeapon;
    CameraManager cameraManager;
    WeaponManager weaponManager;

    [SerializeField] public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        aiming = GetComponent<CharacterAiming>();
        cameraManager = FindObjectOfType<CameraManager>();
        meleeWeapon = GetComponentInChildren<meleeHitbox>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        StartCoroutine(PollRelics());
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentHealth / maxHealth;
    }
    
    public void takeDamage(float damage)
    {
        this.currentHealth -= damage;
        ItemManager.instance.OnTakeDamage();
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }



    private void Die()
    {
        isDead = true;

        ragdoll.ActivateRagdoll();
        aiming.enabled = false;

        cameraManager.EnableKillCam();
    }
    private IEnumerator PollRelics(){
        ItemStats newStats = ItemManager.instance.TotalStats();
        // TODO: Make it not call everything if nothing is changed
        weaponManager.damage = newStats.gunDamage;
        weaponManager.bulletsPerShot = newStats.bulletCount;
        weaponManager.fireRate = weaponManager.baseFireRate * newStats.fireRate;
        meleeWeapon.swordDamage = newStats.swordDamage;

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(PollRelics());
    }
}
