using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public void Retry()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RestartFromGameOver();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}