using UnityEngine;
using UnityEngine.UI;
using TMPro; // Se não tiver TextMeshPro, troque TMP_Text por Text

/// <summary>
/// Gerencia a interface do jogador (HUD).
/// Coloque esse script em um GameObject vazio chamado "HUDManager" dentro do Canvas.
/// </summary>
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("Textos do HUD")]
    public TMP_Text livesText;        // Ex.: "Vidas: 3"
    public TMP_Text scoreText;        // Ex.: "Pontos: 150"
    public TMP_Text collectiblesText; // Ex.: "Gemas: 5"

    [Header("Telas")]
    public GameObject gameOverPanel;  // Painel de Game Over (pode deixar vazio por agora)

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Garante que o painel de Game Over começa oculto
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Inicializa os textos
        UpdateLives(3);
        UpdateScore(0);
        UpdateCollectibles(0);
    }

    public void UpdateLives(int lives)
    {
        if (livesText != null)
            livesText.text = "Vidas: " + lives;
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "Pontos: " + score;
    }

    public void UpdateCollectibles(int amount)
    {
        if (collectiblesText != null)
            collectiblesText.text = "Gemas: " + amount;
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
}
