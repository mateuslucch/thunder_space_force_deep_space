using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {

    //float speedFactor;

    WaveConfig waveConfig;
    List<Transform> waypoints;
    [SerializeField] int restartPoint = 1;
    [SerializeField] bool loopShip = false;

    int waypointIndex = 0;
    DifficultyFactors speedFactor;
    
    void Start()
    {
        speedFactor = FindObjectOfType<DifficultyFactors>();
        
        
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

	void Update ()
    {
        MoveEnemy();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
   
    private void MoveEnemy()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = (waveConfig.EnemySpeed() + speedFactor.IncreaseSpeed()) * Time.deltaTime; //speedFactor.IncreaseSpeed() pega o valor que aumenta a velocidade no arquivo DifficultyFactors.cs

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }

        else
        {
            if (loopShip == false)
            {
                Destroy(gameObject);
            }
            else
            {
                waypointIndex = restartPoint;
            }
        }
    }
}
