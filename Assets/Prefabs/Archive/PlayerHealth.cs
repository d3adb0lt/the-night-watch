using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int healAmount = 25;
    public int maxHealth = 100;
    public int currentHealth;
    public int maxVials = 5;
    public int currentVials = 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");
            //  death logic here
        }
    }

    public void AddVial(int amount)
    {
        currentVials = Mathf.Min(currentVials + amount, maxVials);
        Debug.Log("Picked up vial! Vials now: " + currentVials);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            UseVial();
        }
    }

    public void UseVial()
    {
        if (currentVials > 0 && currentHealth < maxHealth)
        {
            currentVials--;
            currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
            Debug.Log("Used a vial! Current health: " + currentHealth + ", Vials left: " + currentVials);
        }
        else if (currentVials <= 0)
        {
            Debug.Log("No vials left!");
        }
        else
        {
            Debug.Log("Health is already full!");
        }
    }

}

