using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemytargeting : MonoBehaviour
{
    

    private NavMeshAgent agent;
    public Transform player;
    GameObject playerAvatar;
    public Animator animator;
    public Player playerScript;
    public bool isRanged = false;
    public GameObject bullet;
    public float attackRange = 3.0f;
    public bool withinAttackRange = false;
    [SerializeField] float fireDelay = 1f;
    float fireTimer = 0;
    [SerializeField] public float bulletVelocity;
    [SerializeField] public Vector3 bulletSize = Vector3.one;
    [SerializeField] GameObject gun;


    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        

        AnimationClip clip = animator.runtimeAnimatorController.animationClips[0]; 
        float animationLength = clip.length;
        float randomTimeOffset = Random.Range(0f, animationLength);
        animator.Play("slow walk", 0, randomTimeOffset);

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        } 
        else
        {
            Debug.Log("Player object not found.");
        }
        playerScript = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.isDead)
        {
            animator.SetBool("IsPlayerDead", true);
        }
        else
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= attackRange)
            {
                agent.destination = this.transform.position;
                withinAttackRange = true;
                animator.SetBool("inRange", true);
                int randomAttack = Random.Range(1, 3);
                //animator.SetInteger("attackNum", randomAttack);
                transform.LookAt(player.position); 
                if (isRanged && fireTimer <= Time.time){
                    //Debug.Log("Here");
                    fireTimer = Time.time + fireDelay;
                    GameObject currentBullet = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
                    currentBullet.transform.localScale = bulletSize;

                    Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
                    rb.AddForce(gun.transform.forward * bulletVelocity, ForceMode.Impulse);
                }
            }
            else
            {
                withinAttackRange = false;
                animator.SetBool("inRange", false); 

                agent.destination = player.position;
            }
        }
    }
}
