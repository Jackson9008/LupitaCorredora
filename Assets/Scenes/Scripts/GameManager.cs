using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Configurações iniciais")]
    public int startingLives = 3;

    [Header("Cenas")]
    public string menuSceneName = "MainMenu";      // Nome do menu
    public string gameOverSceneName = "GameOver";  // Nome da cena de Game Over

    // Estado atual
    private int currentLives;
    private int currentScore;
    private int currentCollectibles;
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetGame();
    }

    // ---- Reset do jogo ----
    public void ResetGame()
    {
        currentLives = startingLives;
        currentScore = 0;
        currentCollectibles = 0;
        isGameOver = false;
        UpdateHUD();
    }

    // ---- Pontuação ----
    public void AddScore(int amount)
    {
        if (isGameOver) return;
        currentScore += amount;
        UpdateHUD();
    }

    public int GetScore() => currentScore;

    // ---- Coletáveis ----
    public void AddCollectible()
    {
        if (isGameOver) return;
        currentCollectibles++;
        UpdateHUD();
    }

    public int GetCollectibles() => currentCollectibles;

    // ---- Vidas ----
    public void LoseLife()
    {
        if (isGameOver) return;

        currentLives--;
        UpdateHUD();

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            // Recarrega a cena atual (respawn)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public int GetLives() => currentLives;

    // ---- Game Over ----
    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("💀 GAME OVER! 💀");

        // Mostra o painel de Game Over no HUD
        if (HUDManager.Instance != null)
        {
            HUDManager.Instance.ShowGameOver();
        }

        // NÃO RESETA AS VIDAS AQUI!
        // Se tiver cena de GameOver, carrega ela
        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    // ---- Reiniciar o jogo ----
    public void RestartGame()
    {
        currentLives = startingLives;
        currentScore = 0;
        currentCollectibles = 0;
        isGameOver = false;
        UpdateHUD();
        SceneManager.LoadScene(0); // Ou a primeira fase
    }

    // ---- Reiniciar após Game Over ----
    public void RestartFromGameOver()
    {
        ResetGame();
        SceneManager.LoadScene(0); // Volta para o primeiro nível
    }

    // ---- Próxima fase ----
    public void LoadNextLevel()
    {
        if (isGameOver) return;

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            // 🌟 ÚLTIMO NÍVEL! O LevelGoal já vai tratar isso
            Debug.Log("🏆 Você completou todos os níveis! 🏆");
        }
    }

    // ---- Atualiza o HUD ----
    private void UpdateHUD()
    {
        if (HUDManager.Instance != null)
        {
            HUDManager.Instance.UpdateLives(currentLives);
            HUDManager.Instance.UpdateScore(currentScore);
            HUDManager.Instance.UpdateCollectibles(currentCollectibles);
        }
    }
}