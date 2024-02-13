using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float currentHealth, maxHealth = 100f;

    Ragdoll ragdoll;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void DecreaseHealth(float amount, Vector3 direction)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        ragdoll.ActivateRagdoll();
        


        //StartCoroutine(DestroyAfterDelay(1.5f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);


        Destroy(gameObject);
    }
}
