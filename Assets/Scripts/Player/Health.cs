using System;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int lives;
    

    

    public event Action OnDead;
    public event Action OnHurt;

    

    public void TakeDamage()
    {
        lives--;
        HandleDamageTaken();
    }

    public void HandleDamageTaken()
    {
        if (lives <= 0)
        {
            OnDead?.Invoke();
            
        }
        else
        {
            OnHurt?.Invoke();
            
        }
    }

    public int GetLives()
    {
        return lives;
    }

    
}