using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;  // ← NOVO: necessário para a corrotina

public class MenuManager : MonoBehaviour
{
    // ← NOVO: variável para controlar se já está carregando
    private bool isLoading = false;

    public void PlayGame()
    {
        // ← NOVO: impede múltiplos cliques
        if (isLoading) return;

        // Reseta o jogo antes de começar
        if (GameManager.Instance != null)
            GameManager.Instance.ResetGame();

        // ← NOVO: inicia o carregamento assíncrono
        StartCoroutine(LoadSceneAsync(1)); // 1 = primeira fase
    }

    // ← NOVO: carregamento assíncrono
    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        isLoading = true;

        // Opcional: mostra mensagem no console
        Debug.Log("🔄 Carregando fase...");

        // Inicia o carregamento da cena em segundo plano
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // Permite que a cena seja ativada quando carregar
        operation.allowSceneActivation = true;

        // Aguarda até o carregamento terminar
        while (!operation.isDone)
        {
            // Mostra o progresso no console (opcional)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progress < 1f)
            {
                Debug.Log($"Carregando: {Mathf.Round(progress * 100)}%");
            }

            yield return null; // Espera um frame
        }

        isLoading = false;
        Debug.Log("✅ Fase carregada!");
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}