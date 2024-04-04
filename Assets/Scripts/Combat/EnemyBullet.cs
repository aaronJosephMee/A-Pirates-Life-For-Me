using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float scaleFactor = 1f;
    private Rigidbody rb;
    private SphereCollider sc;

    private bool reflected = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        noHitTimer = Time.time + noHitPeriod;
        damage *= scaleFactor;
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
                    if (hit.tag == "Cutlass")
                    {
                        if (!reflected)
                        {
                            reflected = true;
                            timer = 0.0f;
                            damage *= 2;
                            rb.velocity = -rb.velocity * 4;
                            // sc.excludeLayers.
                        }   
                        return;
                    }
                    if (hit.tag == "Player"){
                        hit.GetComponent<Player>().takeDamage(damage, false);
                    }
                    else if (hit.tag == "Enemy"){
                        hit.GetComponentInParent<EnemyHealth>().DecreaseHealth(damage, "", false);
                    }
                }  
                Destroy(this.gameObject);
            }       
        }    
    }

    private void OnTriggerEnter(Collider other) {
        print(other);
        if (noHitTimer < Time.time){
            hit = other.gameObject;
            collisionDelay = 0.0001f;
        }
    }
}
