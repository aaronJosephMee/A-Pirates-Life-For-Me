using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float currentHealth, maxHealth = 100f;
    private bool isDead;

    Ragdoll ragdoll;

    SkinnedMeshRenderer skinnedMeshRenderer;
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    combatManager combatManager;

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

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Hit");
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        blinkTimer = blinkDuration;
    }

    private void Die()
    {
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
