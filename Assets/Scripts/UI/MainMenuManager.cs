using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    public AudioManager AudioManager;
    public InputManager InputManager { get; private set; }
    
    

    

    



    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;
        InputManager = new InputManager();
        
    }

    

    

    
}