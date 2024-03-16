using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShip : MonoBehaviour
{
    public Transform player;
    public GameObject CannonBall;
    public Transform EnemyCannon;
    public float FireRate = 4.0f;
    [SerializeField] private float cannonForce = 20.0f;
    public float EnemyHealth = 100.0f;

    private bool isAttacking = false;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < 40 && !isAttacking)
        {
            isAttacking = true;
            Attack();
        }
        else if (distance >= 40)
        {
            isAttacking = false;
            Chase();
        }
    }

    private void Chase()
    {
        transform.LookAt(player.position);
        transform.position += transform.forward * 1.0f * Time.deltaTime;
    }

    private void Attack()
    {
        transform.LookAt(player);

        var _cannonBall = Instantiate(CannonBall, EnemyCannon.position, EnemyCannon.rotation);
        _cannonBall.GetComponent<Rigidbody>().velocity = EnemyCannon.forward * cannonForce;

        Invoke(nameof(ResetAttack), FireRate);
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(float dmg)
    {
        EnemyHealth -= dmg;

        if (EnemyHealth <= 0) DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
