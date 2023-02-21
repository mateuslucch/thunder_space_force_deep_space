using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Header("Space Wave List")]
    [SerializeField] List<WaveConfig> spaceWaveConfig;
    [SerializeField] public int[] spaceInterval;
    int intervalBetWave;

    [Header("Land Wave List")]
    [SerializeField] List<WaveConfig> landWaveConfig;
    [SerializeField] public int[] landInterval;
    [SerializeField] int startingWave = 0;
    public GameObject[] lastEnemies;
    bool countEnemies = false;
    float scenarioType;

    void Start()
    {
        StartCoroutine(StartWaveDelay());        
    }

    IEnumerator StartWaveDelay()
    {
        yield return new WaitForSecondsRealtime(4);
        LaunchSpaceWave();
    }
    private void Update()
    {
        if (countEnemies == true)
        {
            lastEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (lastEnemies.Length == 0)
            {
                countEnemies = false;

                //IniCIO DAS TROCAS
                if (scenarioType == 1)
                {
                    FindObjectOfType<BossSpawner>().SpaceBossWave();
                    //FindObjectOfType<GameControl>().ChangeCondition(1); //troca o cenario após fim de onda
                }

                if (scenarioType == 2)
                {
                    FindObjectOfType<BossSpawner>().LandBossWave();
                    //FindObjectOfType<GameControl>().ChangeCondition(3); //troca o cenario após fim de onda
                }
            }
            else
            {
                return;
            }
        }

    }

    public void LaunchSpaceWave()
    {
        StartCoroutine(SpawnSpaceWave());
        
    }
    public void LaunchLandWave()
    {
        
        StartCoroutine(SpawnLandWave());
    }

    //ORIGINAL

    //Onda Espacial
    private IEnumerator SpawnSpaceWave() //lança uma onda
    {
        for (int waveIndex = startingWave; waveIndex < spaceWaveConfig.Count; waveIndex++)
        {
            intervalBetWave = waveIndex;
           
            var currentSpaceWave = spaceWaveConfig[waveIndex];
            yield return StartCoroutine(SpawnSpaceEnemies(currentSpaceWave));

            //termina a spacewave
            if (waveIndex == spaceWaveConfig.Count - 1)
            {
                countEnemies = true;
                scenarioType = 1;
            }
        }
        
    }
    
    //Onda Terrestre
    private IEnumerator SpawnLandWave()
    {
        for (int waveIndex = startingWave; waveIndex < landWaveConfig.Count; waveIndex++)
        {
            var currentLandWave = landWaveConfig[waveIndex];
            yield return StartCoroutine(SpawnLandEnemies(currentLandWave));

            //termina a landwave
            if (waveIndex == spaceWaveConfig.Count - 1)//
            {
                countEnemies = true;
                scenarioType = 2;
                //Debug.Log(scenarioType);
            }

        }
        
    }

    private IEnumerator SpawnSpaceEnemies(WaveConfig waveConfig) //lança naves em cada onda
    {
        for (int enemyCount = 0; enemyCount < waveConfig.NumberOfEnemies(); enemyCount++)  //"waveConfig.NumberOfEnemies" busca o metodo no arquivo WaveConfig.cs
        {                                                                                   // que entrega o numero de inimigos por onda
            var newEnemy = Instantiate(
             waveConfig.GetEnemyPrefab(),
             waveConfig.GetWaypoints()[0].transform.position,
             Quaternion.Euler(new Vector3(0, 0, 0))); /*rotação do objeto*/
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeSpawns()); //tempo entre cada nave de uma onda, busca do waveConfig

        }
        //yield return new WaitForSeconds(spaceInterval[intervalBetWave]);
        
    }
    
    private IEnumerator SpawnLandEnemies(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.NumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                /*rotação do objeto*/Quaternion.Euler(new Vector3(0, 0, 0)));
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeSpawns());

        }
        //yield return new WaitForSeconds(landInterval[intervalBetWave]);
    }


}
 