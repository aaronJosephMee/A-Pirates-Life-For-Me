using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageAmount = 10.0f; 
    public float damageRadius = 2.0f; 
    public LayerMask targetLayer; 

    public float rehitTime = 1.0f; 

    private float nextHitTime;

    private void Start()
    {

    }

    private void Update()
    {
        if (Time.time >= nextHitTime)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius, targetLayer);
            foreach (Collider col in colliders)
            {
                Player player = col.GetComponent<Player>();
                if (player)
                {
                    player.takeDamage(damageAmount);

                    nextHitTime = Time.time + rehitTime;
                }
            }
        }
    }

}
