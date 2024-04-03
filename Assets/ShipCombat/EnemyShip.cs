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
    private bool Alive = true;

    private Rigidbody rb;

    public GameObject fire;

    public AudioSource cannonSound;
    public AudioClip cannonSFX;
    public GameObject cannonShotVFX;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fire.SetActive(false);

        GameObject playerObject = GameObject.FindGameObjectWithTag("PlayerShip");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (!SeaGameManager.instance.stopCombat && Alive)
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
    }

    private void Chase()
    {
        transform.LookAt(player.position);
        transform.position += transform.forward * 2.5f * Time.deltaTime;
    }

    private void Attack()
    {
        transform.LookAt(player);

        var _cannonBall = Instantiate(CannonBall, EnemyCannon.position, EnemyCannon.rotation);
        _cannonBall.GetComponent<Rigidbody>().velocity = EnemyCannon.forward * cannonForce;

        GameObject effect = Instantiate(cannonShotVFX, EnemyCannon.position, EnemyCannon.rotation);
        Destroy(effect, 1.5f);

        cannonSound.PlayOneShot(cannonSFX);

        Invoke(nameof(ResetAttack), FireRate);
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(float dmg)
    {
        EnemyHealth -= dmg;

        if (EnemyHealth <= 0)
        {
            SeaGameManager.instance.AddPoint(1);
            fire.SetActive(true);
            Alive = false;
            DisableMovementAndAttack();
            Invoke(nameof(DestroyEnemy), 5.0f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void DisableMovementAndAttack()
    {
        isAttacking = false;
        rb.useGravity = true;
    }
}
