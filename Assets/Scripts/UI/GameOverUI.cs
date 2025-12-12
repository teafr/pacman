using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject gameOverPanel;

    public void Show()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; // Останавливаем игру
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
