using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitToMenuButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        quitToMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
