using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    private static WorldSaveGameManager instance;

    [SerializeField] int worldSceneIndex = 1;

    private void Awake()
    {
        // can only ever be one, if another exists, destroy
        if (instance == null)
        {
            SetInstance(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator LoadNewGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

        yield return null;
    }

    public static WorldSaveGameManager GetInstance()
    {
        return instance;
    }
    public static void SetInstance(WorldSaveGameManager instance)
    {
        WorldSaveGameManager.instance = instance;
    }
}
