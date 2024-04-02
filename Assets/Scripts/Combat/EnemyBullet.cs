using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    float timer;
    [SerializeField] LayerMask bulletLayer;
    [HideInInspector] public WeaponManager weapon;
    List<GameObject> prevHits = new List<GameObject>();
    float collisionDelay = 0;
    GameObject hit;
    public float damage;
    float noHitPeriod = 0.3f;
    float noHitTimer;
    // Start is called before the first frame update
    void Start()
    {
        noHitTimer = Time.time + noHitPeriod;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToDestroy) Destroy(this.gameObject);
        if (collisionDelay > 0){
            
            collisionDelay -= Time.deltaTime;
            if (collisionDelay <= 0){
                if (hit != null){
                    if (hit.tag == "Player"){
                        hit.GetComponent<Player>().takeDamage(damage);
                    }
                    else{
                        hit.GetComponentInParent<EnemyHealth>().DecreaseHealth(damage, "", false);
                    }
                }  
                Destroy(this.gameObject);
            }       
        }    
    }

    private void OnTriggerEnter(Collider other) {
        if (noHitTimer < Time.time){
            hit = other.gameObject;
            collisionDelay = 0.025f;
        }
    }
}
