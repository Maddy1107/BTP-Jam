using System;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public GameObject EnemyFirepoint;
    Transform player;
    public float enemyspeed = 6f;
    public float enemyFirerate = 4f;
    float NextFire;

    Animator animator;

    public GameObject blades;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            EnemyMove();
        }
            
    }

    private void EnemyMove()
    {
        if (Vector2.Distance(transform.position, player.position) > 5)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyspeed * Time.deltaTime);
        }
        else
        {
            if (NextFire < Time.time)
            {
                EnemyShoot();
                NextFire = Time.time + enemyFirerate;
            }
        }
        float avoidSpeed = enemyspeed / 2f;
        float avoidRadius = 1f;

        Vector2 avoidDir = Vector2.zero;
        float count = 0f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, avoidRadius);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<FlyingEnemy>() != null && hit.transform != transform)
            {
                Vector2 difference = transform.position - hit.transform.position;
                difference = difference.normalized / Mathf.Abs(difference.magnitude);

                avoidDir += difference;
                count++;
            }
        }

        if (count > 0)
        {
            avoidDir /= count;
            avoidDir = avoidDir.normalized * avoidSpeed;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)avoidDir, avoidSpeed * Time.deltaTime);
        }
    }

    private void EnemyShoot()
    {
        Instantiate(blades, EnemyFirepoint.transform.position, Quaternion.identity);
    }
}
