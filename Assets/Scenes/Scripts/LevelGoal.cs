using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    [Header("Configurações")]
    public AudioClip victorySound;
    public GameObject completionEffect;

    [Header("Animação")]
    public bool floatAnimation = true;
    public float floatSpeed = 2f;
    public float floatHeight = 0.2f;

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

        if (completionEffect != null)
            Instantiate(completionEffect, transform.position, Quaternion.identity);

        if (GameManager.Instance != null)
            GameManager.Instance.LoadNextLevel();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}