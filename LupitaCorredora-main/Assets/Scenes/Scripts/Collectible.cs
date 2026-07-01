using UnityEngine;

/// <summary>
/// Coloca esse script em qualquer item coletável (gema, moeda, estrela etc).
/// O objeto precisa ter um Collider2D marcado como TRIGGER (Is Trigger = true).
/// </summary>
public class Collectible : MonoBehaviour
{
    [Header("Configurações")]
    public int value = 1;               // Quantos pontos vale esse item
    public string collectibleType = "gem"; // Tipo do item (para o HUD diferenciar se quiser)

    [Header("Efeitos")]
    public AudioClip collectSound;      // Som ao coletar (arraste um arquivo de áudio aqui)
    public GameObject collectEffect;    // Partícula/efeito visual (opcional, pode deixar vazio)

    [Header("Animação flutuante")]
    public bool floatAnimation = true;  // O item fica "flutuando" no lugar?
    public float floatSpeed = 2f;
    public float floatHeight = 0.15f;

    private Vector3 startPosition;

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
        // Só reage ao Player
        if (!other.CompareTag("Player")) return;

        // Toca som
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        // Spawna efeito visual (partícula)
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }

        // Avisa o GameManager para somar pontos
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(value);
            GameManager.Instance.AddCollectible();
        }

        // Destrói o item
        Destroy(gameObject);
    }
}
