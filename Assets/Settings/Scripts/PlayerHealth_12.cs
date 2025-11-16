using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public PlayerStats player;
    public TextMeshProUGUI alltext;
    private Stats stat;


    [Header("Player Stats")]
    //public int maxHealth = 100;
    private int currentHealth;


    void Start()
    {
        currentHealth = PlayerStats.Health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage - PlayerStats.Defense;
        // alltext.text = ("Player took " + damage + " damage! HP left: " + currentHealth);
        Debug.Log("Player took " + damage + " damage! HP left: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        //stat = currentHealth;
        //player.health = stat;
        player.health.Buff(-(damage - PlayerStats.Defense));
        player.updateText();
    }

    void Die()
    {
        Debug.Log("Player defeated!");
        gameObject.SetActive(false);
        MinigameManager.SetStateToFailure();
        MinigameManager.EndGame();
        // You could trigger a game over screen here instead
    }
}
