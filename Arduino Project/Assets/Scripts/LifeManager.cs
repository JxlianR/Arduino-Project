using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    public GameObject gameoverObject;
    public Text gameoverText;
    public Button retryButton;
    public Text livesText;

    private void Start()
    {
        currentLives = maxLives;
        UpdateLivesText();
        HideGameover();
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
        gameoverObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives.ToString();
        }
    }

    private void HideGameover()
    {
        if (gameoverObject != null)
        {
            gameoverObject.SetActive(false);
        }
    }
}
