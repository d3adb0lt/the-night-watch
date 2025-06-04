using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerHealthManagerScript : MonoBehaviour
{
    public float playerHealth;
    public float maxHealth;
    public float healCharges;
    public float maxCharges;
    public GameObject generalUIParent;
    public GameObject playerRoot;
    public TextMeshProUGUI healthUI;
    public float healAmount;
    public GameObject loseTextObject;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = 100;
        playerHealth = maxHealth;
        
        maxCharges = 3;
        healCharges = maxCharges;
        healAmount = 20;
        loseTextObject.SetActive(false);
        SetPlayerHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        //for testing purposes, subtract health
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            PlayerChangeHealth(-10);
            if (playerHealth < 0) {
                playerHealth = 0;
            }
            SetPlayerHealthText();
        }

        //heal
        if (Input.GetKeyDown(KeyCode.F))
        {
            //check if healcharges are left
            if (healCharges <= 0 || playerHealth == maxHealth) 
            {
                //do nothing
            } 
            else
            {
                //restoring health
                PlayerChangeHealth(20);

                //if healing over max, set to max
                if (playerHealth > maxHealth) 
                {
                    playerHealth = maxHealth;
                }

                //subtract one heal charge
                healCharges--;

                //setting new health value
                SetPlayerHealthText();
            }

        }
    }

    void SetPlayerHealthText()
    {
        healthUI.text = "HP: " + playerHealth.ToString() + " Charges Left: " + healCharges.ToString();
        if (playerHealth <= 0)
        {
            //for game loss
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

    //temporary end-game function. Can be moved to another file if it fits better
    public void EndGame()
    {
        playerRoot.gameObject.SetActive(false);
        loseTextObject.gameObject.SetActive(true);
        generalUIParent.gameObject.SetActive(false);
    }


}
