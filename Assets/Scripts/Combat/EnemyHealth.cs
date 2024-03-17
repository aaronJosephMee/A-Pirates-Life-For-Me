using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float currentHealth, maxHealth = 100f;
    private bool isDead;

    Ragdoll ragdoll;

    combatManager combatManager;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;

        combatManager = FindObjectOfType<combatManager>();

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Hit");
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        
    }

    private void Die()
    {
        isDead = true;

        ragdoll.ActivateRagdoll();
        combatManager.Instance.DecreaseEnemyCount();

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


        //StartCoroutine(DestroyAfterDelay(1.5f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);


        Destroy(gameObject);
    }
}
