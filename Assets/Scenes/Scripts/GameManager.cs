using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerencia o estado global do jogo: vidas, pontuação e coletáveis.
/// Coloque esse script em um GameObject vazio chamado "GameManager" na cena.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Configurações iniciais")]
    public int startingLives = 3;

    // Estado atual
    private int currentLives;
    private int currentScore;
    private int currentCollectibles;

    void Awake()
    {
        // Singleton: só existe um GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentLives = startingLives;
        currentScore = 0;
        currentCollectibles = 0;
        UpdateHUD();
    }

    // ---- Pontuação ----
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateHUD();
    }

    public int GetScore() => currentScore;

    // ---- Coletáveis ----
    public void AddCollectible()
    {
        currentCollectibles++;
        UpdateHUD();
    }

    public int GetCollectibles() => currentCollectibles;

    // ---- Vidas ----
    public void LoseLife()
    {
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

    // ---- Próxima fase ----
    public void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("Você completou todos os níveis!");
            // Aqui você pode carregar uma cena de "Você venceu!" se tiver
        }
    }

    // ---- Game Over ----
    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Reinicia o jogo do zero
        currentLives = startingLives;
        currentScore = 0;
        currentCollectibles = 0;
        SceneManager.LoadScene(0); // Volta para o Menu Principal (cena 0)
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
