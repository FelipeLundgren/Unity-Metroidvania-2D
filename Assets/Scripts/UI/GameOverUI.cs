using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(GoToMenu);
    }

    private void GoToMenu()
    {
        
        LoadScene.Instance.StartLoad("Menu");
    }

    private void RestartGame()
    {
        LoadScene.Instance.StartLoad("Gameplay");
    }
}
