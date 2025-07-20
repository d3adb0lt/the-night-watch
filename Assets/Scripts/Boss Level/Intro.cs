using UnityEngine;

public class Intro : MonoBehaviour
{
    public BossController bossController;

    void OnTriggerExit(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            bossController.BossIntro();
        }
    }

    void OnTriggerEnter(Collider boss)
    {
        if (boss.CompareTag("Boss"))
        {
            bossController.IntroSequence();
            // enabled = false;
        }
    }
}
