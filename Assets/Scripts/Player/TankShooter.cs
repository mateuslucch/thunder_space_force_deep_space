using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject bulletPrefab; //variavel gameobject, libera no unity pra linkar com o laser
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] int tankControl = 0;
    [SerializeField] bool multipleShots = false;
    [SerializeField] float shootAngle = 0f;
    [SerializeField] float shotCounter = 10f;

    [Header("Cannons")]
    [SerializeField] GameObject leftCannon;
    [SerializeField] GameObject middleCannon;
    [SerializeField] GameObject rightCannon;

    [Header("Sound")]
    [SerializeField] AudioClip shootSound;
    [Range(0f, 1f)] [SerializeField] float shootVolume = 0.5f;

    Coroutine firingCoroutine;
    //bool shooting = false;

    void Update()
    {
        if (tankControl == 0)
        {
            Fire();
        }
        if (tankControl == 1)
        {
            //StopCoroutine(firingCoroutine);
            return;
        }

        if (multipleShots == true)
        {
            CountDownMultipleShoot();
        }

    }
    public void ChangingConditions(int numberCondition)
    {
        tankControl = numberCondition;
        StopAllCoroutines();

    }
    private void Fire() //FIRE LASERS!!
    {

        if (Input.GetButtonDown("Fire1")) //Atira quando botão presisonado
        {
            firingCoroutine = StartCoroutine(FireContinuously());

        }
        if (Input.GetButtonUp("Fire1")) //Para de atirar quando botão é solto
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
            ExtraCannonOff();
            shotCounter = 10f;

        }
    }
    public void MultipleShots()
    {
        ExtraCannonOn();
        shotCounter = 10f;
        multipleShots = true;
    }

    public void StopMultipleShots() //chamado pelo gameControl
    {
        ExtraCannonOff();
        multipleShots = false;
    }
    private void ExtraCannonOn()
    {
        leftCannon.SetActive(true);
        rightCannon.SetActive(true);
    }
    private void ExtraCannonOff()
    {
        leftCannon.SetActive(false);
        rightCannon.SetActive(false);
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            if (multipleShots != false)
            {
                GameObject cannon1 = Instantiate(
                       bulletPrefab,
                       leftCannon.transform.position,
                       transform.rotation) as GameObject;

                cannon1.GetComponent<Rigidbody2D>().velocity = new Vector2(shootAngle, projectileSpeed);

                GameObject cannon3 = Instantiate(
                       bulletPrefab,
                       middleCannon.transform.position,
                       transform.rotation) as GameObject;

                cannon3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                GameObject cannon2 = Instantiate(
                       bulletPrefab,
                       rightCannon.transform.position,
                       transform.rotation) as GameObject;

                cannon2.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootAngle, projectileSpeed);
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
                yield return new WaitForSeconds(projectileFiringPeriod);
            }

            else
            {
                GameObject laser = Instantiate(
                       bulletPrefab,
                       middleCannon.transform.position,
                       transform.rotation) as GameObject;
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                yield return new WaitForSeconds(projectileFiringPeriod);
            }
        }
    }
}
