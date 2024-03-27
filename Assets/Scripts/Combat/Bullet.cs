using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    float timer;
    [SerializeField] LayerMask bulletLayer;
    [HideInInspector] public WeaponManager weapon;
    List<GameObject> prevHits = new List<GameObject>();
    int hits = 0;
    float collisionDelay = 0;
    EnemyHealth enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToDestroy) Destroy(this.gameObject);
        if (collisionDelay > 0){
            
            collisionDelay -= Time.deltaTime;
            if (collisionDelay <= 0){
                if (enemy != null){
                    if (new System.Random().NextDouble() < weapon.critChance){
                        enemy.DecreaseHealth(weapon.damage * weapon.critMultiplier,"gun");
                    }
                    else{
                        enemy.DecreaseHealth(weapon.damage,"gun");
                    }
                    enemy = null;
                }  
                hits++;
                if (weapon.richochet < hits){
                    Destroy(this.gameObject);
                }  
            }       
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & bulletLayer) != 0)
            return;
        if (collision.gameObject.GetComponentInParent<EnemyHealth>()){
            this.enemy = collision.gameObject.GetComponentInParent<EnemyHealth>();
            
        }
        collisionDelay = 0.025f;
    }
}
