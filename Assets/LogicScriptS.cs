using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;

public class LogicScript : MonoBehaviour
{
    [Header("UI Elements")]
    public Text hpText;
    public GameObject gameOverUI;   // <- Drag the GameOver parent object here
    public Button playButton;       // <- Drag the Play button here

    [Header("Player Health")]
    public float playerHP = 100f;

    void Start()
    {
        // Hide Game Over UI at the start
        gameOverUI.SetActive(false);

        // Bind Play button to restart the game
        playButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        // Example: If HP <= 0, trigger Game Over
        if (playerHP <= 0 && !gameOverUI.activeSelf)
        {
            GameOver();
        }
    }

    public void UpdatePlayerHP(float hp)
    {
        playerHP = hp;
        hpText.text = "HP: " + math.round(hp).ToString();

        if (playerHP <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);   // Show Game Over UI + Play button
        Time.timeScale = 0f;          // Pause the game (optional)
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;  // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
