using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private int damage = 20;     // how much damage to deal
    private float lifetime = 15f; // destroy after a few seconds
    public GameObject explosionPrefab;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"BossAttack hit: {collision.gameObject.name} (tag={collision.gameObject.tag})");
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            Debug.Log("BossAttack -> PlayerHealth found. Dealing damage.");
            player.TakeDamage(damage-PlayerStats.Defense);
            Destroy(gameObject, lifetime); // remove attack after hitting
            return;
        }
    }
}
