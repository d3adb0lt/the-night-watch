using Unity.Netcode;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    private static PlayerUIManager instance;

    [Header("NETWORK JOIN")]
    [SerializeField] bool startGameAsClient;

    private void Awake()
    {
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

    private void Update()
    {
        if (startGameAsClient)
        {
            startGameAsClient = false;
            // FIRST MUST SHUT DOWN NETWORK,
            // BECAUSE WE HAVE STARTED THE NETWORK AS A HOST FROM THE TITLE SCREEN
            NetworkManager.Singleton.Shutdown();
            // THEN RESTART NETWORK, AS A CLIENT
            NetworkManager.Singleton.StartClient();
        }
    }

    public static PlayerUIManager GetInstance()
    {
        return instance;
    }
    public static void SetInstance(PlayerUIManager instance)
    {
        PlayerUIManager.instance = instance;
    }
}
