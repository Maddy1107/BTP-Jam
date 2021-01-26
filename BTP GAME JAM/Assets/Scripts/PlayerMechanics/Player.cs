using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpstrength;
        }
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, whatisGround);

        xMoveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xMoveInput * playermoveSpeed, rb.velocity.y);
    }

    void Shoot()
    {
        GameObject spell = Instantiate(bulletPrefab, firepoint.transform.position, Quaternion.identity);
        Rigidbody2D spellrb = spell.GetComponent<Rigidbody2D>();
        spellrb.AddForce(Vector2.right * playermoveSpeed, ForceMode2D.Impulse);
    }
}
