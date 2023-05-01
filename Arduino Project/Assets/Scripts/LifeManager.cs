using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    public GameObject gameOverAsset;
    public Text livesText; 

    private void Start()
    {
        currentLives = maxLives;
        UpdateLivesText(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ReduceLives(1);
        }
    }

    private void ReduceLives(int amount)
    {
        currentLives -= amount;

        UpdateLivesText(); 

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        if (gameOverAsset != null)
        {
            gameOverAsset.SetActive(true);
        }

        Destroy(this);
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives.ToString();
        }
    }
}
