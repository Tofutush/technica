using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour, MinigameSubscriber
{
    private Rigidbody2D rb;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 4f;
    private float nextFireTime = 0f;
    private int facingDirection = 1;
<<<<<<< Updated upstream
    
    [Header("Jumping")] 
    public float jumpForce = 7f; // how high player can rise
=======

    [Header("Jumping")]
    public float jumpVelocity = 7f;      // velocity applied when jumping
>>>>>>> Stashed changes
    private bool isJumping = false;

    public LayerMask groundLayer;
    public Transform groundCheck;
<<<<<<< Updated upstream
    public float groundCheckRadius=0.15f;

    private int jumpCount = 0;
    public int maxJumps = 2;
    public bool isGrounded;

    [Header("Movement")]
    public float moveSpeed = 5f;


    public PlayerInput playerInput;
    private bool inputBlocked = false;
    private Animator anim;
=======
    public float groundCheckRadius = 0.15f;

    private int jumpCount = 0;           // jumps used
    public int maxJumps = 2;             // 1 = single jump, 2 = double jump
    private bool isGrounded;

    [Header("Movement")]
    public float moveSpeed = 5f;
>>>>>>> Stashed changes

    void Start()
    {
        MinigameManagerTrue.Subscribe(this);
        rb = GetComponent<Rigidbody2D>();
<<<<<<< Updated upstream
        anim = GetComponent<Animator>();
=======
>>>>>>> Stashed changes
    }

    void Update()
    {
<<<<<<< Updated upstream
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            jumpCount = 0;
            isJumping = false;
        }

        if (isJumping && rb.linearVelocity.y > jumpForce)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position - Vector3.down, groundCheckRadius + 2, groundLayer);

    }

    /*void Update()
    {
        if (isJumping && transform.position.y >= jumpStartY + maxJumpHeight)
=======
        // ---------------- Ground Check ----------------
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
>>>>>>> Stashed changes
        {
            jumpCount = 0;       // reset jumps when grounded
            isJumping = false;
        }

<<<<<<< Updated upstream
        // When the player starts falling naturally, stop limiting
        //if (rb.linearVelocity.y <= 0)
          //  isJumping = false;
=======

        // ---------------- Stop upward motion if needed ----------------
        if (isJumping && rb.linearVelocity.y > jumpVelocity)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
        }

        

        isGrounded = Physics2D.OverlapCircle(groundCheck.position - Vector3.down, groundCheckRadius + 2, groundLayer);
Debug.DrawRay(groundCheck.position, Vector3.down * 0.2f, Color.red); // visual ray


>>>>>>> Stashed changes
    }

    // ---------------- Movement ----------------
    void OnMove(InputValue val)
    {
        if (!MinigameManagerTrue.IsReady()) return;

        Vector2 input = val.Get<Vector2>();

        if (Mathf.Abs(input.x) < 0.01f && isGrounded)
        {
            Debug.Log("no movement");
            // Stop horizontal movement when no input and grounded
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

<<<<<<< Updated upstream
        Vector2 input = val.Get<Vector2>(); // Get the Vector2 that represents input

        if (Mathf.Abs(input.x) < 0.01f && isGrounded)
        {
            rb.linearVelocity = new Vector2(input.x * moveSpeed, rb.linearVelocity.y);
            return;
        }

        rb.linearVelocity = new Vector2(0,rb.linearVelocity.y); // 5f is a magic number; speed.


=======
        rb.linearVelocity = new Vector2(input.x * moveSpeed, rb.linearVelocity.y);
>>>>>>> Stashed changes

        if (input.x > 0.01f)
            facingDirection = 1;
        else if (input.x < -0.01f)
            facingDirection = -1;
        //transform.localScale = new Vector3(facingDirection, 1, 1);
    }

<<<<<<< Updated upstream
=======
    // ---------------- Shooting ----------------
>>>>>>> Stashed changes
    void OnShoot(InputValue val)
    {
        if (!MinigameManagerTrue.IsReady()) return;
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + 1f / fireRate;
        Shoot();
        anim.SetBool("isAttack", false); // * after shoot completes, resest isAttack status for animation
    }

    void Shoot()
    {
<<<<<<< Updated upstream
        //anim.SetTrigger("Throw");
        /*if (projectilePrefab == null || firePoint == null) return;

        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Projectile spawned!");*/
        anim.SetBool("isAttack", true);
=======
>>>>>>> Stashed changes
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Cannot shoot: missing projectilePrefab or firePoint!");
            return;
        }

        Vector3 spawnPos = firePoint.position + firePoint.right * 0.5f;
        GameObject proj = Instantiate(projectilePrefab, spawnPos, firePoint.rotation);

        if (!proj.activeSelf) proj.SetActive(true);

        Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
        if (projRb != null)
        {
            projRb.linearVelocity = new Vector2(facingDirection, 0) * proj.GetComponent<Projectile>().speed;
        }
    }
<<<<<<< Updated upstream
    
    void OnJump(InputValue val)
    {
        if(!MinigameManagerTrue.IsReady()) return;
        if (val.isPressed) Jump();
    }    

    void Jump()
    {
        //isJumping = true;
        //jumpStartY = transform.position.y;
        if (jumpCount >= maxJumps) return;

=======

    // ---------------- Jumping ----------------
    void OnJump(InputValue val)
    {
        Debug.Log("we have arrived at the OnJump method");
        if (!MinigameManagerTrue.IsReady()) return;

        if (val.isPressed) Jump();
    }

    void Jump()
    {
        // Cannot exceed max jumps
        if (jumpCount >= maxJumps) return;

        // First jump can only happen when grounded
>>>>>>> Stashed changes
        if (jumpCount == 0 && !isGrounded) return;

        jumpCount++;
        isJumping = true;
<<<<<<< Updated upstream
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        anim.SetBool("isJump", true);
=======

        // Apply jump velocity
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
>>>>>>> Stashed changes
    }

    // ---------------- Gizmos for GroundCheck ----------------
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    // ---------------- Minigame events ----------------
    public void OnMinigameStart()
    {
        // Called automatically by Minigame system
    }

    public void OnTimerEnd()
    {
        MinigameManagerTrue.SetStateToFailure();
        MinigameManagerTrue.EndGame();
    }
}
