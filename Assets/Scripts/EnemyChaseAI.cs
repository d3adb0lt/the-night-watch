using UnityEngine;

public class EnemyChaseAI : MonoBehaviour
{
    private Transform player;
    public float chaseRange = 15f;
    public float speed = 3f;

    private bool isChasing = false;

    private Vector3 idleTarget;
    private float idleTimer = 0f;
    public float idleMoveDuration = 2f;
    public float idleWaitTime = 2f;
    public float idleSpeed = 1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PickNewIdleTarget();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= chaseRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            IdleMovement();
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            // Smoothly rotate towards the player
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Move forward in the facing direction
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void IdleMovement()
    {
        idleTimer += Time.deltaTime;

        Vector3 direction = (idleTarget - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            // this rotates the character
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // move foward motion
        transform.position += transform.forward * idleSpeed * Time.deltaTime;

        
        if (Vector3.Distance(transform.position, idleTarget) < 0.5f || idleTimer >= idleMoveDuration)
        {
            idleTimer = 0f;
            Invoke(nameof(PickNewIdleTarget), idleWaitTime);
        }
    }

    void PickNewIdleTarget()
    {
       
        Vector3 randomDirection = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
        idleTarget = transform.position + randomDirection;
    }
}