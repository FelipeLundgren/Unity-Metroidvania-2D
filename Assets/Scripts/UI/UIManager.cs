using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI KeysText;
    [SerializeField] private TextMeshProUGUI livesText;

    public void UpdateKeysLeftTexty(int totalValue, int leftValue)
    {
        KeysText.text = $"{leftValue}/{totalValue}";
    }

    public void UpdateLivesText(int amount)
    {
        livesText.text = $"{amount}";
    }
}