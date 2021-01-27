using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobScript : MonoBehaviour
{
    public float speed = 5f;
    public bool movingleft = true;
    public float ray_distance = 1f;

    public Transform GroundDetect;

    public bool ShieldOn = true;

    public static BlobScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(gameObject.transform.childCount == 2)
        {
            ShieldOn = false;
        }
        else
        {
            ShieldOn = true;
        }
        if (ShieldOn == true)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            RaycastHit2D groundinfo = Physics2D.Raycast(GroundDetect.position, Vector2.down, ray_distance);
            if (groundinfo.collider == false)
            {
                if (movingleft == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingleft = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingleft = true;
                }
            }
        }
    }
}
