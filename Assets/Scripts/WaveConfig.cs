using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]

public class WaveConfig : ScriptableObject
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f; //nao ficar previsivel
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] bool isMirrored = false;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints()
    {
        if (isMirrored != false)
        {
            float positionY = pathPrefab.transform.position.y;
            //Debug.Log(positionY);
            //new Vector2 pathPrefab.transform.position = positionY * -1f;
        }

        var waveWaypoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)
        {

            waveWaypoints.Add(child);

        }
        return waveWaypoints;

    }

    public float GetTimeSpawns() { return timeBetweenSpawns; }

    public float GetRandomSpawn() { return spawnRandomFactor; }

    public int NumberOfEnemies() { return numberOfEnemies; }

    public float EnemySpeed() { return moveSpeed; }

}
