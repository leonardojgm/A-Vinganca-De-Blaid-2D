using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager InputManager { get; private set; }
    [Header("Dynamic Game objects")]
    [SerializeField] private GameObject bossDoor;
    [SerializeField] private PlayerBehavior player;
    [SerializeField] private BossBehavior boss;
    [SerializeField] private BossFightTrigger bossFightTrigger;
    [Header("Managers")]
    public UIManager UIManager;
    public AudioManager AudioManager;
    private int totalKeys;
    private int keysLeftToCollect;

    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);

        Instance = this;
        totalKeys = FindObjectsOfType<CollectableKey>().Length;
        keysLeftToCollect = totalKeys;

        print($"Totalkeys: {totalKeys}");

        InputManager = new InputManager();

        UIManager.UpdateKeysLeftText(totalKeys, keysLeftToCollect);

        bossFightTrigger.OnPlayerEnterBossFight += ActivateBossBehavior;

        player.GetComponent<Health>().OnDead += HandleGameOver;
        boss.GetComponent<Health>().OnDead += HandleVictory;
    }

    private void HandleGameOver()
    {
        UIManager.OpenGameOverPanel();
    }

    private void HandleVictory()
    {
        UIManager.ShowVictoryText();

        StartCoroutine(GoToCreditsScene());
    }

    public IEnumerator GoToCreditsScene()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Credits");
    }

    public void UpdateKeysLeft()
    {
        keysLeftToCollect--;

        UIManager.UpdateKeysLeftText(totalKeys, keysLeftToCollect);

        CheckAllKeysCollected();
    }

    private void CheckAllKeysCollected()
    {
        if (keysLeftToCollect <= 0)
        {
            print("TODAS AS CHAVES FORAM COLETADAS");

            Destroy(bossDoor);
        }
    }

    public void UpdateLives(int amount)
    {
        UIManager.UpdateLivesText(amount);
    }

    public PlayerBehavior GetPlayer()
    {
        return player;
    }

    private void ActivateBossBehavior()
    {
        boss.StartChasing();
    }
}
