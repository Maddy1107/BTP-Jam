using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonScript : MonoBehaviour
{
    Transform player;

    Vector3 enemyShootDirection;

    float NextFire;

    public GameObject stone;

    public GameObject canonFirePoint;

    public float firerate = 10f;

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        enemyShootDirection = (player.position - transform.position).normalized;
        transform.up = -enemyShootDirection;
        if (NextFire < Time.time)
        {
            EnemyShoot();
            NextFire = Time.time + firerate;
        }
    }

    private void EnemyShoot()
    {
        Instantiate(stone, canonFirePoint.transform.position, Quaternion.identity);
    }
}
