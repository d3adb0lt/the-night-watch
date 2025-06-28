using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private BossHealth bossHealth;

    public float attackRange = 2.0f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0;
    private float introCount = 0;
    private float deathCount = 0;
    private bool isMoving = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        bossHealth = GetComponent<BossHealth>();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        if (isMoving)
        {
            if (distance > attackRange)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            else
            {
                agent.isStopped = true;
                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10);

                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    BossAttack();
                    lastAttackTime = Time.time;
                }
            }
        }

        if (bossHealth.currBossHealth <= 0 && deathCount == 0)
        {
            BossDie();
            deathCount++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BossIntroSpot") && introCount == 0)
        {
            isMoving = false;
            Debug.Log("The Root has made his prescence known (boss intro triggered)");
            agent.isStopped = true;
            animator.SetTrigger("Intro");
            introCount++;
        }
    }

    public void OnRoarComplete()
    {
        isMoving = true;
    }

    void BossAttack()
    {
        Debug.Log("The Root used a punch");
        animator.SetTrigger("Attack");
    }
    
    void BossDie()
    {
        isMoving = false;
        Debug.Log("The Root has died");
        animator.SetTrigger("Death");
    }
}
