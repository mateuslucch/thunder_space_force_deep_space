using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooter : MonoBehaviour
{
    [Header("Player Sounds")]
    
    [SerializeField] AudioClip shootSound;
    [Range(0f, 1f)] [SerializeField] float shootVolume = 0.5f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab; //variavel gameobject, libera no unity pra linkar com o laser
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.01f;
    
    [SerializeField] bool multipleShots = false;
    [SerializeField] float shootAngle = 90f;
    [SerializeField] float shotCounter = 10f;

    [Header("Guns")]
    [SerializeField] GameObject shipGun;
    [SerializeField] GameObject shipLeftGun;
    [SerializeField] GameObject shipRightGun;

    [Header("Others")]
    Coroutine firingCoroutine;
    [SerializeField] int changingScenario = 0;

    private void Update()
    {
        if (changingScenario == 0)
        {
            Fire();
        }
        else
        {
            return;
        }

        if (multipleShots == true)
        {
            CountDownMultipleShoot();
        }
    }
    public void ShipControl(int numberCondition) //altera as condições no update
    {
        StopCoroutine(firingCoroutine);        
        
    }
    private void Fire() //FIRE LASERS!!
    {

        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            //StopAllCoroutines(); //para todas as coroutines, TODAS MESMO
            StopCoroutine(firingCoroutine); //para só a "firingCoroutine"
        }

    }
    private void CountDownMultipleShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            multipleShots = false;
            shotCounter = 10f;
        }
    }

    public void KindShoot()//??????
    {

    }
    public void MultipleShots() //chamado pelo PowerUp.cs
    {
        shotCounter = 10f;
        multipleShots = true;
    }

    private void StartMultipleShots()
    {
        multipleShots = true;
    }

    public void StopMultipleShots()
    {
        multipleShots = false;
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            if (multipleShots != false)
            {
                GameObject laser1 = Instantiate( //Esquerda
                       laserPrefab,
                       shipLeftGun.transform.position,
                       transform.rotation) as GameObject;

                laser1.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootAngle, projectileSpeed);

                GameObject laser = Instantiate( //tiro central
                       laserPrefab,
                       shipGun.transform.position,
                       transform.rotation) as GameObject;

                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                GameObject laser2 = Instantiate( //Direita
                       laserPrefab,
                       shipRightGun.transform.position,
                       transform.rotation) as GameObject;

                laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(shootAngle, projectileSpeed);

                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
                yield return new WaitForSeconds(projectileFiringPeriod);
            }

            else
            {
                GameObject laser = Instantiate(
                       laserPrefab,
                       shipGun.transform.position,
                       transform.rotation) as GameObject;
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                yield return new WaitForSeconds(projectileFiringPeriod);
            }


        }
    }
}
