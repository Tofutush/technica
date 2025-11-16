using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    public float attackCoolDown;
    private float attackCounter;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayer;
    public int damage;

    private PlayerInput playerInput;
    private InputAction hitAction;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // Get the Hit action from the current action map
        hitAction = playerInput.currentActionMap.FindAction("Hit");

        // Subscribe to the action event
        hitAction.performed += OnHitPerformed;
    }

    // Update is called once per frame
    void Update()
    {
        attackCounter -= Time.deltaTime;
    }

    void OnHitPerformed(InputAction.CallbackContext context)
    {
        Debug.Log(attackCounter);
        if (attackCounter <= 0)
        {
            Debug.Log("pressed left mouse");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
            Debug.Log(enemiesToDamage.Length);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
            }
            attackCounter = attackCoolDown;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
