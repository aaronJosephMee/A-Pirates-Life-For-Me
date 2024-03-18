using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemytargeting : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    GameObject playerAvatar;

    public float attackRange = 3.0f;
    public bool withinAttackRange = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.Log("Player object not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            withinAttackRange = true;
            //attack
            Debug.Log("enemy attacks");
        }
        else
        {
            withinAttackRange = false;
            agent.destination = player.position;
        }
    }
}
