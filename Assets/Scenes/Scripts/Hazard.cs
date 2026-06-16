using UnityEngine;

/// <summary>
/// Coloque esse script em qualquer obstáculo que cause dano ao player
/// (espinhos, fogo, serra, etc).
/// O objeto precisa ter um Collider2D marcado como IS TRIGGER.
/// </summary>
public class Hazard : MonoBehaviour
{
    [Header("Configurações")]
    public int damage = 1;                  // Quantas vidas tira
    public float invincibilityTime = 1f;   // Tempo entre danos consecutivos

    [Header("Efeitos")]
    public AudioClip hitSound;             // Som ao causar dano

    private static float lastDamageTime = -999f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        ApplyDamage(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        ApplyDamage(other.gameObject);
    }

    private void ApplyDamage(GameObject player)
    {
        if (Time.time - lastDamageTime < invincibilityTime) return;
        lastDamageTime = Time.time;

        // Toca som
        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, transform.position);

        // Empurra o player para cima ao tocar no espinho
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 8f);

        // Avisa o GameManager
        if (GameManager.Instance != null)
            GameManager.Instance.LoseLife();
    }
}
