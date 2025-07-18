using UnityEngine;
using System.Collections.Generic;

public class EnemyWeaponManager : MonoBehaviour
{
    private Collider weaponCollider;
    private HashSet<Collider> alreadyHit = new HashSet<Collider>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alreadyHit.Contains(other))
        {
            Debug.Log("hit player!");

            alreadyHit.Add(other);
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(10);
            }
        }
    }

    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
        alreadyHit.Clear();
        Debug.Log("Enemy weapon collision enabled");
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
        Debug.Log("Enemy weapon collision disabled");
    }

    public bool CollisionIsEnabled()
    {
        return weaponCollider.enabled;
    }
}
