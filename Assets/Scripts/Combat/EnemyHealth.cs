using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float currentHealth, maxHealth = 100f;
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

    [Header("Debuff Types")]
    public bool isIgnitable = false; 

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        combatManager = FindObjectOfType<combatManager>();

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }

    public void DecreaseHealth(float amount, string type, string debuff)
    {
        currentHealth -= amount;
        if (type == "gun")
        {
            if (gunhitClips != null)
            {
                int randomClip = Random.Range(0, gunhitClips.Length - 1);
                audioSource.clip = gunhitClips[randomClip];
                audioSource.Play();
            }
        }

        if (type == "sword")
        {
            if (swordhitSounds != null)
            {
                int randomClip = Random.Range(0, swordhitSounds.Length - 1);
                audioSource.clip = swordhitSounds[randomClip];
                audioSource.Play();
            }
        }
        if (type=="gun")
        {Debug.Log("Gun Hit"); }
        if(type == "sword")
        { Debug.Log("sword Hit" + debuff); }
        ShowFloatingText(amount);

        

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        blinkTimer = blinkDuration;
    }

    void ShowFloatingText(float amount)
    {
        var go = Instantiate(FloatingText, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().text = amount.ToString();
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

        ragdoll.ActivateRagdoll();
        combatManager.Instance.DecreaseEnemyCount();

        

        //StartCoroutine(DestroyAfterDelay(1.5f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);


        Destroy(gameObject);
    }
}
