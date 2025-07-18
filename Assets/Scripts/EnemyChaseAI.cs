using UnityEngine;

public class EnemyChaseAI : MonoBehaviour
{
    private Transform player;
    private Animator animator;
    private EnemyWeaponManager enemyWeaponManager;

    public float chaseRange = 20f;
    public float attackRange = 6f;
    public float speed = 2.2f;
    public float idleSpeed = 1f;

    private bool isChasing = false;
    private bool isAttacking = false;

    // Idle patrol variables
    private Vector3 idleTarget;
    private float idleTimer = 0f;
    public float idleMoveDuration = 2f;
    public float idleWaitTime = 2f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        enemyWeaponManager = GetComponentInChildren<EnemyWeaponManager>();
        enemyWeaponManager.DisableWeaponCollider();
        

        PickNewIdleTarget();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        // Update state flags
        isChasing = distance <= chaseRange;
        isAttacking = isChasing && distance <= attackRange;

        // Set Animator parameters
        animator.SetBool("isChasing", isChasing);
        animator.SetBool("isAttacking", isAttacking);

        if (isAttacking)
        {
            FaceTarget(player.position);
            // No movement while attacking
        }
        else if (isChasing)
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
        FaceTarget(player.position);
    }

    void IdleMovement()
    {
        FaceTarget(idleTarget);
        // idleTimer += Time.deltaTime;

        // FaceTarget(idleTarget);
        // transform.position += transform.forward * idleSpeed * Time.deltaTime;

        // if (Vector3.Distance(transform.position, idleTarget) < 0.5f || idleTimer >= idleMoveDuration)
        // {
        //     idleTimer = 0f;
        //     Invoke(nameof(PickNewIdleTarget), idleWaitTime);
        // }
    }

    void PickNewIdleTarget()
    {
        idleTarget = transform.position;
        // Vector3 randomDirection = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
        // idleTarget = transform.position + randomDirection;
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public void EnableWeaponCollider()
    {
        enemyWeaponManager.EnableWeaponCollider();
    }

    public void DisableWeaponCollider()
    {
        enemyWeaponManager.DisableWeaponCollider();
    }
}