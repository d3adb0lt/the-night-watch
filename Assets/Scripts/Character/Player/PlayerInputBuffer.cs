using UnityEngine;

public enum PlayerInputType
{
    None,
    Attack1,
    Attack2,
    Attack3,
    Roll
}

public class PlayerInputBuffer : MonoBehaviour
{

    public static PlayerInputBuffer Instance {get; private set;}
    public float bufferDuration = 0.2f;

    public PlayerInputType bufferedInput = PlayerInputType.None;
    private float bufferTimer = 0f;

    public bool HasBufferedInput => bufferedInput != PlayerInputType.None;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        Debug.Log("This buffer is updating!");
        if (bufferTimer > 0f)
        {
            Debug.Log("This buffer is subtracting time!");
            bufferTimer -= Time.deltaTime;

            if (bufferTimer <= 0f)
            {
                Debug.Log("buffer cleared!");
                ClearBuffer();
            }
        }
    }

    public void BufferInput(PlayerInputType input)
    {
        bufferedInput = input;
        bufferTimer = bufferDuration;
    }

    public PlayerInputType GetBufferedInput()
    {
        return bufferedInput;
    }

    public void ConsumeBufferedInput()
    {
        ClearBuffer();
    }

    private void ClearBuffer()
    {
        bufferedInput = PlayerInputType.None;
        bufferTimer = 0f;
    }

}
