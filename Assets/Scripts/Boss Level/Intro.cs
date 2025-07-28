using UnityEngine;

public class Intro : MonoBehaviour
{
    public BossController bossController;

    void OnTriggerExit(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            Debug.Log("EXIT");
            bossController.BossIntro();
            enabled = false;
        }
    }
}
