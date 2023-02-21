using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyFactors : MonoBehaviour {

    [SerializeField] float startSpeedFactor = 0f;
    [SerializeField] float standartSpeedFactor = 0f;

    [SerializeField] float shotFrequencyFact = 0;
    

    public void ExtraSpeedLevel()
    {
        
        //Debug.Log(speedFactor);
        startSpeedFactor += standartSpeedFactor;
        //Debug.Log(speedFactor);
    }

    public float IncreaseSpeed() { return startSpeedFactor; }

    /*
    public float IncreaseShotFrequency()
    {
        //return shotFrequency;
    }
   */
}
