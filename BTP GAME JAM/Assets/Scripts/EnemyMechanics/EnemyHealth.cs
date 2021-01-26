using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHP;
    float currentHP;

    public Animator animator;

    void Start()
    {
        currentHP = enemyHP;
    }

    void Update()
    {
        if(currentHP <= 0)
        {
            //Destroy(transform.parent.gameObject, 0.5f);
            animator.SetBool("Destroyed", true);
            Destroy(gameObject,1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }
}
