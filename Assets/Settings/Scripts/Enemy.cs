using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public GameObject projectilePrefab;
    public float projectileCooldown;
    public Transform firePoint;

    public float patrolDistance;

    private bool movingRight = true;
    private float leftLimit;
    private float rightLimit;

    private float timerCurrent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftLimit = transform.position.x - patrolDistance;
        rightLimit = transform.position.x + patrolDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            ChoiceUI.Instance.showChoice(this);
        }
        else
        {
            if (movingRight)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
                if (transform.position.x >= rightLimit)
                {
                    movingRight = false;
                }
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
                if (transform.position.x <= leftLimit)
                {
                    movingRight = true;
                }
            }

            if (timerCurrent <= 0)
            {
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
                    rb.linearVelocity = new Vector2(movingRight ? 1 : -1, 0) * proj.GetComponent<Projectile>().speed; // speed, adjust as needed
                }

                timerCurrent = projectileCooldown;
            }
            else
            {
                timerCurrent -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("damage taken!");
    }
}
