
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Jump")]
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Coyote Time")]
    public float coyoteTime = 0.15f;

    private float coyoteTimeCounter;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private float horizontal;
    private bool isGrounded;

    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento horizontal
        horizontal = 0f;

        if (Keyboard.current.leftArrowKey.isPressed ||
            Keyboard.current.aKey.isPressed)
        {
            horizontal = -1f;
        }

        if (Keyboard.current.rightArrowKey.isPressed ||
            Keyboard.current.dKey.isPressed)
        {
            horizontal = 1f;
        }

        

        // Orientación
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Comunicar al animator si se está en movimiento hacia izquierda o derecha
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Detectar suelo
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            0.2f,
            groundLayer
        );

        // Comunicar al animator si se está en el suelo
        animator.SetBool("IsGrounded", isGrounded);

        // Actualizar Coyote Time
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Saltar
        if (Keyboard.current.spaceKey.wasPressedThisFrame &&
            coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            coyoteTimeCounter = 0f;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            horizontal * speed,
            rb.linearVelocity.y
        );
    }

    // Dibujar detector de suelo en Scene
    void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    }
}



