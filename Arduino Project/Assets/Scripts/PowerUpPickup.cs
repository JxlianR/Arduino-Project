using UnityEngine;
using UnityEngine.UI;

public class PowerUpPickup : MonoBehaviour
{
    float lifetime = 5f;

    public enum PowerUpType
    {
        SpeedBoost,
        Invincibility,
        ScoreMultiplier
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null && player.powerUpAvailable == false)
            {
                PowerUpPickup.PowerUpType randomPowerUpType = GetRandomPowerUpType();
                player.PickupPowerUp(randomPowerUpType);

                Debug.Log("Player picked up power-up: " + randomPowerUpType);
            

                Destroy(gameObject);
            }
        }
    }

    // Get a random Power-Up
    public PowerUpPickup.PowerUpType GetRandomPowerUpType()
    {
        PowerUpPickup.PowerUpType[] powerUpTypes = (PowerUpPickup.PowerUpType[])System.Enum.GetValues(typeof(PowerUpPickup.PowerUpType));
        int randomIndex = Random.Range(0, powerUpTypes.Length);
        return powerUpTypes[randomIndex];
    }
}
