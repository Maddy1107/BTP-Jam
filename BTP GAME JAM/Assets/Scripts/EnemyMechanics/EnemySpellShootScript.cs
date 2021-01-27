using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellShootScript : MonoBehaviour
{
    Transform player;

    Rigidbody2D rb;

    float speed = 5f;

    Vector3 enemyShootDirection;

    public ParticleSystem playerHit;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyShootDirection = (player.position - transform.position).normalized * speed;
    }

    void Update()
    {
        transform.up = enemyShootDirection;
        rb.velocity = new Vector2(enemyShootDirection.x, enemyShootDirection.y);
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ParticleSystem playerHitPrefab = Instantiate(playerHit, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(playerHitPrefab, 1);
            Destroy(gameObject);
            Player.instance.playerHealth -= 10;
            if (Player.instance.playerHealth == 0)
            {
                Destroy(collision.gameObject);
            }

        }
    }
}
