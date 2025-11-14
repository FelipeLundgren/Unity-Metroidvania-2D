using System;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collison)
    {
        GameManager.Instance.UpdateKeysLeft();
        Destroy(this.gameObject);
        
    }
}
