using UnityEngine;
using System.Collections.Generic;

public class EnemyWeaponManager : MonoBehaviour
{
    private Collider weaponCollider;
    public bool isBossWeapon;
    [SerializeField] float regDamage = 10f;
    [SerializeField] float bossDamage = 20f;
    private HashSet<Collider> alreadyHit = new HashSet<Collider>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponCollider = GetComponent<Collider>();
        isBossWeapon = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alreadyHit.Contains(other))
        {
            alreadyHit.Add(other);
            PlayerManager player = other.GetComponent<PlayerManager>();
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (player != null && !player.isInvulnerable)
            {
                Debug.Log("hit player!");
                if (this.isBossWeapon)
                {
                    playerHealth.TakeDamage(bossDamage);
                }
                else 
                {
                    playerHealth.TakeDamage(regDamage);
                }
            }
        }
    }

    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
        alreadyHit.Clear();
        //Debug.Log("Enemy weapon collision enabled");
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
        //Debug.Log("Enemy weapon collision disabled");
    }

    public bool CollisionIsEnabled()
    {
        return weaponCollider.enabled;
    }

    public void SetBossWeapon(bool arg)
    {
        this.isBossWeapon = arg;
    }

    public bool GetIsBossWeapon()
    {
        return this.isBossWeapon;
    }
}
