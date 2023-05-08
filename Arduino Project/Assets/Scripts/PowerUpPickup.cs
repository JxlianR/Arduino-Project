using UnityEngine;
using UnityEngine.UI;

public class PowerUpPickup : MonoBehaviour
{
    public enum PowerUpType
    {
        SpeedBoost,
        Invincibility,
        ScoreMultiplier
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

        /*if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.storedPowerUp = this.gameObject;
                player.powerUpAvailable = true;
            }
        }*/
    }

    /*private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.storedPowerUp = null;
                player.powerUpAvailable = false;
            }
        }
    }*/

    public PowerUpPickup.PowerUpType GetRandomPowerUpType()
    {
        PowerUpPickup.PowerUpType[] powerUpTypes = (PowerUpPickup.PowerUpType[])System.Enum.GetValues(typeof(PowerUpPickup.PowerUpType));
        int randomIndex = Random.Range(0, powerUpTypes.Length);
        return powerUpTypes[randomIndex];
    }
}
