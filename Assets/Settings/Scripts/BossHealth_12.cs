using UnityEngine;
using TMPro;
public class BossHealth : MonoBehaviour
{
    [Header("Boss Stats")]
    public TextMeshProUGUI alltext;
    public int maxHealth = 100;
    public int CurrentHealth => currentHealth;
    public int currentHealth;
    

    public GameObject explosionPrefab;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on Boss!");
        }
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        alltext.text = ("Boss took " + damage+ " damage! HP left: " + currentHealth);
        Debug.Log("Boss took " + damage+ " damage! HP left: " + currentHealth);

        // Trigger hit animation
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");

        // Play death animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Optional: spawn explosion
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Disable collider so it stops taking hits
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Destroy after the animation finishes (adjust 2f if needed)
        Destroy(gameObject, 2f);

        MinigameManager.SetStateToSuccess();
        MinigameManager.EndGame();
    }
    
}