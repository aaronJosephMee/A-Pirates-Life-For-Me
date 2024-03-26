using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] float currentHealth, maxHealth = 100f;
    [SerializeField] float speedDenominator = 10f;
    Ragdoll ragdoll;
    public bool isDead;
    public CharacterAiming aiming;
    Animator anim;
    meleeHitbox meleeWeapon;
    CameraManager cameraManager;
    WeaponManager weaponManager;
    float toHeal = 0;
    [SerializeField] public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        aiming = GetComponent<CharacterAiming>();
        cameraManager = FindObjectOfType<CameraManager>();
        meleeWeapon = GetComponentInChildren<meleeHitbox>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        anim = GetComponent<Animator>();
        StartCoroutine(PollRelics());
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentHealth / maxHealth;
        if (toHeal > 0 && currentHealth < maxHealth){
            currentHealth += toHeal * Time.deltaTime;
            if (currentHealth > maxHealth){
                currentHealth = maxHealth;
            }
        }
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
        StartCoroutine(ReturnToTitle());
    }

    private IEnumerator ReturnToTitle()
    {
        yield return new WaitForSeconds(2);
        OverworldMapManager.Instance?.MarkMapForReset();
        GameManager.instance.LoadScene(SceneName.TitleScreen);
    }
    private IEnumerator PollRelics(){
        ItemStats newStats = ItemManager.instance.TotalStats();
        // TODO: Make it not call everything if nothing is changed
        weaponManager.damage = newStats.gunDamage;
        weaponManager.bulletsPerShot = newStats.bulletCount;
        weaponManager.fireRate = weaponManager.baseFireRate * newStats.fireRate;
        weaponManager.critChance = newStats.critChance;
        weaponManager.critMultiplier = newStats.critMultiplier;
        meleeWeapon.swordDamage = newStats.swordDamage;
        meleeWeapon.critChance = newStats.critChance;
        meleeWeapon.critMultiplier = newStats.critMultiplier;
        toHeal = newStats.hpRegen;
        anim.SetFloat("Speed", newStats.speedBoost/speedDenominator);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(PollRelics());
    }
}
