using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBoss : MonoBehaviour
{

    [Header("Shooting")]
    [SerializeField] GameObject enemySingleLaserPrefab;
    [SerializeField] GameObject laserShowerPrefab;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    //float projectileFiringPeriod = 0.5f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float verticalOffset = 0;
    [SerializeField] float horizontalOffset = 0;
    [SerializeField] GameObject leftGun;
    [SerializeField] GameObject rightGun;

    [Header("SpreadShooting")]
    [SerializeField] float xspreadOffset = 0;
    [SerializeField] float yspreadOffset = 0;
    [SerializeField] float speedFactor = 2;
    [SerializeField] float maxAngle = 90;
    [SerializeField] float minAngle = 90;
    [SerializeField] float angleInterval = 5;
    [SerializeField] float spreadInterval = 5;

    [Header("Others")]
    [SerializeField] float changeAttackTime = 10f;
    [SerializeField] float typeShot = 0f;

    // Use this for initialization
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
        CountDownAndChangeAttack();

    }

    //troca de tipo de ataque
    private void CountDownAndChangeAttack()
    {
        changeAttackTime -= Time.deltaTime;
        if (changeAttackTime <= 0)
        {
            if (typeShot == 0)
            {
                changeAttackTime = 8f;
                typeShot = 1f;
            }
            else if (typeShot == 1)
            {
                changeAttackTime = 10f;
                typeShot = 0f;
            }
        }
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if (typeShot == 0f)
        {
            if (shotCounter <= 0f)
            {
                FireSimpleAttack();
                shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
        }
        if (typeShot == 1f)
        {
            if (shotCounter <= 0f)
            {
                LaserShower();
                shotCounter = spreadInterval;
            }
        }

    }

    private void FireSimpleAttack()
    {
        GameObject enemyLaser1 = Instantiate(
                  enemySingleLaserPrefab,
                   leftGun.transform.position,
                  Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        enemyLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

        GameObject enemyLaser2 = Instantiate(
                  enemySingleLaserPrefab,
                   rightGun.transform.position,
                  Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        enemyLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

    }

    private void LaserShower()
    {
        for (float i = minAngle; i <= maxAngle; i = i + angleInterval)
        {

            var x = Mathf.Sin(i * Mathf.Deg2Rad) * speedFactor;
            var y = Mathf.Cos(i * Mathf.Deg2Rad) * speedFactor;

            GameObject enemyLaser = Instantiate(
                          laserShowerPrefab,
                          new Vector3(transform.position.x - xspreadOffset, transform.position.y + yspreadOffset, transform.position.z),
                          Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
        }

    }
}
