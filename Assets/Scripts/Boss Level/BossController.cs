using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private BossHealth bossHealth;

    [SerializeField] float attackRange = 2.0f;
    [SerializeField] float attackCooldown = 1.5f;
    private float lastAttackTime = 0;
    private bool isChasing;

    public GameObject introTextUI;
    public GameObject bossHealthBar;
    public GameObject bossText;

    public AudioSource audioSource;
    public AudioClip eatAudio; 
    public AudioClip roarAudio;
    public AudioClip punchAudio;
    public AudioClip walkAudio;
    public AudioClip deathAudio;



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
            agent.isStopped = false;

            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);

            float distance = Vector3.Distance(transform.position, player.position); 
            if (distance <= attackRange && ((Time.time - lastAttackTime) >= attackCooldown))
            {
                agent.isStopped = true;
                animator.SetTrigger("Punch");
                lastAttackTime = Time.time;
            }
        }

        // INTEGRATION TO DO - add end of game logic here
        if (bossHealth.currBossHealth == 0)
        {
            animator.SetTrigger("Die");
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

    // void OnTriggerEnter (Collider other)
    // {
    //     if (this.CompareTag("Fist") && other.CompareTag("Player"))
    //     {
    //         // INTEGRATION TO DO - have player take damage from collision with boss punch
    //         PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
    //         if (!playerHealth == null)
    //         {
    //             playerHealth.TakeDamage(20);
    //         }
    //     }
    // }

    public void PlaySfx(AudioClip audio)
    {
        if (audio != null)
        {
            audioSource.PlayOneShot(audio);
        }
    }
}
