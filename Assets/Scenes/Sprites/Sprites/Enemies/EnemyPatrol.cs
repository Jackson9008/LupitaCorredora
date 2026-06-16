using UnityEngine;

/// <summary>
/// Faz o inimigo patrulhar entre dois pontos automaticamente.
/// Crie dois objetos vazios na cena como filhos do inimigo: "PointA" e "PointB"
/// e arraste-os nos campos abaixo, ou use a distância de patrulha automática.
/// </summary>
public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrulha")]
    public Transform pointA;           // Ponto esquerdo da patrulha
    public Transform pointB;           // Ponto direito da patrulha
    public float speed = 2f;           // Velocidade do inimigo

    [Header("Detecção de borda (opcional)")]
    public bool detectEdge = true;     // Vira ao chegar na borda da plataforma?
    public float edgeCheckDistance = 0.5f;
    public LayerMask groundLayer;

    private Transform currentTarget;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Começa indo para o ponto B
        if (pointA != null && pointB != null)
            currentTarget = pointB;
    }

    void Update()
    {
        if (pointA == null || pointB == null) return;

        Patrol();
        CheckEdge();
    }

    private void Patrol()
    {
        // Move em direção ao alvo atual
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(currentTarget.position.x, transform.position.y),
            speed * Time.deltaTime
        );

        // Vira o sprite conforme direção
        if (currentTarget == pointB)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;

        // Troca de alvo ao chegar perto
        float distToTarget = Vector2.Distance(
            new Vector2(transform.position.x, 0),
            new Vector2(currentTarget.position.x, 0)
        );

        if (distToTarget < 0.1f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
        }
    }

    private void CheckEdge()
    {
        if (!detectEdge) return;

        // Raio para baixo na frente do inimigo
        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        Vector2 origin = (Vector2)transform.position + direction * edgeCheckDistance;

        bool hasGround = Physics2D.Raycast(origin, Vector2.down, 1f, groundLayer);

        // Se não há chão na frente, vira
        if (!hasGround)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
        }
    }

    // Desenha os pontos de patrulha na cena
    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pointA.position, 0.15f);
            Gizmos.DrawSphere(pointB.position, 0.15f);
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
