using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask groundMask, playerMask;

    Animator animator;

    // Patrolling
    public Vector3 walkpoint;
    bool walkpointSet;
    public float walkpointRange;

    //Attacking
    public float attackResetTime;
    public bool hasAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake(){
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if(!playerInSightRange && !playerInAttackRange){
            Patrol();
            animator.SetInteger("State", 0);
            agent.speed = 4;
        }

        if(playerInSightRange && !playerInAttackRange){
            Chase();
            animator.SetInteger("State", 1);
            agent.speed = 8;
        }

        if(playerInSightRange && playerInAttackRange){
            Attack();
            animator.SetInteger("State", 2);
            agent.speed = 4;
        }
    }

    private void Patrol(){
        if(!walkpointSet){
            SearchWalkPoint();
        }
        else {
            agent.SetDestination(walkpoint);
        }

        Vector3 walkpointDisplacement = transform.position - walkpoint;

        if(walkpointDisplacement.magnitude < 1f){
            walkpointSet = false;
        }
    }

    private void Chase(){
        agent.SetDestination(player.position);
    }

    private void Attack(){
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        // Make attack TODO

        if(!hasAttacked){
            hasAttacked = true;
            Invoke("ResetAttack", attackResetTime);
        }
    }

    private void ResetAttack(){
        hasAttacked = false;
    }

    private void SearchWalkPoint(){
        float z = Random.Range(-walkpointRange, walkpointRange);
        float x = Random.Range(-walkpointRange, walkpointRange);

        walkpoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(walkpoint, -transform.up, 2f, groundMask)){
            walkpointSet = true;
        }
    }

    // TODO Health stats;
}
