using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{

    [SerializeField] GameObject tankObject;
    [SerializeField] GameObject shipObject;

    GameObject spaceBackgroundObject;
    GameObject landBackgroundObject;
    GameObject[] spaceParticles;
    GameObject[] landParticles;

    GameObject difficultyFactors;

    Vector3 startPosition = new Vector3(0, -3, 0);
    Vector3 flyAway = new Vector3(-8, -8, 0);
    public GameObject[] powerUps;


    int conditionNumber = 0;

    void Start()
    {
        tankObject = GameObject.FindGameObjectWithTag("PlayerTank");
        shipObject = GameObject.FindGameObjectWithTag("PlayerShip");
        spaceBackgroundObject = GameObject.FindGameObjectWithTag("SpaceBack");
        landBackgroundObject = GameObject.FindGameObjectWithTag("LandBack");
        spaceParticles = GameObject.FindGameObjectsWithTag("SpaceParticles");

        landParticles = GameObject.FindGameObjectsWithTag("LandParticles");
        tankObject.SetActive(false); //desativa o tanque logo no começo

        for (int i = 0; i < landParticles.Length; i++)
        {
            GameObject landObjects = landParticles[i];
            landObjects.SetActive(false);
        }

    }
    public void StartDisableTank() //desativa tanque a partir do tankhealtdisplay.cs. evitar bug no inicio do jogo
    {
        if (tankObject) { tankObject.SetActive(false); }//se tanque for true...
        else { Debug.Log("tanque desativado"); }
    }

    void Update()
    {
        /*
        if (conditionNumber == 0) //não executa nenhuma condição
        {
            return;
        }
        */
        if (conditionNumber == 1)
        {
            ChangeToLand();

        }
        if (conditionNumber == 2)
        {
            RemoveSpaceship();
        }
        if (conditionNumber == 3)
        {
            ChangeToSpace();
        }
        if (conditionNumber == 4)
        {
            RecallSpaceship();
        }
        if (conditionNumber == 5)
        {
            RescaleShip();
        }
    }
    public void ChangeCondition(int theCondition)
    {

        conditionNumber = theCondition;

    }

    //TROCA DE ESPAÇO PARA TERRA
    private void ChangeToLand()
    {
        shipObject.GetComponent<Collider2D>().enabled = false;
        for (int i = 0; i < spaceParticles.Length; i++)
        {
            GameObject starsBack = spaceParticles[i];
            starsBack.SetActive(false);
        }
        //PROCESSO MOVER A NAVE E ALTERAR A ESCALA

        //ALTERA A ESCALA da nave(AUMENTA)
        shipObject.GetComponent<PlayerShip>().ShipControl(1); //remove controle manual da nave 
        if (shipObject.transform.localScale.x <= 3f)
        {
            float scaleFactor = Time.deltaTime;
            shipObject.transform.localScale += new Vector3(scaleFactor, scaleFactor, 1);
        }

        //MOVE A NAVE PARA O INICIO E APAGA POWERUPS
        DestroyPowerUps();
        if (shipObject.transform.position != startPosition)
        {
            shipObject.transform.position = Vector3.MoveTowards(shipObject.transform.position, startPosition, Time.deltaTime * 5);
        }

        //INICIA A TROCA APÓS A ALTERAÇÃO NA ESCALA
        if (shipObject.transform.localScale.x >= 2.9f)
        {


            shipObject.transform.localScale = new Vector3(3f, 3f, 1f); //fixa a escala em valores inteiros(sem animação)

            landBackgroundObject.GetComponent<BackgroundScroller>().MoveBackgroundToCameraBool(); //move background do BackgrounsScroller.cs
                                                                                                  //mantém o movimento da nave
            if (shipObject.transform.position == startPosition)
            {
                conditionNumber = 0;

                StartCoroutine(WaitAndReleaseTank());
                landBackgroundObject.GetComponent<BackgroundScroller>().ChangeSpeedConditions(1);

            }
        }
    }

    private void DestroyPowerUps()
    {
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        for (int i = 0; i < powerUps.Length; i++)
        {
            Destroy(powerUps[i]);
        }

    }

    IEnumerator WaitAndReleaseTank()
    {
        yield return new WaitForSecondsRealtime(3);

        tankObject.GetComponent<PlayerTank>().ChangingConditions(1);
        tankObject.SetActive(true);
        conditionNumber = 2;
    }

    private void RemoveSpaceship() //remove a nave, habilita o tanque, e começa a onda. chamado no update, de acordo com condição
    {

        if (shipObject.transform.position != flyAway)
        {
            shipObject.transform.position = Vector3.MoveTowards(shipObject.transform.position, flyAway, Time.deltaTime * 5);
        }
        if (shipObject.transform.position == flyAway)
        {
            conditionNumber = 0;
            //libera tanque
            tankObject.GetComponent<PlayerTank>().ChangingConditions(0);
            shipObject.GetComponent<ShipShooter>().StopMultipleShots();
            shipObject.SetActive(false); //esconde nave
            FindObjectOfType<EnemySpawner>().LaunchLandWave(); //inicia ataques terrestres

            for (int i = 0; i < landParticles.Length; i++)
            {
                GameObject landObjects = landParticles[i];
                landObjects.SetActive(true);
            }

        }
    }

    //TROCA TERRA PARA ESPAÇO
    private void ChangeToSpace()
    {
        tankObject.GetComponent<Collider2D>().enabled = false; //desativa colisor do tanque, evitar tomar dano
        tankObject.GetComponent<PlayerTank>().ChangingConditions(1); //muda a condição do tanque, valor 1 desativa qqr controle sobre o mesmo no update
        MoveTankToStart();//Move tanque para inicio

    }

    private void MoveTankToStart() //MOVE TANQUE PARA INICIO E REMOVE POWERUPS
    {

        DestroyPowerUps(); //destroi powerups voando no cenario
        tankObject.GetComponent<PlayerTank>().ExtraCannonOff(); //desativa o canhao extra
        if (tankObject.transform.position != startPosition) //move tanque para posição inicial
        {
            tankObject.transform.position = Vector3.MoveTowards(tankObject.transform.position, startPosition, Time.deltaTime * 5);
        }
        if (tankObject.transform.position == startPosition) //
        {
            conditionNumber = 0;
            shipObject.SetActive(true);
            spaceBackgroundObject.GetComponent<BackgroundScroller>().ChangeMaterial();
            conditionNumber = 4;

        }

    }

    private void RecallSpaceship()//chama a nave para o inicio, esconde o tanque e começa processo de troca de cenário e alteração da escala
    {
        if (shipObject.transform.position != startPosition) //move a nave pra posição start(mesma do tanque)
        {
            shipObject.transform.position = Vector3.MoveTowards(shipObject.transform.position, startPosition, Time.deltaTime * 5);
        }
        if (shipObject.transform.position == startPosition) //trava nave no inicio quando chega no ponto inicial
        {
            conditionNumber = 0; //muda condição da nave
            DestroyPowerUps();
            tankObject.GetComponent<TankShooter>().StopMultipleShots();

            tankObject.GetComponent<Collider2D>().enabled = true;
            tankObject.SetActive(false); //desativa o tanque, para ficar só nave

            landBackgroundObject.GetComponent<BackgroundScroller>().MoveBackgroundBehindBool(); //troca cenario
            landBackgroundObject.GetComponent<BackgroundScroller>().ChangeSpeedConditions(0); //troca cenario
            StartCoroutine(WaitAndRescale()); //
            for (int i = 0; i < landParticles.Length; i++)
            {
                GameObject landObjects = landParticles[i];
                landObjects.SetActive(false); //desativa particulas terrestres
            }
        }
    }

    IEnumerator WaitAndRescale()
    {
        yield return new WaitForSecondsRealtime(3);
        conditionNumber = 5; //muda condição aqui para rodar o RescaleShip()

    }

    private void RescaleShip()//altera escala da nave e começa processo de troca de cenário até começar onda espacial
    {

        if (shipObject.transform.localScale.x >= 1f) //muda escala da nave
        {
            float scaleFactor = -Time.deltaTime;
            shipObject.transform.localScale += new Vector3(scaleFactor, scaleFactor, 1);
        }
        if (shipObject.transform.localScale.x <= 1.1f)
        {
            conditionNumber = 0;
            shipObject.GetComponent<PlayerShip>().ShipControl(0); //restaura controle da nave
            shipObject.transform.localScale = new Vector3(1, 1, 1); //fixa escala da nave
            //Aumentar dificuldade
            FindObjectOfType<DifficultyFactors>().ExtraSpeedLevel();

            FindObjectOfType<EnemySpawner>().LaunchSpaceWave(); //(re)INICIA ONDA ESPACIAL
            shipObject.GetComponent<Collider2D>().enabled = true;
            for (int i = 0; i < spaceParticles.Length; i++)
            {
                GameObject starsBack = spaceParticles[i];
                starsBack.SetActive(true);
            }
        }
    }

}
