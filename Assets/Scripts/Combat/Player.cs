using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using Unity.VisualScripting;
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
    ItemStats newStats;
    float toHeal = 0;
    [SerializeField] Slider slider;
    [SerializeField] GameObject dmgNumb;
    [SerializeField] Color hitColor;
    [SerializeField] Color dodgeColor;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        aiming = GetComponent<CharacterAiming>();
        cameraManager = FindObjectOfType<CameraManager>();
        meleeWeapon = GetComponentInChildren<meleeHitbox>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        anim = GetComponent<Animator>();
        currentHealth = ItemManager.instance.GetHealth().curHealth;
        maxHealth = ItemManager.instance.GetHealth().maxHealth;
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
        float damageToTake;
        if (new System.Random().NextDouble() < newStats.dodgeChance){
            damageToTake = 0;
        }
        else if (newStats.defense >= damage){
            damageToTake = 1;
        }
        else{
            damageToTake = damage - newStats.defense;
        }
        damageToTake = MathF.Round(damageToTake,2);
        this.currentHealth -= damageToTake; 
        GameObject dmgNumber = Instantiate(dmgNumb, slider.transform);
        if (damageToTake == 0){
            dmgNumb.GetComponent<PlayerDamageNumbers>().SetText("DODGE", dodgeColor);
        }
        else{
            dmgNumb.GetComponent<PlayerDamageNumbers>().SetText("-" + damageToTake, hitColor);
            ItemManager.instance.OnTakeDamage();
        }
        Destroy(dmgNumber);
        Instantiate(dmgNumb, slider.transform);
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
        newStats = ItemManager.instance.TotalStats();
        // TODO: Make it not call everything if nothing is changed
        weaponManager.damage = newStats.gunDamage;
        weaponManager.bulletsPerShot = newStats.bulletCount;
        weaponManager.fireRate = weaponManager.baseFireRate * newStats.fireRate;
        weaponManager.critChance = newStats.critChance;
        weaponManager.critMultiplier = newStats.critMultiplier;
        weaponManager.richochet = newStats.richochet;

        meleeWeapon.swordDamage = newStats.swordDamage;
        meleeWeapon.critChance = newStats.critChance;
        meleeWeapon.critMultiplier = newStats.critMultiplier;
        
        toHeal = newStats.hpRegen;
        anim.SetFloat("Speed", newStats.speedBoost/speedDenominator);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(PollRelics());
    }
    public void UpdateHealth(){
        ItemManager.instance.SetHealth(currentHealth, maxHealth);
    }
}
