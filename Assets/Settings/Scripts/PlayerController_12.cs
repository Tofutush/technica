using UnityEngine;
using UnityEngine.InputSystem;

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
    //private Animator anim;

    void Start()
    {
        // Subscribes this class to the minigame manager. This gives access to the
        // 'OnMinigameStart()' and 'OnTimerEnd()' functions. Otherwise, they won't be called
        MinigameManagerTrue.Subscribe(this);
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
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
        rb.linearVelocity = input * 5f; // 5f is a magic number; speed.

        //bool moving = input.x != 0 || input.y != 0;
        //anim.SetBool("isRunning", moving);
    }
    void OnShoot(InputValue val)
    {
        Debug.Log("Shoot button pressed!");
        if (!MinigameManagerTrue.IsReady())
            return;
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + 1f / fireRate;
        Shoot();
    }

    void Shoot()
    {
        //anim.SetTrigger("Throw");
        if (projectilePrefab == null || firePoint == null) return;

        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Projectile spawned!");
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
