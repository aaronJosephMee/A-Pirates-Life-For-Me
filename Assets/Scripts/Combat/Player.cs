using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeDamage(float damage)
    {
        this.health -= damage;
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyRange"))
        {
            takeDamage(5);
        }
    }
    */

}
