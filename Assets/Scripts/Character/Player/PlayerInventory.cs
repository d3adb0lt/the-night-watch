using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public int keyCount = 0;
    public int vialCount = 0;
    public bool hasKey = false;


    public Image keyBox;
    public TextMeshProUGUI keyCountText;

    public Image vialBox;
    public TextMeshProUGUI vialCountText;

    public Sprite keySprite;
    public Sprite vialSprite;


    void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (keyBox != null)
        {
            if (keyCount > 0)
            {
                keyBox.sprite = keySprite;
                keyBox.color = Color.white;
            }
            else
            {
                keyBox.sprite = null;
                keyBox.color = new(1, 1, 1, 0.3f);
            }
        }
        if (keyCountText != null)
        {
            keyCountText.gameObject.SetActive(keyCount > 0);
            keyCountText.text = keyCount.ToString();
        }

        if (vialBox != null)
        {
            if (vialCount > 0)
            {
                vialBox.sprite = vialSprite;
                vialBox.color = Color.white;
            }
            else
            {
                vialBox.sprite = null;
                vialBox.color = new(1, 1, 1, 0.3f);
            }
        }
        if (vialCountText != null)
        {
            vialCountText.gameObject.SetActive(vialCount > 0);
            vialCountText.text = vialCount.ToString();
        }
    }


    public void AddKey(int amount = 1)
    {
        keyCount += amount;
        hasKey = true;
        UpdateDisplay();
    }

    public void AddVial(int amount = 1)
    {
        vialCount += amount;
        UpdateDisplay();
    }

    public void UseVial(int amount = 1)
    {
        if (vialCount >= amount)
        {
            vialCount -= amount;
            UpdateDisplay();
        }
    }
}

