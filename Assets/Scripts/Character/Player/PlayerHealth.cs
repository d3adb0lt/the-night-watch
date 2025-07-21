using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //health and vial
    public float playerHealth;
    public float maxHealth = 100;
    public int healAmount = 25;
    public int maxVials = 4;
    public int currentVials = 0;

    //UI Elements
    public GameObject generalUIParent;
    public GameObject playerRoot;
    public Canvas loseTextObject;

    //health bar
    public Slider playerHealthBar;

    public TextMeshProUGUI playerHealthText;

    private PlayerAnimatorManager playerAnimatorManager;


    

    private void Start()
    {
        playerHealth = maxHealth;
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
    }

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        Debug.Log("Player took damage! Current health: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("Player died!");

            playerAnimatorManager.PlayTargetActionAnimation("Death", true, false, false);
            //  death logic here
            // kill player, end game

        }
        else
        {
            playerAnimatorManager.PlayTargetActionAnimation("TakeDamage", true, false, false);
            

            //clear input buffer
            PlayerInputBuffer.Instance.ClearBuffer();
        }
    }

    private void Update()
    {
        //for testing purposes, subtract health
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            PlayerChangeHealth(-10);
            if (playerHealth < 0) playerHealth = 0;
            SetPlayerHealthText();
        }

        // Use Vial when pressing H
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseVial();
        }
    }

    public void SetPlayerHealthText()
    {
        playerHealthText.text = "HP: " + playerHealth.ToString() + "/" + maxHealth.ToString();
        playerHealthBar.value = playerHealth;
        playerHealthBar.maxValue = maxHealth;
        if (playerHealth <= 0)
        {
            EndGame();
        }
    }

    public void PlayerChangeHealth(float healNum)
    {
        playerHealth += healNum;
        if (playerHealth < 0)
        {
            playerHealth = 0;
        }
        SetPlayerHealthText();
    }

    // public void UseVial()
    // {
    //     if (currentVials > 0 && playerHealth < maxHealth)
    //     {
    //         currentVials--;
    //         playerHealth = Mathf.Min(playerHealth + healAmount, maxHealth);
    //         SetPlayerHealthText();
    //         Debug.Log("Used a vial! Current health: " + playerHealth + ", Vials left: " + currentVials);
    //     }
    //     else if (currentVials <= 0)
    //     {
    //         Debug.Log("No vials left!");
    //     }
    //     else
    //     {
    //         Debug.Log("Health is already full!");
    //     }
    // }

    public void UseVial()
    {
        PlayerInventory inventory = GetComponent<PlayerInventory>();

        if (inventory != null && inventory.vialCount > 0 && playerHealth < maxHealth)
        {
            inventory.UseVial(1); // Use from inventory system
            //currentVials = inventory.vialCount;
            playerHealth = Mathf.Min(playerHealth + healAmount, maxHealth);
            SetPlayerHealthText();
            Debug.Log("Used a vial! Current health: " + playerHealth + ", Vials left: " + inventory.vialCount);
        }
        else if (inventory == null || inventory.vialCount <= 0)
        {
            Debug.Log("No vials left!");
        }
        else
        {
            Debug.Log("Health is already full!");
        }
    }

    // Temporary end-game logic
    public void EndGame()
    {
        playerRoot.gameObject.SetActive(false);
        loseTextObject.gameObject.SetActive(true);
        generalUIParent.gameObject.SetActive(false);
    }

    // Add a vial to the player
    // public void AddVial(int amount)
    // {
    //     currentVials = Mathf.Min(currentVials + amount, maxVials);
    //     Debug.Log("Picked up vial! Vials now: " + currentVials);
    // }
    
    public void AddVial(int amount)
{
    PlayerInventory inventory = GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            inventory.AddVial(amount); // Add to inventory system
            currentVials = inventory.vialCount;
    }
}
}
