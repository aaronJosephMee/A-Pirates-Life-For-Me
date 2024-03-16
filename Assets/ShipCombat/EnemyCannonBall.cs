using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonBall : MonoBehaviour
{
    public float damage = 25.0f;
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ShipCombat>().TakeDamage(damage);
        Destroy(this.gameObject);
    }
}
