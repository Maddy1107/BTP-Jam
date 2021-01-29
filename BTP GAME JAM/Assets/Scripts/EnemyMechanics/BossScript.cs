using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int numberofStones;

    public float radius = 5f;

    public GameObject stones;

    public float firespeed = 5f;

    public GameObject EnemyFirepoint;
    Transform player;
    public float enemyFirerate = 4f;
    public float NextFire;

    public float dashSpeed;
    public float nextdashTime;
    public float dashstarttime;
    public float dashRate = 20;

    public bool isCooldown = true;

    Rigidbody2D rb;

    EnemyWaveSpawner enemySpawner;
    private float nextpositionChangetime;

    private void Start()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemyWaveSpawner>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            if(dashstarttime > 0)
            {
                dashstarttime -= Time.deltaTime;
                rb.velocity = (player.position - transform.position).normalized * dashSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
                
            if (nextdashTime < Time.time)
            {
                dashstarttime = 0.1f;
                nextdashTime = Time.time + dashRate;
            }

            if (nextpositionChangetime < Time.time)
            {
                Transform randomspawnPoint = enemySpawner.Bossspawnpoints[Random.Range(0, enemySpawner.Bossspawnpoints.Length)];
                transform.position = randomspawnPoint.position;
                nextpositionChangetime = Time.time + 6;

                Vector3 bossScale = transform.localScale;

                if(transform.position.x < 0)
                {
                    bossScale.x = -1;
                }
                else
                {
                    bossScale.x = 1;
                }
                transform.localScale = bossScale;
            }
        }

        if (NextFire < Time.time)
        {
            EnemyShoot(numberofStones);
            NextFire = Time.time + enemyFirerate;
        }

    }

    private void EnemyShoot(int numberOfStones)
    {
        float angleStep = 360f / numberOfStones;
        float angle = 0f;

        for (int i = 0; i <= numberOfStones - 1; i++)
        {

            float projectileDirXposition = EnemyFirepoint.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = EnemyFirepoint.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = projectileVector - new Vector2(EnemyFirepoint.transform.position.x, EnemyFirepoint.transform.position.y)
                .normalized * firespeed;

            var proj = Instantiate(stones, EnemyFirepoint.transform.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
            Destroy(proj, 3);

            angle += angleStep;
        }
    }
}
