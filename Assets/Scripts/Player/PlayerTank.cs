using System.Collections;
using UnityEngine;

public class PlayerTank : MonoBehaviour
{

    [Header("Player Attributes")] //para organizart melhor no inspector   
    [SerializeField] int health = 200;

    [SerializeField] GameObject tankEnergyShield;

    [Header("Player Sounds")]
    [SerializeField] AudioClip explosionPlayer;
    [Range(0f, 1f)] [SerializeField] float deathVolume = 0.5f;

    [Header("Extra Parts")]
    [SerializeField] GameObject[] playerPartsOne;

    [Header("Others")]    
    [SerializeField] int tankControl = 0;
    //bool shooting = false;

    void Start()
    {
        //turn off extra parts in the beginning
        for (int i = 0; i < playerPartsOne.Length; i++)
        {
            playerPartsOne[i].SetActive(false);
        }
        //gameObject.SetActive(false);
        tankEnergyShield.SetActive(false);

    }
     
    public void ChangingConditions(int numberCondition)
    {
        tankControl = numberCondition;
        StopAllCoroutines();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }

        ProcessHit(damageDealer); //mantém damageDealer, se não não é chamado no metodo
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
    private void ExtraCannonOn()
    {
        for (int i = 0; i < playerPartsOne.Length; i++)
        {
            playerPartsOne[i].SetActive(true);
        }
    }
    public void ExtraCannonOff()
    {
        for (int i = 0; i < playerPartsOne.Length; i++)
        {
            playerPartsOne[i].SetActive(false);
        }
    }

    public void EnergyShield() { tankEnergyShield.SetActive(true); }
}


