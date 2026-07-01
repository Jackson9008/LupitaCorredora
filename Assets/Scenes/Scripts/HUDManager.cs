using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("Textos do HUD")]
    public TMP_Text livesText;
    public TMP_Text scoreText;
    public TMP_Text collectiblesText;

    [Header("Telas")]
    public GameObject gameOverPanel;     // Painel de Game Over
    public GameObject victoryPanel;      // 🌟 Painel de Vitória (opcional)
    public TMP_Text gameOverMessage;     // Mensagem de Game Over
    public TMP_Text victoryMessage;      // 🌟 Mensagem de Vitória

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

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

    // 🌟 MOSTRA GAME OVER
    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (gameOverMessage != null)
                gameOverMessage.text = "💀 GAME OVER 💀\nPressione R para reiniciar";
        }
    }

    // 🌟 MOSTRA MENSAGEM DE VITÓRIA
    public void ShowVictoryMessage()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            if (victoryMessage != null)
                victoryMessage.text = "🏆 PARABÉNS!\nVocê completou o jogo! 🏆";
        }
        else
        {
            // Se não tiver painel, mostra no console e no texto de score
            Debug.Log("🏆 PARABÉNS! VOCÊ VENCEU! 🏆");
            if (scoreText != null)
                scoreText.text = "🏆 VOCÊ VENCEU! 🏆";
        }
    }
}