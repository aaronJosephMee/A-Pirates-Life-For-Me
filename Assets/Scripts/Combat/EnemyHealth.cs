using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float currentHealth, maxHealth = 100f;
    private bool isDead;

    Ragdoll ragdoll;

    [Header("Visuals")]
    SkinnedMeshRenderer skinnedMeshRenderer;
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;
    public GameObject FloatingText;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip[] gunhitClips;
    public AudioClip[] swordhitSounds;

    combatManager combatManager;
    NavMeshAgent navMeshAgent;
    [SerializeField] private float moveSpeed = 1f;

    [Header("Debuff Types")]
    public bool isIgnitable = false; 
    int fireDuration = 0;
    int poisonGunStacks = 0;
    int freezeGunStacks = 0;
    int poisonMeleeStacks = 0;
    int freezeMeleeStacks = 0;
    [SerializeField] Color normalHit;
    [SerializeField] Color criticalHit;
    [SerializeField] Color fireHit;
    [SerializeField] Color poisonHit;
    [SerializeField] Color freezeHit;
    
    public bool isBoss = false;
    [SerializeField] Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        combatManager = FindObjectOfType<combatManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBoss)
        {
            slider.value = currentHealth / maxHealth;
        }

        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }

    public void DecreaseHealth(float amount, string type, bool wasCrit)
    {
        currentHealth -= amount;
        if (type == "gun")
        {
            if (gunhitClips != null)
            {
                int randomClip = UnityEngine.Random.Range(0, gunhitClips.Length - 1);
                audioSource.clip = gunhitClips[randomClip];
                audioSource.Play();
            }
        }

        if (type == "sword")
        {
            if (swordhitSounds != null)
            {
                int randomClip = UnityEngine.Random.Range(0, swordhitSounds.Length - 1);
                audioSource.clip = swordhitSounds[randomClip];
                audioSource.Play();
            }
        }
        if (type=="gun")
        {
            Dictionary<string, DebuffStats> debuffs = ItemManager.instance.GetGunDebuffs();    
            foreach (KeyValuePair<string, DebuffStats> debuff in debuffs){
                if (debuff.Key == Debuffs.Fire.ToString()){
                    if (fireDuration == 0){
                        fireDuration += debuff.Value.duration;
                        StartCoroutine(OnFire());
                    }
                    else{
                        fireDuration += debuff.Value.duration;
                    }
                }
                if (debuff.Key == Debuffs.Poison.ToString()){
                    if (poisonGunStacks < debuff.Value.maxStacks){
                        poisonGunStacks++;
                        StartCoroutine(Poisoned(debuff.Value.duration, true));
                    }
                }
                if (debuff.Key == Debuffs.Freeze.ToString()){
                    if (freezeGunStacks < debuff.Value.maxStacks){
                        freezeGunStacks++;
                        StartCoroutine(Freeze(debuff.Value.duration, true));
                    }
                }
            } }
        if(type == "sword")
        { 
            Dictionary<string, DebuffStats> debuffs = ItemManager.instance.GetSwordDebuffs();    
            foreach (KeyValuePair<string, DebuffStats> debuff in debuffs){
                if (debuff.Key == Debuffs.Fire.ToString()){
                    if (fireDuration == 0){
                        fireDuration += debuff.Value.duration;
                        StartCoroutine(OnFire());
                    }
                    else{
                        fireDuration += debuff.Value.duration;
                    }
                }
                if (debuff.Key == Debuffs.Poison.ToString()){
                    if (poisonMeleeStacks < debuff.Value.maxStacks){
                        poisonMeleeStacks++;
                        StartCoroutine(Poisoned(debuff.Value.duration, false));
                    }
                }
                if (debuff.Key == Debuffs.Freeze.ToString()){
                    if (freezeMeleeStacks < debuff.Value.maxStacks){
                        freezeMeleeStacks++;
                        StartCoroutine(Freeze(debuff.Value.duration, false));
                    }
                }
            }

        }
        if (!isDead){
            if (wasCrit){
                ShowFloatingText(MathF.Round(amount), "crit");
            }
            else{
                ShowFloatingText(MathF.Round(amount), type);
            }
            
        }
        
        

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        blinkTimer = blinkDuration;
    }

    void ShowFloatingText(float amount, string type)
    {
        var go = Instantiate(FloatingText, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().text = amount.ToString();
        if (type == "fire"){
            go.GetComponent<TextMeshPro>().color = fireHit;
        }
        else if (type == "poison"){
            go.GetComponent<TextMeshPro>().color = poisonHit;
        }
        else if (type == "crit"){
            go.GetComponent<TextMeshPro>().color = criticalHit;
        }
        else if (type == "freeze"){
            go.GetComponent<TextMeshPro>().color = freezeHit;
        }
        else{
            go.GetComponent<TextMeshPro>().color = normalHit;
        }
    }


    private void Die()
    {
        ItemManager.instance.OnKill();
        isDead = true;
        enemytargeting targeting = GetComponent<enemytargeting>();
        if (targeting != null)
        {
            targeting.enabled = false;
        }

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
        }
        EnemyDamage damage = GetComponent<EnemyDamage>();
        if (damage != null)
        {
            damage.enabled = false;
        }

        ragdoll.ActivateRagdoll();
        combatManager.Instance.DecreaseEnemyCount();

        

        StartCoroutine(DestroyAfterDelay(1.5f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);


        Destroy(gameObject);
    }
    IEnumerator OnFire(){
        int damage = 8;
        while (fireDuration > 0){
            yield return new WaitForSeconds(1f);
            this.DecreaseHealth(damage, "fire", false);
            fireDuration--;
            damage *= 2;
        }

    }
    IEnumerator Poisoned(int duration, bool isGun){
        int damage = 20;
        while (duration > 0){
            yield return new WaitForSeconds(1f);
            this.DecreaseHealth(damage, "poison", false);
            duration--;
        }
        if (isGun){
            poisonGunStacks--;
        }
        else{
            poisonMeleeStacks--;
        }
    }
    IEnumerator Freeze(int duration, bool isGun){
        moveSpeed *= 0.7f;
        navMeshAgent.speed = moveSpeed;
        while (duration > 0){
            yield return new WaitForSeconds(1f);
            this.DecreaseHealth(10f, "freeze", false);
            duration--;
        }
        if (isGun){
            freezeGunStacks--;
        }
        else{
            freezeMeleeStacks--;
        }
        moveSpeed = moveSpeed / 0.7f;
        navMeshAgent.speed = moveSpeed;
    }
}
