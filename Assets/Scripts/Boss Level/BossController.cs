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
    private bool isChasing;

    public GameObject introTextUI;
    public GameObject bossHealthBar;
    public GameObject bossText;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        bossHealth = GetComponent<BossHealth>();

        introTextUI.SetActive(true);
        bossHealthBar.SetActive(false);
        bossText.SetActive(false);

        isChasing = false;
    }

    void Update()
    {
        if (player != null && isChasing == true)
        {
            agent.SetDestination(player.position);

            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
    }

    public void BossIntro()
    {
        animator.SetTrigger("Jump");
    }

    public void OnRoarComplete()
    {
        Debug.Log("ON ROAR COMPLETE");
        introTextUI.SetActive(false);
        bossHealthBar.SetActive(true);
        bossText.SetActive(true);
        isChasing = true;
    }
}
