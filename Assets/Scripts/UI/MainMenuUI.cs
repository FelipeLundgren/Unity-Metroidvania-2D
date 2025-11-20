using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [Header("Menu UI properties")] 
    
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        optionsMenu.SetActive(false);
        startButton.onClick.AddListener(GoToGameplayScene);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void OpenOptionsMenu()
    {
        MainMenuManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        optionsMenu.SetActive(true);
    }

    private void GoToGameplayScene()
    {
        MainMenuManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        LoadScene.Instance.StartLoad("Gameplay");
    }

    private void ExitGame()
    {
        MainMenuManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}