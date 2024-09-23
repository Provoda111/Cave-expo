using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform player;
    private NavMeshAgent agent;

    public float detectionRadius = 10f;
    public float attackRadius = 2f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    public float enemyAttack = 5;
    private float playerAttack;

    public Transform[] patrolPoints;
    private int currentPatrolIndex;

    public float waitTimeAtPatrolPoint = 2f;
    private float waitTimer;

    private float speed;
    public float health = 30;

    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        playerAttack = playerController.playerAttack;


        if (patrolPoints.Length > 0)
        {
            currentPatrolIndex = 0;
            MoveToNextPatrolPoint();
        }
    }
    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, player.position);
        if (distanceToTarget <= detectionRadius && distanceToTarget > attackRadius)
        {
            agent.SetDestination(player.position);
        }
        else if (distanceToTarget <= attackRadius)
        {
            agent.isStopped = true;
            if (Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Patrol();
        }
        speed = agent.velocity.magnitude;
        HandleAnimations();
    }
    void Patrol()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
        }
        else
        {
            MoveToNextPatrolPoint();
        }
    }
    void HandleAnimations()
    {
        if (speed >= 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        if (health <= 0)
        {
            Death();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
           
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            
            waitTimer = waitTimeAtPatrolPoint;
        }
    }
    public void GetHit()
    {
        health -= 10f;
        animator.SetTrigger("GotHit");
        if (health <= 0)
        {
            Death();
        }

    }
    public void Attack()
    {
        animator.SetTrigger("Attacks");
        StartCoroutine(DisableSwordColliderAfterAttack());
    }
    private void Death()
    {
        animator.SetTrigger("Death");
        StartCoroutine(WaitForDelete());
        Destroy(gameObject);
    }
    private IEnumerator DisableSwordColliderAfterAttack()
    {
        yield return new WaitForSeconds(1.5f);
    }
    private IEnumerator WaitForDelete()
    {
        yield return new WaitForSeconds(5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("RightHand@Attack01"))
        {
            GetHit();
        }
    }
    public void DebugTest()
    {
        print("Test Debugging Test");
    }
}
