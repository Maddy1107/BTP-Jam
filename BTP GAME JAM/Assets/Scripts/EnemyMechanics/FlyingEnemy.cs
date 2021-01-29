using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public GameObject EnemyFirepoint;
    Transform player;
    public float enemyspeed = 6f;
    public float enemyFirerate = 2f;
    float NextFire;

    public GameObject blades;

    public bool ShieldOn = true;
    public GameObject shield;

    public float shieldcooldowntime = 5f;

    public float nextshieldspawn;

    public bool isCooldown = true;

    public static FlyingEnemy instance;

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
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemyWaveSpawner>();
        nextshieldspawn = shieldcooldowntime;
    }

    void Update()
    {
        if (GameManager.instance.gameplay == true && GameManager.instance.gameOver == false && GameManager.instance.gamewin == false)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                if (ShieldOn == true)
                {
                    if (nextpositionChangetime < Time.time)
                    {
                        Transform randomspawnPoint = enemySpawner.Blobspawnpoints[Random.Range(0, enemySpawner.Blobspawnpoints.Length)];
                        transform.position = randomspawnPoint.position;
                        nextpositionChangetime = Time.time + 2;
                    }
                    EnemyMove();
                }
                else
                {
                    if (nextshieldspawn <= 0)
                    {
                        ShieldOn = true;
                        nextshieldspawn = shieldcooldowntime;
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
        }
            
    }

    private void EnemyMove()
    {
        if (Vector2.Distance(transform.position, player.position) > 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyspeed * Time.deltaTime);
        }
        if (NextFire < Time.time)
        {
            EnemyShoot();
            NextFire = Time.time + enemyFirerate;
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
        FindObjectOfType<AudioManager>().Play("Shoot");
    }
}
