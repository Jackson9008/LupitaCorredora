using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Mostra no Console QUALQUER objeto que entrar na DeathZone
        Debug.Log("DeathZone tocou: " + other.gameObject.name + " | Tag: " + other.tag);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detectado! Perdendo vida...");
            GameManager.Instance.LoseLife();
        }
    }

    // Mostra o collider em verde na cena
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
        {
            Gizmos.DrawWireCube(transform.position + (Vector3)col.offset, col.size);
        }
    }
}