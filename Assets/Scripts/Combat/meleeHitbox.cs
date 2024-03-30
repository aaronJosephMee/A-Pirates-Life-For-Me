using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeHitbox : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public float swordDamage;
    public float critChance;
    public float critMultiplier;
    public string swordDebuff=null;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                if (new System.Random().NextDouble() < critChance){
                    enemyHealth.DecreaseHealth(MathF.Round(swordDamage * critMultiplier,2),"sword", true);
                }
                else{
                    enemyHealth.DecreaseHealth(MathF.Round(swordDamage,2),"sword", false);
                }
                
                ItemManager.instance.OnMelee();
            }
        }
    }
}
