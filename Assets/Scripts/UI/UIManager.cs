using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keysText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI victoryText;
    [Header("Panels")]
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject GameOverPanel;

    private void Awake()
    {
        victoryText.gameObject.SetActive(false);
        OptionsPanel.SetActive(false);
        PausePanel.SetActive(false); 
        GameOverPanel.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnMenuOpenClose += OpenClosePauseMenu;
    }

    public void OpenClosePauseMenu()
    {
        if (PausePanel.activeSelf == false)
        {
            PausePanel.SetActive(true);
        }
        else
        {
            PausePanel.SetActive(false);
        }
    }

    public void OpenOptionsPanel()
    {
        print("Set options to be opened");
        
        OptionsPanel.SetActive(true);
    }

    public void OpenGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }

    public void ShowVictoryText()
    {
        victoryText.gameObject.SetActive(true);
    }

    public void UpdateKeysLeftText(int totalValue, int leftValue)
    {
        keysText.text = $"{leftValue}/{totalValue}";
    }

    public void UpdateLivesText(int amount)
    {
        livesText.text = $"{amount}";
    }
}