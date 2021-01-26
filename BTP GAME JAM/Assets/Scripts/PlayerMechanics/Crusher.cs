using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    Rigidbody2D rb;
    public float bounceforce;
    public int givenDamage;
    TimeManager timeManager;

    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CrushHurter")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(givenDamage);
            timeManager.SlowMotion();
            rb.AddForce(transform.up * bounceforce, ForceMode2D.Impulse);
        }
    }
}
