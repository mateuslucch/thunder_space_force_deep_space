using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    [SerializeField] GameObject enemyLaserPrefab;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    //float projectileFiringPeriod = 0.5f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] bool doubleShoot = false;
    [SerializeField] float verticalOffset = 0;
    [SerializeField] float horizontalOffset = 0;
    [SerializeField] bool isABoss = false;

    [Header("Sound And Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip explosionEnemy;
    Animator myAnimator;


    [Header("Others")]
    [SerializeField] GameObject powerUps;
    bool enemyAlive = true;
    GameObject[] bossParts;

    // Use this for initialization
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        bossParts = GameObject.FindGameObjectsWithTag("BossParts");

    }

    // Update is called once per frame
    void Update()
    {
        if (isABoss == false)
        {
            if (enemyAlive == true)
            {
                CountDownAndShoot();
            }
        }
        else { return; }
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        if (doubleShoot == false)
        {
            GameObject enemyLaser = Instantiate(
                       enemyLaserPrefab,
                       transform.position,
                       Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        }
        else
        {
            GameObject enemyLaser1 = Instantiate(
                      enemyLaserPrefab,
                       new Vector3(transform.position.x - horizontalOffset, transform.position.y + verticalOffset, transform.position.z),
                      Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            enemyLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

            GameObject enemyLaser2 = Instantiate(
                      enemyLaserPrefab,
                       new Vector3(transform.position.x - horizontalOffset, transform.position.y - verticalOffset, transform.position.z),
                      Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            enemyLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            EnemyDiePath();

        }
    }

    private void EnemyDiePath()
    {
        CallPowerUp();
        enemyAlive = false; //interromper tiro
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        GetComponent<Collider2D>().enabled = false;
        AudioSource.PlayClipAtPoint(explosionEnemy, Camera.main.transform.position);

        myAnimator.SetBool("Explode", true);

        for (int i = 0; i < bossParts.Length; i++)
        {
            Destroy(bossParts[i]);

        }
        Destroy(gameObject, 0.5f);

        //ORIGINAL DE PARTICULAS
        /*
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        */
    }

    private void CallPowerUp() //chamado quando inimigo morre
    {
        float randomPercentage = Random.Range(0f, 101f);
        if (randomPercentage <= 30f)
        {
            GameObject power = Instantiate(
                      powerUps,
                      transform.position,
                      transform.rotation) as GameObject;
            power.GetComponent<Rigidbody2D>().velocity = new Vector2( 0, -2f);
        }
        else { }


    }

}
