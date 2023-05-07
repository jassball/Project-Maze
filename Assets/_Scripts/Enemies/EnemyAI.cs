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
    public float initialSpeed;
    public float reducedSpeed;
    private bool playerDetected = false;

    // Attack
    [Header("Attack")]
    public float attackRange;
    public float attackDamage;

    [Header("Active Time")]
    public float activeTime = 30f;
    private Vector3 initialPosition;

    [Header("Chase Delay Timer")]
    public float fromSecond;
    public float toSecond;
    private bool chaseDelayComplete = false;

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
        StartCoroutine(ChaseDelay());
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

    private IEnumerator ChaseDelay()
    {
        float delay = Random.Range(fromSecond, toSecond);
        Debug.Log(delay);
        yield return new WaitForSeconds(delay);
        chaseDelayComplete = true;
        StartCoroutine(UpdateDestination());
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

            if(CanSeePlayer()) {
                agent.speed = reducedSpeed;
            } else {
                playerDetected = false;
                agent.speed = initialSpeed;
            }

            yield return new WaitForSeconds(movementUpdateInterval);
        }
    }

    private void Update()
    {
    
    if (!chaseDelayComplete) {
        return; 
    }    

    if (playerDetected)
    {
        if(canScream) {
            audioSource.clip = screamSound;
            audioSource.loop = false;
            audioSource.Play(); 
            canScream = false;
            StartCoroutine(ScreamCooldownTimer());
        }
        // Check if we're close enough to attack
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            Attack();
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
    
    // Create a ray from the enemy to the player
    Vector3 directionToPlayer = player.position - transform.position;
    if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit))
    {
        // Check if the hit object is the player
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer"))
        {
            if (!playerDetected)
            {
                playerDetected = true;
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
        playerHealth.TakeDamage(attackDamage);
        Debug.Log("Attack!");
            
        audioSource.clip = JumpScare;
        audioSource.loop = false;
        audioSource.Play();
    }
}


