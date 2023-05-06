using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrawlerEnemyAI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask obstacleMask;

    // Detection
    [Header("Detection")]
    public float detectionRange;
    public float patrolSpeed;
    public float chaseSpeed;
    private bool playerDetected = false;
    private Vector3 lastSeenPosition;

    // Attack
    [Header("Attack")]
    public float attackRange;
    public float attackDamage;
    public float attackCooldown;
    private bool canAttack = true;

    // Patrol
    [Header("Patrol")]
    public List<Transform> patrolPoints;
    private int currentPatrolIndex = 0;

    private void Start()
    {
        agent.autoRepath = false; // Disable auto-repath
        agent.speed = patrolSpeed;
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            playerDetected = true;
            lastSeenPosition = player.position;
            agent.speed = chaseSpeed;
            agent.SetDestination(lastSeenPosition);
        }
        else if (playerDetected && agent.remainingDistance <= agent.stoppingDistance)
        {
            playerDetected = false;
            agent.speed = patrolSpeed;
            FindClosestPatrolPoint();
            Patrol();
        }
        else if (!playerDetected)
        {
            Patrol();
        }

        if (playerDetected && Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (canAttack)
            {
                Attack();
            }
        }

        for (int i = 0; i < agent.path.corners.Length - 1; i++)
        {
            Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.red);
        }
    }

    private bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) > detectionRange)
        {
            return false;
        }

        Vector3 directionToPlayer = player.position - transform.position;
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer"))
            {
                return true;
            }
        }
        return false;
    }

    private void Patrol()
    {
        if (patrolPoints.Count == 0) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    private void FindClosestPatrolPoint()
    {
        int closestIndex = 0;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < patrolPoints.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, patrolPoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        currentPatrolIndex = closestIndex;
    }

    private void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (canAttack)
            {
                playerHealth.TakeDamage(attackDamage);
                canAttack = false;
            }
        }
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}
