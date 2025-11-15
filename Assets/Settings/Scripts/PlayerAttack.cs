using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{

    public float attackCoolDown;
    private float attackCounter;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayer;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        if (attackCounter <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                attackCounter = attackCoolDown;
            }
        }
        else
        {
            attackCounter -= Time.deltaTime;
        }
    }
}
