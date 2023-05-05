using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    public enum PowerUpType
    {
        SpeedBoost,
        Invincibility,
        ScoreMultiplier
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("Player"))
        {
            MovePlayer player = other.GetComponent<MovePlayer>();
            if (player != null)
            {
                PowerUpPickup.PowerUpType randomPowerUpType = GetRandomPowerUpType();
                player.PickupPowerUp(randomPowerUpType);

                Debug.Log("Player picked up power-up: " + randomPowerUpType);

                Destroy(gameObject);
            }
        }*/

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.nearPowerUp = this.gameObject;
                player.powerUpAvailable = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.nearPowerUp = null;
                player.powerUpAvailable = false;
            }
        }
    }

    public PowerUpPickup.PowerUpType GetRandomPowerUpType()
    {
        PowerUpPickup.PowerUpType[] powerUpTypes = (PowerUpPickup.PowerUpType[])System.Enum.GetValues(typeof(PowerUpPickup.PowerUpType));
        int randomIndex = Random.Range(0, powerUpTypes.Length);
        return powerUpTypes[randomIndex];
    }
}
