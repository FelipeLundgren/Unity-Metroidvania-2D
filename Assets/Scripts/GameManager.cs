using UnityEngine;

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

    public void UpdateLives(int amount)
    {
        UIManager.UpdateLivesText(amount);
    }

    public PlayerBehavior GetPlayer()
    {
        return player;
    }
}