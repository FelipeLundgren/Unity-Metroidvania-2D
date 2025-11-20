using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsBehavior : MonoBehaviour
{
    private void GoToMenu()
    {
        LoadScene.Instance.StartLoad("Menu");
    }
}
