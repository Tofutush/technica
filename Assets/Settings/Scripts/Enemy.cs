using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;

    public float patrolDistance;

    private bool movingRight = true;
    private float leftLimit;
    private float rightLimit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftLimit = transform.position.x - patrolDistance;
        rightLimit = transform.position.x + patrolDistance;
        Debug.Log("right limit:");
        Debug.Log(rightLimit);
        Debug.Log("left limit:");
        Debug.Log(leftLimit);
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
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                Debug.Log(transform.position.x);
                if (transform.position.x >= rightLimit)
                {
                    movingRight = false;
                }
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                Debug.Log(transform.position.x);
                if (transform.position.x <= leftLimit)
                {
                    movingRight = true;
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("damage taken!");
    }
}
