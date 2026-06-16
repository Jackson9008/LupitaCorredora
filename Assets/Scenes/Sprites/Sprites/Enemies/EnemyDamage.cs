using UnityEngine;

/// <summary>
/// Gerencia o dano causado pelo inimigo ao player e a possibilidade
/// de ser derrotado quando o player pula em cima dele.
/// Requer: Collider2D no inimigo (não trigger para corpo, trigger para detecção de topo).
/// </summary>
public class EnemyDamage : MonoBehaviour
{
    [Header("Configurações")]
    public int damageToPlayer = 1;          // Dano causado ao player (em vidas)
    public bool canBeStomped = true;        // Pode ser derrotado pulando em cima?
    public float stompBounceForce = 8f;     // Força do quique ao pisar no inimigo

    [Header("Stomped (topo do inimigo)")]
    public Transform stompCheck;            // Ponto no TOPO do inimigo para detectar stomping
    public float stompCheckRadius = 0.2f;

    [Header("Efeitos")]
    public AudioClip damageSound;           // Som ao causar dano no player
    public AudioClip stompSound;            // Som ao ser derrotado
    public GameObject deathEffect;          // Efeito visual ao morrer (opcional)

    [Header("Invencibilidade do Player")]
    public float invincibilityTime = 1f;    // Tempo que o player fica invencível após dano

    private bool isDead = false;
    private static float lastDamageTime = -999f; // Controle de invencibilidade

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        // Verifica se o player está caindo em cima (stomping)
        if (canBeStomped && IsBeingStomped(collision))
        {
            Stomp(collision.gameObject);
        }
        else
        {
            DamagePlayer(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        // Continua causando dano se o player ficar encostado (com delay)
        if (!IsBeingStomped(collision))
        {
            DamagePlayer(collision.gameObject);
        }
    }

    private bool IsBeingStomped(Collision2D collision)
    {
        // Verifica se o player está acima e caindo
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (playerRb == null) return false;

        bool playerAbove = collision.gameObject.transform.position.y > transform.position.y + 0.2f;
        bool playerFalling = playerRb.linearVelocity.y < -0.1f;

        return playerAbove && playerFalling;
    }

    private void DamagePlayer(GameObject player)
    {
        // Invencibilidade temporária para não tirar várias vidas de uma vez
        if (Time.time - lastDamageTime < invincibilityTime) return;
        lastDamageTime = Time.time;

        // Toca som de dano
        if (damageSound != null)
            AudioSource.PlayClipAtPoint(damageSound, transform.position);

        // Avisa o GameManager
        if (GameManager.Instance != null)
            GameManager.Instance.LoseLife();

        // Empurra o player para longe do inimigo
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 knockback = (player.transform.position - transform.position).normalized;
            knockback = new Vector2(knockback.x * 4f, 5f);
            playerRb.linearVelocity = knockback;
        }
    }

    private void Stomp(GameObject player)
    {
        // Toca som de stomping
        if (stompSound != null)
            AudioSource.PlayClipAtPoint(stompSound, transform.position);

        // Efeito visual
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Quica o player para cima
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, stompBounceForce);

        // Destrói o inimigo
        isDead = true;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (stompCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(stompCheck.position, stompCheckRadius);
        }
    }
}
