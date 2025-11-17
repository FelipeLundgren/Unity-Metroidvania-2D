using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI KeysText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI victoryText;
    
    [Header("Panels")]
    [SerializeField] private GameObject GameOverPanel;

    private void Awake()
    {
        victoryText.gameObject.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    public void UpdateKeysLeftTexty(int totalValue, int leftValue)
    {
        KeysText.text = $"{leftValue}/{totalValue}";
    }

    public void UpdateLivesText(int amount)
    {
        livesText.text = $"{amount}";
    }

    public void OpenGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }

    public void ShowVictoryText()
    {
        victoryText.gameObject.SetActive(true);
    }
}