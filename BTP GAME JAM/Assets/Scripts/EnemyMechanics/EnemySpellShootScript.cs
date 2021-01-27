using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellShootScript : MonoBehaviour
{
    Transform player;

    Rigidbody2D rb;

    float speed = 5f;

    Vector3 enemyShootDirection;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyShootDirection = (player.position - transform.position).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = enemyShootDirection;
        rb.velocity = new Vector2(enemyShootDirection.x, enemyShootDirection.y);
        Destroy(gameObject, 5);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerMovement.playerHealth -= 10;
            FindObjectOfType<AudioManager>().Play("PlayerHit");
            playerRender.color = new Color(1f, 0f, 0f);
            if (playerMovement.playerHealth == 0)
            {
                GameObject BlastPrefab = Instantiate(playerBlast, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(BlastPrefab, 2);
                Destroy(collision.gameObject);
                GameManager.instance.endgame();
            }

        }
        else if(collision.gameObject.CompareTag("Tile"))
        {
            Destroy(gameObject);
        }
    }*/
}
