using UnityEngine;
using UnityEngine.UI;

public class BlobScript : MonoBehaviour
{
    public float speed = 5f;
    public bool movingleft = true;
    public float ray_distance = 1f;

    public Transform GroundDetect;

    public GameObject shield;

    public float shieldcooldowntime = 5f;

    public float nextshieldspawn;

    public bool isCooldown = true;

    public bool ShieldOn = true;

    public static BlobScript instance;

    public GameObject ShieldHUD;

    EnemyWaveSpawner enemySpawner;
    private float nextpositionChangetime;

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

    private void Start()
    {
        nextshieldspawn = shieldcooldowntime;
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemyWaveSpawner>();
    }

    void Update()
    {
        if (ShieldOn == true)
        {
            if (nextpositionChangetime < Time.time)
            {
                Transform randomspawnPoint = enemySpawner.Blobspawnpoints[Random.Range(0, enemySpawner.Blobspawnpoints.Length)];
                transform.position = randomspawnPoint.position;
                nextpositionChangetime = Time.time + 2;
            }
            BlobMove();
        }
        else
        {
            if (nextshieldspawn <= 0)
            {
                ShieldOn = true;
                nextshieldspawn = 5;
                shield.gameObject.SetActive(true);
                shield.GetComponent<EnemyHealth>().setHP(10);
                shield.GetComponent<EnemyHealth>().animator.SetBool("ShieldDestroyed", false);
            }
            else
            {
                nextshieldspawn -= Time.deltaTime;
            }
        }
    }

    private void BlobMove()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        RaycastHit2D groundinfo = Physics2D.Raycast(GroundDetect.position, Vector2.down, ray_distance);
        if (groundinfo.collider == false || groundinfo.collider.tag == "Lava")
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
