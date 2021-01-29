using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellShootScript : MonoBehaviour
{
    public float speed = 0.1f;

    Vector3 moveDirection;

    TimeManager timeManager;

    public ParticleSystem enemyHit;

    public ParticleSystem enemyHit1;

    public ParticleSystem bulletHit;

    public ParticleSystem bossHit;

    // Start is called before the first frame update
    void Start()
    {
        CalculateDirectiontoMouse();
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = moveDirection;
        transform.position = transform.position + moveDirection * speed * Time.deltaTime;
        Destroy(gameObject, 1f);
    }

    public void CalculateDirectiontoMouse()
    {
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "FlyingEnemy" && FlyingEnemy.instance.ShieldOn == false)
        {
            Instantiate(bulletHit, collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            if (collision.gameObject.GetComponent<EnemyHealth>().getHP() <= 0)
            {
                timeManager.SlowMotion();
            }
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Hit");
        }
        else if (collision.gameObject.tag == "BlobShield")
        {
            Instantiate(enemyHit, collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Hit");
        }
        else if (collision.gameObject.tag == "FlyingEnemyShield")
        {
            Instantiate(enemyHit, collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Hit");
        }
        else if (collision.gameObject.tag == "FlyingEnemyBullet")
        {
            Instantiate(bulletHit, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "CanonStone")
        {
            Instantiate(enemyHit1, collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            collision.gameObject.GetComponent<EnemyHealth>().animator.SetBool("Break", true);
            Destroy(collision.gameObject, 0.5f);
            FindObjectOfType<AudioManager>().Play("Blast");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Canon"))
        {
            Instantiate(enemyHit1, collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            if (collision.gameObject.GetComponent<EnemyHealth>().getHP() <= 0)
            {
                timeManager.SlowMotion();
            }
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Hit");
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Instantiate(bossHit, collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            if (collision.gameObject.GetComponent<EnemyHealth>().getHP() <= 0)
            {
                timeManager.SlowMotion();
            }
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Hit");
        }
    }
}
