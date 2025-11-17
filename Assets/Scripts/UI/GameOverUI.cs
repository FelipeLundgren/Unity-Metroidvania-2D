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
        
        SceneManager.LoadScene("Menu");
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
