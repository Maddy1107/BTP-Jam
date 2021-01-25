using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float xMoveInput;
    public float playermoveSpeed = 10;
    public float jumpstrength = 10;

    public bool isMoving = false;

    public bool isGrounded;
    public float checkRadius;
    public LayerMask whatisGround;
    public Transform GroundCheck;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpstrength;
        }

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, whatisGround);

        xMoveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xMoveInput * playermoveSpeed, rb.velocity.y);
    }
}
