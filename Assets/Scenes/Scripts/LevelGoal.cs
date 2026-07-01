using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    [Header("Configurações")]
    public AudioClip victorySound;
    public GameObject completionEffect;

    [Header("Animação")]
    public bool floatAnimation = true;
    public float floatSpeed = 2f;
    public float floatHeight = 0.2f;

    [Header("Cena de Vitória")]
    public string victorySceneName = "VictoryScreen";  // Nome da cena de vitória
    public bool showMessage = true;                     // Mostrar mensagem na tela

    private Vector3 startPosition;
    private bool levelCompleted = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (floatAnimation)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (levelCompleted) return;
        if (!other.CompareTag("Player")) return;

        levelCompleted = true;

        // Efeito visual
        if (completionEffect != null)
            Instantiate(completionEffect, transform.position, Quaternion.identity);

        // Toca som de vitória
        if (victorySound != null)
            AudioSource.PlayClipAtPoint(victorySound, transform.position);

        // 🌟 MOSTRA MENSAGEM DE FIM DE JOGO
        if (showMessage)
        {
            // Verifica se é o último nível
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            int totalScenes = SceneManager.sceneCountInBuildSettings;

            if (currentIndex >= totalScenes - 1)
            {
                // ÚLTIMO NÍVEL! Mostra mensagem de "Você Venceu!"
                Debug.Log("🏆 PARABÉNS! VOCÊ COMPLETOU O JOGO! 🏆");
                
                // Se tiver um painel de vitória no HUD, mostra
                if (HUDManager.Instance != null)
                {
                    HUDManager.Instance.ShowVictoryMessage();
                }
                
                // Opcional: carrega uma cena de vitória
                if (!string.IsNullOrEmpty(victorySceneName))
                {
                    SceneManager.LoadScene(victorySceneName);
                }
            }
            else
            {
                // Avança para o próximo nível
                if (GameManager.Instance != null)
                    GameManager.Instance.LoadNextLevel();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}