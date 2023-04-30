using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask obstacleMask;

    // Detection
    [Header("Detection")]
    public float detectionRange;
    public float initialSpeed;
    public float reducedSpeed;
    private bool playerDetected = false;

    // Attack
    [Header("Attack")]
    public float attackRange;
    public float attackDamage;
    public float attackCooldown;
    private bool canAttack = true;

    [Header("Active Time")]
    public float activeTime = 30f;
    private Vector3 initialPosition;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip movingSound;
    public AudioClip JumpScare;
    public AudioClip screamSound;
    public float screamCooldown = 20f;
    private bool canScream = true;

    [Header("Movement Update Interval")]
    public float movementUpdateInterval = 0.1f;

    private void Start()
    {
        agent.autoRepath = false; // Disable auto-repath
        initialPosition = transform.position;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        agent.speed = initialSpeed;
        StartCoroutine(ActiveTimer());
        StartCoroutine(UpdateDestination());
        audioSource.clip = movingSound;
        audioSource.loop = true;
        audioSource.Play();
        canScream = true;
    }

    private IEnumerator ActiveTimer()
    {
        yield return new WaitForSeconds(activeTime);
        transform.position = initialPosition;
        gameObject.SetActive(false);
    }

    private IEnumerator UpdateDestination()
    {
        while (true)
        {
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, player.position, NavMesh.AllAreas, path))
            {
                agent.SetPath(path);
            }

            yield return new WaitForSeconds(movementUpdateInterval);
        }
    }

    private void Update()
{
    // Check if we can see the player
    if (CanSeePlayer())
    {
        agent.speed = reducedSpeed; // Set agent speed to reduced speed
    }
    else
    {
        playerDetected = false;
        agent.speed = initialSpeed; // Set agent speed to initial speed
    }

    if (playerDetected)
    {
        // Check if we're close enough to attack
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (canAttack)
            {
                Attack();
            }
        }
    }

    if (agent.velocity.magnitude > 0f && !audioSource.isPlaying)
    {
        audioSource.clip = movingSound;
        audioSource.loop = true;
        audioSource.Play();
    }
    else if (agent.velocity.magnitude == 0f && audioSource.clip == movingSound)
    {
        audioSource.Stop();
    }
}

    private bool CanSeePlayer()
{
    // Check if the player is within detection range
    if (Vector3.Distance(transform.position, player.position) > detectionRange)
    {
        return false;
    }

    // Create a ray from the enemy to the player
    Vector3 directionToPlayer = player.position - transform.position;
    if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange))
    {
        // Check if the hit object is the player
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer"))
        {
            if (!playerDetected)
            {
                playerDetected = true;

                if(canScream){
                  audioSource.clip = screamSound;
                  audioSource.loop = false;
                  audioSource.Play(); 
                  canScream = false;
                  StartCoroutine(ScreamCooldownTimer());

                }else{
                    return false;
                }
                
            }
            return true;
        }
    }
    return false;
}

private IEnumerator ScreamCooldownTimer()
{
    yield return new WaitForSeconds(screamCooldown);
    canScream = true;
}

    private void Attack()
    {
        // Check if we're close enough to attack
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (canAttack)
            {
                playerHealth.TakeDamage(attackDamage);
                canAttack = false;
                Debug.Log("Attack!");
            }
        }
        audioSource.clip = JumpScare;
        audioSource.loop = false;
        audioSource.Play();
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}


