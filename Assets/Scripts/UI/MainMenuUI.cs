using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;

    [Header("Menu UI properties")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        optionsPanel.SetActive(false);
        startButton.onClick.AddListener(GoToGameplayScene);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void GoToGameplayScene()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

        SceneManager.LoadScene("Gameplay");
    }

    private void OpenOptionsMenu()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

        optionsPanel.SetActive(true);
    }

    private void ExitGame()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
