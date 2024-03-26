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
            //Debug.Log("enemy hit");
            enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Debug.Log(swordDebuff);
                if (new System.Random().NextDouble() < critChance){
                    enemyHealth.DecreaseHealth(swordDamage * critMultiplier,"sword");
                }
                else{
                    enemyHealth.DecreaseHealth(swordDamage,"sword");
                }
                
                ItemManager.instance.OnMelee();
            }
        }
    }
}
