using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    
    private Rigidbody[] rigidbodies;
    private Animator[] animators;
    private MonoBehaviour[] scripts;



    void Start()
    {
        pauseMenu.SetActive(false);

        rigidbodies = FindObjectsOfType<Rigidbody>();
        animators = FindObjectsOfType<Animator>();
        scripts = FindObjectsOfType<MonoBehaviour>();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        foreach (var rb in rigidbodies)
            rb.isKinematic = true;

        foreach (var animator in animators)
            animator.speed = 0;

        foreach (var script in scripts)
        {
            if (script != this && script.gameObject != pauseMenu && !(script is UIBehaviour))
                script.enabled = false;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        foreach (var rb in rigidbodies)
            rb.isKinematic = false;

        foreach (var animator in animators)
            animator.speed = 1;

        foreach (var script in scripts)
        {
            if (script != this && script.gameObject != pauseMenu && !(script is UIBehaviour))
                script.enabled = true;
        }
        
    }

    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}