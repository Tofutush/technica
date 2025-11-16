using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/* PROJECTILES
    This is an example script part of the debug minigame

    The purpose of it is to show you how to properly deal with input
    and use the provided MinigameManager.cs class
*/

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))] // This component must be attached to the GameObject for input to register
public class PlayerController : MonoBehaviour, MinigameSubscriber
{
    private Rigidbody2D rb;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 4f;
    private float nextFireTime = 0f;
    private int facingDirection = 1;
    
    [Header("Jumping")] 
    public float jumpForce = 7f; // how high player can rise
    private bool isJumping = false;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius=0.15f;

    private int jumpCount = 0;
    public int maxJumps = 2;
    public bool isGrounded;

    [Header("Movement")]
    public float moveSpeed = 5f;


    public PlayerInput playerInput;
    private bool inputBlocked = false;
    private Animator anim;

    void Start()
    {
        // Subscribes this class to the minigame manager. This gives access to the
        // 'OnMinigameStart()' and 'OnTimerEnd()' functions. Otherwise, they won't be called
        MinigameManagerTrue.Subscribe(this);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
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
        {
            // Cut upward velocity to zero so they stop rising
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            isJumping = false; // no more height checks until next jump
        }

        // When the player starts falling naturally, stop limiting
        //if (rb.linearVelocity.y <= 0)
          //  isJumping = false;
    }
    /*void OnInteract(InputValue val)
    {
        if (!MinigameManager.IsReady()) // IMPORTANT: Don't allow any input while the countdown is still occuring
            return;

        anim.SetTrigger("Throw");

        MinigameManager.SetStateToSuccess(); // Change the minigame state to "Success"
        MinigameManager.EndGame(); // End the minigame. Without this, the minigame would end when the timer finishes instead (still with success).
    }*/

    void OnMove(InputValue val)
    {
        if (!MinigameManagerTrue.IsReady()) // IMPORTANT: Don't allow any input while the countdown is still occuring
            return;

        Vector2 input = val.Get<Vector2>(); // Get the Vector2 that represents input

        if (Mathf.Abs(input.x) < 0.01f && isGrounded)
        {
            rb.linearVelocity = new Vector2(input.x * moveSpeed, rb.linearVelocity.y);
            return;
        }

        rb.linearVelocity = new Vector2(0,rb.linearVelocity.y); // 5f is a magic number; speed.



        //bool moving = input.x != 0 || input.y != 0;
        //anim.SetBool("isRunning", moving);
        if (input.x > 0.01f)
            facingDirection = 1;
        else if (input.x < -0.01f)
            facingDirection = -1;
        //transform.localScale = new Vector3(facingDirection, 1, 1);
    }

    void OnShoot(InputValue val)
    {
        Debug.Log("Shoot button pressed! " + MinigameManagerTrue.IsReady());
        if (!MinigameManagerTrue.IsReady())
            return;
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + 1f / fireRate;
        Shoot();
        anim.SetBool("isAttack", false); // * after shoot completes, resest isAttack status for animation
    }

    void Shoot()
    {
        //anim.SetTrigger("Throw");
        /*if (projectilePrefab == null || firePoint == null) return;

        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Projectile spawned!");*/
        anim.SetBool("isAttack", true);
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Cannot shoot: projectilePrefab or firePoint is null!");
            return;
        }

        // Spawn projectile slightly in front of firePoint to avoid colliding with player
        Vector3 spawnPosition = firePoint.position + firePoint.right * 0.5f; // tweak 0.5f if needed
        GameObject proj = Instantiate(projectilePrefab, spawnPosition, firePoint.rotation);

        // Make sure projectile is active
        if (!proj.activeSelf)
            proj.SetActive(true);

        // Ensure Rigidbody2D is set up correctly
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("Projectile has no Rigidbody2D!");
        }
        else
        {
            rb.linearVelocity = new Vector2(facingDirection, 0) * proj.GetComponent<Projectile>().speed; // speed, adjust as needed
        }

        // Debug log
        Debug.Log($"Projectile spawned at {spawnPosition} with velocity {rb.linearVelocity}");
    }
    
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

        if (jumpCount == 0 && !isGrounded) return;

        jumpCount++;
        isJumping = true;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        anim.SetBool("isJump", true);
    }

    public void OnMinigameStart()
    {
        Debug.Log("Minigame started!");
        // There isn't anything interesting that needs to happen in here for this example
    }

    public void OnTimerEnd()
    {
        // Timer has expired
        MinigameManagerTrue.SetStateToFailure();
        MinigameManagerTrue.EndGame();
    }
}
