using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitToMenuButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(ClosePauseMenu);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        quitToMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void ClosePauseMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void OpenOptionsMenu()
    {
        print("Opening options");

        GameManager.Instance.UIManager.OpenOptionsPanel();
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}