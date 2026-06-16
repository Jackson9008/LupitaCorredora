using UnityEngine;

public class HitBox : MonoBehaviour
{
    [Header("Configurações")]
    public GameObject coinPrefab;        // Prefab da moeda
    public int coinsToSpawn = 1;         // Quantas moedas spawnar
    public AudioClip hitSound;           // Som ao bater na caixa
    public Sprite emptyBoxSprite;        // Sprite da caixa vazia (opcional)

    private bool hasBeenHit = false;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasBeenHit) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        // Verifica se o player bateu por baixo
        bool hitFromBelow = collision.gameObject.transform.position.y < transform.position.y - 0.3f;

        if (hitFromBelow)
        {
            hasBeenHit = true;
            HitBoxAction();
        }
    }

    private void HitBoxAction()
    {
        // Som
        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, transform.position);

        // Spawna moedas acima da caixa
        for (int i = 0; i < coinsToSpawn; i++)
        {
            if (coinPrefab != null)
            {
                Vector3 spawnPos = transform.position + new Vector3(0, 1f, 0);
                Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            }
        }

        // Troca sprite para caixa vazia
        if (emptyBoxSprite != null && sr != null)
            sr.sprite = emptyBoxSprite;
    }
}