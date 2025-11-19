using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsBehavior : MonoBehaviour
{
    private void GoToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
