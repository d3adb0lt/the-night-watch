using System.Collections.Generic;
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
    private class BufferedInput
    {
        public PlayerInputType input;
        public float timeAdded;

        public BufferedInput(PlayerInputType input, float timeAdded)
        {
            this.input = input;
            this.timeAdded = timeAdded;
        }

        public bool IsExpired(float currentTime, float duration)
        {
            return currentTime > timeAdded + duration;
        }
    }


    public static PlayerInputBuffer Instance { get; private set; }

    private Queue<BufferedInput> inputQueue = new Queue<BufferedInput>();

    [Header("Buffer Settings")]
    public float bufferDuration = 0.75f;

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
        float currentTime = Time.time;

        // Remove expired inputs
        while (inputQueue.Count > 0 && inputQueue.Peek().IsExpired(currentTime, bufferDuration))
        {
            inputQueue.Dequeue();
        }
    }

    public void BufferInput(PlayerInputType input)
    {
        inputQueue.Enqueue(new BufferedInput(input, Time.time));
    }

    public PlayerInputType GetBufferedInput()
    {
        return inputQueue.Count > 0 ? inputQueue.Peek().input : PlayerInputType.None;
    }

    public bool HasBufferedInput => inputQueue.Count > 0;

    public void ConsumeBufferedInput()
    {
        if (inputQueue.Count > 0)
        {
            inputQueue.Dequeue();
        }
    }

    public void ClearBuffer()
    {
        inputQueue.Clear();
    }
}
