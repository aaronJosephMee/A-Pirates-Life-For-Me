using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionHitbox : MonoBehaviour
{
    public float explosionForce = 1000f;
    public float explosionRadius = 5f;

    public EnemyHealth enemyHealth;
    public float exDamage;
    public LayerMask targetLayer;

    public float rehitTime = 10.0f;

    private float nextHitTime;

    private void Update()
    {
        if (Time.time >= nextHitTime)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius/*, targetLayer*/);
            foreach (Collider col in colliders)
            {
                enemyHealth = col.gameObject.GetComponent<EnemyHealth>();
                Vector3 explosionPosition = transform.position;

                if (enemyHealth != null)
                {
                    enemyHealth.DecreaseHealth(exDamage,"sword",false);
                }

                Rigidbody rb = col.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }

            nextHitTime = Time.time + rehitTime;
        }

    }
}
