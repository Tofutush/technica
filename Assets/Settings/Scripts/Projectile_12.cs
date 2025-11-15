using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rb.linearVelocity = transform.right * speed;
        Invoke(nameof(Disable), lifetime);
        Debug.Log("Velocity set to: " + rb.linearVelocity);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object we hit has a BossHealth component
        BossHealth boss = collision.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeDamage(PlayerStats.Attack); // deal 10 damage (you can change this)
            Disable();           // deactivate projectile after hit
        }
    }
}
