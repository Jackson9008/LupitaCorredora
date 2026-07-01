using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryButtons : MonoBehaviour
{
    public void PlayAgain()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RestartFromGameOver();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}