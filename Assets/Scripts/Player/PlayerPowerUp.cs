using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    private string powerup = "Hyper Jump enable";
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerBehavior>().PowerUpCollected(powerup);
            Destroy(this.gameObject);

        }
    }

 
}
