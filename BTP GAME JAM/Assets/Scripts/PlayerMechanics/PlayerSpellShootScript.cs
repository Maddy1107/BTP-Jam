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
        Destroy(gameObject, 2f);
    }

    public void CalculateDirectiontoMouse()
    {
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && FlyingEnemy.instance.ShieldOn == false)
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            if (collision.gameObject.GetComponent<EnemyHealth>().getHP() <= 0)
            {
                timeManager.SlowMotion();
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "BlobShield")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            if (collision.gameObject.GetComponent<EnemyHealth>().getHP() <= 0)
            {
                //Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "FlyingEnemyShield")
        {
            Instantiate(enemyHit, collision.gameObject.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            if (collision.gameObject.GetComponent<EnemyHealth>().getHP() <= 0)
            {
                //Destroy(collision.gameObject, 1);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "FlyingEnemyBullet")
        {
            //ParticleSystem enemyHitPrefab = Instantiate(enemyHit, collision.gameObject.transform.position, Quaternion.identity);
            //Destroy(enemyHitPrefab, 1);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "CanonStone")
        {
            ParticleSystem enemyHitPrefab = Instantiate(enemyHit1, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(enemyHitPrefab, 1);
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            timeManager.SlowMotion();
            collision.gameObject.GetComponent<EnemyHealth>().animator.SetBool("Break", true);
            Destroy(collision.gameObject, 0.5f);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Canon"))
        {
            ParticleSystem enemyHitPrefab = Instantiate(enemyHit1, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(enemyHitPrefab, 1);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            timeManager.SlowMotion();
            Destroy(gameObject);
        }
    }
}
