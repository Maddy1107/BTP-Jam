using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHP;
    public float currentHP;

    public Animator animator;

    void Start()
    {
        currentHP = enemyHP;
    }

    void Update()
    {
        if(currentHP <= 0)
        {
            if (gameObject.tag == "Enemy")
            {
                animator.SetBool("Destroyed", true);
                Destroy(gameObject, 1);
            }
            else if(gameObject.tag == "CrushHurter")
            {
                Destroy(transform.parent.gameObject);
            }
            else if (gameObject.tag == "BlobShield")
            {
                Destroy(gameObject);
                BlobScript.instance.ShieldOn = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }

    public float getHP()
    {
        return currentHP;
    }
}
