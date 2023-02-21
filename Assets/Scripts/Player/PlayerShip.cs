using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{

    [Header("Player Attributes")]
    [SerializeField] int health = 200;

    [SerializeField] GameObject shipEnergyShield;

    [Header("Player Sounds")]
    [SerializeField] AudioClip explosionPlayer;
    [Range(0f, 1f)] [SerializeField] float deathVolume = 0.5f;

    [Header("Others")]
    Coroutine firingCoroutine;
    [SerializeField] int changingScenario = 0;
    
    void Start()
    {
        shipEnergyShield.SetActive(false);
    }

    public void ShipControl(int numberCondition)
    {
        changingScenario = numberCondition;
        gameObject.GetComponent<MovePlayer>().ChangeScenario(numberCondition);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyThings")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }

            ProcessHit(damageDealer);
        }

        if (other.gameObject.tag == "Enemy")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }

            ProcessHit(damageDealer);
        }
       
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        AudioSource.PlayClipAtPoint(explosionPlayer, Camera.main.transform.position, deathVolume);
        Destroy(gameObject);

    }

    public int GetHealt()
    {
        return health;
    }
    
    public void EnergyShield()
    {
        shipEnergyShield.SetActive(true);
    }
}

