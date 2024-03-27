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

    public float attackRange = 3.0f;
    public bool withinAttackRange = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();

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
                withinAttackRange = true;
                animator.SetBool("inRange", true);
                int randomAttack = Random.Range(1, 3);
                //animator.SetInteger("attackNum", randomAttack);
                transform.LookAt(player.position); 
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
