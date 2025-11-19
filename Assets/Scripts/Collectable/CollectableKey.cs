using System;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Player"))
        {
            GameManager.Instance.UpdateKeysLeft();
            Destroy(this.gameObject);
        }
        
        
    }
}
