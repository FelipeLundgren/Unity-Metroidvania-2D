using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioManager AudioManager;
    public InputManager InputManager { get; private set; }

    private int totalKeys;
    private int keysLeftToCollect;

    [Header("Managers")] public UIManager UIManager;

    [Header("Dynamic Game Objects")] 
    [SerializeField] private GameObject bossDoor;
    [SerializeField] private PlayerBehavior player;
    [SerializeField] private BossBehavior boss;
    [SerializeField] private BossFightTrigger bossFightTrigger;

    



    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;
        InputManager = new InputManager();

        totalKeys = FindObjectsOfType<CollectableKey>().Length;
        keysLeftToCollect = totalKeys;

        UIManager.UpdateKeysLeftTexty(totalKeys, -keysLeftToCollect + totalKeys);
        bossFightTrigger.OnPlayerEnterBossFight += ActivateBossBehavior;

        player.GetComponent<Health>().OnDead += HandleGameOver;
        boss.GetComponent<Health>().OnDead += HandleVictory;
    }

    public void UpdateKeysLeft()
    {
        keysLeftToCollect--;

        UIManager.UpdateKeysLeftTexty(totalKeys, -keysLeftToCollect + totalKeys);
        CheckAllKeysCollected();
    }

    private void CheckAllKeysCollected()
    {
        if (keysLeftToCollect == 0)
        {
            Destroy(bossDoor);
        }
    }

    private void ActivateBossBehavior()
    {
        boss.StartChasing();
    }

    private void HandleGameOver()
    {
        StartCoroutine(WaitPanel());
        
    }

    private IEnumerator WaitPanel()
    {
        yield return new WaitForSeconds(2f);
        UIManager.OpenGameOverPanel();
    }

    private void HandleVictory()
    {
        UIManager.ShowVictoryText();
        StartCoroutine(GoToCreditsScene());
    }

    private IEnumerator GoToCreditsScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadSceneAsync("Credits");
    }

    public void UpdateLives(int amount)
    {
        UIManager.UpdateLivesText(amount);
    }

    public PlayerBehavior GetPlayer()
    {
        return player;
    }

    public void SetPowerUpText(string text)
    {

        StartCoroutine(UIManager.PowerUpText(text));

    }
}