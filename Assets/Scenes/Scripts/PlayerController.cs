using UnityEngine;

/// <summary>
/// Controla o movimento do personagem: andar, correr, pular e escalar.
/// Requer: Rigidbody2D + Collider2D no GameObject do player.
/// Para escalar, objetos com a tag "Climbable" devem ter um Collider2D (pode ser trigger).
/// Para detectar o chão, crie um objeto vazio "GroundCheck" como filho do player,
/// posicionado nos "pés" do personagem.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;

    [Header("Pulo")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Escalada")]
    public float climbSpeed = 3f;

    [Header("Áudio")]
    public AudioClip jumpSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float moveInput;
    private float climbInput;
    private bool isGrounded;
    private bool isRunning;
    private bool isClimbing;
    private bool canClimb;

    private float defaultGravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultGravityScale = rb.gravityScale;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleInput();
        CheckGround();
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        Move();
        Climb();
    }

    private void HandleInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        climbInput = Input.GetAxisRaw("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Inicia/para escalada se estiver em área "Climbable"
        if (canClimb && Mathf.Abs(climbInput) > 0f)
        {
            isClimbing = true;
        }
        else if (!canClimb)
        {
            isClimbing = false;
        }

        // Pulo
        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            if (jumpSound != null && audioSource != null)
                audioSource.PlayOneShot(jumpSound);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Permite "soltar" da escalada com pulo
        if (Input.GetButtonDown("Jump") && isClimbing)
        {
            if (jumpSound != null && audioSource != null)
                audioSource.PlayOneShot(jumpSound);
            isClimbing = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

        

private void Move()
    {
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        if (isClimbing)
        {
            // Movimento horizontal reduzido enquanto escala (opcional)
            rb.linearVelocity = new Vector2(moveInput * walkSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
        }

        // Vira o sprite conforme direção
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;
    }

    private void Climb()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, climbInput * climbSpeed);
        }
        else
        {
            rb.gravityScale = defaultGravityScale;
        }
    }

    private void CheckGround()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
    }

    private void UpdateAnimations()
    {
        if (animator == null) return;

        bool isMoving = Mathf.Abs(moveInput) > 0.01f;

        animator.SetBool("isRunning", isMoving && isRunning);
        animator.SetBool("isWalking", isMoving && !isRunning);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isClimbing", isClimbing);
        animator.SetFloat("verticalVelocity", rb.linearVelocity.y);
    }

    // Detecta entrada/saída de areas escalaveis (escadas, cordas, paredes)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Climbable"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Climbable"))
        {
            canClimb = false;
            isClimbing = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}