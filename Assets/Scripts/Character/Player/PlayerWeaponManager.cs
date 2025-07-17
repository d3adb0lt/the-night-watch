using UnityEngine;
using System.Collections.Generic;

public class PlayerWeaponManager : MonoBehaviour
{

    public Collider weaponCollider;
    float damageAmount;
    PlayerManager playerManager;

    private HashSet<Collider> alreadyHit = new HashSet<Collider>();

    void Awake()
    {
       playerManager = GetComponentInParent<PlayerManager>();
       damageAmount = 10;
       weaponCollider = GetComponent<Collider>();
    }


    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !alreadyHit.Contains(other))
        {
            Debug.Log("hit enemy!");

            alreadyHit.Add(other);
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            }
        }
        else if (other.CompareTag("Spawner") && !alreadyHit.Contains(other))
        {
            Debug.Log("hit spawner!");

            alreadyHit.Add(other);
            EnemySpawner enemySpawner = other.GetComponent<EnemySpawner>();
            if (enemySpawner != null)
            {
                enemySpawner.TakeDamage(damageAmount);
            }
        }
    }

    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
        alreadyHit.Clear();
        Debug.Log("Weapon collision enabled");
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
        Debug.Log("Weapon collision disabled");
    }

    public void SetWeaponDamage(float newDamageAmount)
    {
        damageAmount = newDamageAmount;
    }


}
