﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float playerHealth = 100;

    float xMoveInput;
    public float playermoveSpeed = 10;
    public float jumpstrength = 10;

    public bool isGrounded;
    public float checkRadius;
    public LayerMask whatisGround;
    public Transform GroundCheck;

    Rigidbody2D rb;

    public GameObject bulletPrefab;
    public GameObject firepoint;

    public static Player instance;

    public Slider health;

    public Image healthFill;

    public Gradient healthbargradient;

    public Text PercentText;

    public float repelForcce = 15;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.gameplay == true && GameManager.instance.gameOver == false && GameManager.instance.gamewin == false)
        {
            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                rb.velocity = Vector2.up * jumpstrength;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }

            health.value = playerHealth;
            healthFill.color = healthbargradient.Evaluate(health.normalizedValue);
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                PercentText.text = playerHealth + " % ";
            }
        }

        if(playerHealth <= 0)
        {
            GameManager.instance.LostGAme();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameplay == true && GameManager.instance.gameOver == false && GameManager.instance.gamewin == false)
        {
            isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, whatisGround);

            xMoveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(xMoveInput * playermoveSpeed, rb.velocity.y);
        }
    }

    void Shoot()
    {
        GameObject spell = Instantiate(bulletPrefab, firepoint.transform.position, Quaternion.identity);
        Rigidbody2D spellrb = spell.GetComponent<Rigidbody2D>();
        spellrb.AddForce(Vector2.right * playermoveSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BlobShield")
        {
            playerHealth -= 2;
            rb.AddForce(transform.up * repelForcce, ForceMode2D.Impulse);
        }
    }
}
