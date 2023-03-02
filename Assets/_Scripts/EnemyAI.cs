using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask obstacleMask;

    // Patrolling
    private bool patrolling = true;
    private Vector3 currentDestination;
    public float patrolRange;

    // Detection
    public float detectionRange;
    private bool playerDetected = false;
    public LineRenderer lineRenderer;

    // Attack
    public float attackRange;
    public float attackDamage;
    public float attackCooldown;
    private bool canAttack = true;

    private void Start()
    {
        currentDestination = GetRandomPositionInMaze();
        agent.SetDestination(currentDestination);
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (patrolling)
        {
            // Check if we've reached the patrol destination
            if (agent.remainingDistance < 0.5f)
            {
                // Set a new random destination
                currentDestination = GetRandomPositionInMaze();
                agent.SetDestination(currentDestination);
            }

            // Check if we can see the player
            if (CanSeePlayer())
            {
                patrolling = false;
                playerDetected = true;
                lineRenderer.enabled = true;
                Debug.Log("Player detected!");
            }
        }
        else
        {
            // Move towards the player
            agent.SetDestination(player.position);

            // Check if we're close enough to attack
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                if (canAttack)
                {
                    Attack();
                    Debug.Log("Attack!");
                }
            }
            else
            {
                // Check if we can still see the player
                if (!CanSeePlayer()) {
                    patrolling = true;
                    playerDetected = false;
                    lineRenderer.enabled = false;
                    Debug.Log("Lost sight of player!");
                }
            }
        }

        // Update the line renderer if we can see the player
        if (playerDetected) {
            lineRenderer.SetPositions(new Vector3[] { transform.position, player.position });
        }
    }

    private bool CanSeePlayer() {
        // Check if the player is within detection range
        if (Vector3.Distance(transform.position, player.position) > detectionRange) {
            return false;
        }

        // Create a ray from the enemy to the player
        Vector3 directionToPlayer = player.position - transform.position;
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange)) {
            // Check if the hit object is the player
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer")) {
                // Draw a line between the enemy and the player
                Debug.DrawLine(transform.position, player.position, Color.green);
                return true;
            }
        }
        return false;
    }

    private void Attack() {
        canAttack = false;

        // Set a cooldown before we can attack again
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack() {
        canAttack = true;
    }

    private Vector3 GetRandomPositionInMaze() {
        // Get a random position within the patrol range
        Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRange, 1);
        return hit.position;
    }
}



