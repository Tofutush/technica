using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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
        CancelInvoke(); // important for pooling!

        rb.linearVelocity = transform.right * speed; // <- FIXED

        Invoke(nameof(Disable), lifetime);
        Debug.Log("Velocity set to: " + rb.linearVelocity);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if (health != null)
        {
            Debug.Log("Hit player");
            health.TakeDamage(10);
            Disable();
        }
    }
}
