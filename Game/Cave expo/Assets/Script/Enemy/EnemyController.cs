﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent agent; // Компонент NavMeshAgent

    // Радиус, в пределах которого враг начинает следовать за игроком
    public float detectionRadius = 10f;
    public float attackRadius = 2f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

    // Точки патрулирования
    public Transform[] patrolPoints;
    private int currentPatrolIndex;

    // Время ожидания на каждой точке
    public float waitTimeAtPatrolPoint = 2f;
    private float waitTimer;

    private float speed;
    private float health;

    private Animator animator;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (patrolPoints.Length > 0)
        {
            currentPatrolIndex = 0;
            MoveToNextPatrolPoint();
        }
    }
    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, player.position);

        // Если игрок в зоне обнаружения, начать следовать за ним
        if (distanceToTarget <= detectionRadius && distanceToTarget > attackRadius)
        {
            agent.SetDestination(player.position);
        }
        // Если игрок достаточно близко, атаковать
        else if (distanceToTarget <= attackRadius)
        {
            agent.isStopped = true;

            // Проверяем время между атаками
            if (Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        // Если игрок не в зоне, патрулировать
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
            animator.SetBool("", false);
        }
    }
    void Attack()
    {
        // Логика атаки (можно добавить анимации, урон и т.д.)
        Debug.Log("Враг атакует!");
    }
    void OnDrawGizmosSelected()
    {
        // Визуализация радиусов обнаружения и атаки
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    void MoveToNextPatrolPoint()
    {
        // Если есть патрульные точки
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);

            // Переход к следующей точке (зацикливаем список)
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

            // Устанавливаем таймер ожидания
            waitTimer = waitTimeAtPatrolPoint;
        }
    }

}
