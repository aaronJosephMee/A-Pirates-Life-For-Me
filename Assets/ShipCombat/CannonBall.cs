using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float damage = 50.0f;
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<EnemyShip>().TakeDamage(damage);
        Destroy(this.gameObject);
    }
}
