using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // General
    private Rigidbody2D rb;

    // Move
    private float horizontalInput;
    private float moveSpeed = 8f;

    // Jump
    private float jumpPower = 27f;

        // Variable Jump Height
        private float jumpCutMultiplier = 0.5f;
        private bool isJumping = false;
    
        // Jump Buffering
        private float jumpBufferTime = 0.08f;
        private float jumpBufferCounter;

        // Coyote Time
        private float coyoteTime = 0.08f;
        private float coyoteCounter;

        // Apex Modifiers
        private float apexThreshold = 0.1f;       // prędkość Y, poniżej której uznajemy, że jesteśmy blisko szczytu
        private float apexGravityScale = 0.5f;   // grawitacja w okolicy szczytu (mniejsza = bardziej "zawieszone")
        private float normalGravityScale = 7f;    // normalna grawitacja (przy wznoszeniu/opadaniu)

        // Ground Check
        [SerializeField] private Transform groundCheck;
        private LayerMask groundLayer;
        private float groundCheckSizeX = 0.9f;
        private float groundCheckSizeY = 0.1f;

    // General
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        groundLayer = LayerMask.GetMask("Ground");
        rb.gravityScale = normalGravityScale;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        ApplyApexModifier();
    }

    // Movement
    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        // Odliczanie bufora skoku
        if (jumpBufferCounter > 0f)
            jumpBufferCounter -= Time.fixedDeltaTime;

        // Coyote time — reset licznika gdy na ziemi, odliczanie gdy w powietrzu
        if (IsGrounded())
            coyoteCounter = coyoteTime;
        else if (coyoteCounter > 0f)
            coyoteCounter -= Time.fixedDeltaTime;

        // Wykonanie skoku
        if (jumpBufferCounter > 0f && coyoteCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            jumpBufferCounter = 0f;
            coyoteCounter = 0f;
            isJumping = true;
        }

        // Zakończenie skoku po wylądowaniu
        if (isJumping && IsGrounded() && rb.linearVelocity.y <= 0f)
        {
            isJumping = false;
        }
    }

    private void ApplyApexModifier()
    {
        // Działa tylko gdy postać jest w powietrzu
        if (IsGrounded())
        {
            rb.gravityScale = normalGravityScale;
            return;
        }

        float verticalVelocity = Mathf.Abs(rb.linearVelocity.y);

        // Blisko szczytu skoku → zmniejsz grawitację, żeby postać "zawisła" w powietrzu
        if (verticalVelocity < apexThreshold)
        {
            rb.gravityScale = apexGravityScale;
        }
        else
        {
            rb.gravityScale = normalGravityScale;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckSizeX, groundCheckSizeY), 0f, groundLayer);
    }

    // Read Input
    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        // Puszczenie przycisku w trakcie wznoszenia → skróć skok
        else if (context.canceled && isJumping && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
            isJumping = false;
        }
    }

    // Gizmos
    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(groundCheckSizeX, groundCheckSizeY, 0f));
    }
}