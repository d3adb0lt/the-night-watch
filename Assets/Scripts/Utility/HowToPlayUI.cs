using UnityEngine;
using UnityEngine.UI;

public class HowToPlayUI : MonoBehaviour
{
    [Header("Assign Component")]
    public GameObject windowPanel;
    public ScrollRect scrollRect;

    void Awake()
    {
        windowPanel.SetActive(true);
        //scrollRect.verticalNormalizedPosition = 1f;
        Time.timeScale = 0f;
        //bruh
    }

    public void HideWindow()
    {
        windowPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
