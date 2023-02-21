using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] public Sprite[] powerUpSprites;

    int pUElement;
    private SpriteRenderer spriteRenderer;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();        

        float randomPercentage = Random.Range(0f, 101f);

        if (randomPercentage <= 81f)
        {
            pUElement = 0;
            spriteRenderer.sprite = powerUpSprites[pUElement];
        }

        if (randomPercentage >= 81f && randomPercentage <= 101f)
        {
            pUElement = 1;
            spriteRenderer.sprite = powerUpSprites[pUElement];
        }

        spriteRenderer.sprite = powerUpSprites[pUElement];

        //pUElement = Random.Range(0, powerUpSprites.Length); //nao usar!! sem probabilidades, só randomico

        //pUElement = 1;  //usar para testes individuais

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShip")
        {
            Destroy(gameObject);

            if (pUElement == 0) //extra tiro
            {
                FindObjectOfType<ShipShooter>().MultipleShots();
            }

            if (pUElement == 1)  //escudo
            {
                FindObjectOfType<PlayerShip>().EnergyShield();
            }
        }

        if (collision.gameObject.tag == "PlayerTank")
        {
            Destroy(gameObject);

            if (pUElement == 0)
            {
                FindObjectOfType<TankShooter>().MultipleShots();
            }

            if (pUElement == 1)
            {
                FindObjectOfType<PlayerTank>().EnergyShield();
            }
        }
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
