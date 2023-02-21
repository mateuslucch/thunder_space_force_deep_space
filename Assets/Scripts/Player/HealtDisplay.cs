using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealtDisplay : MonoBehaviour {

    TextMeshProUGUI healtText;
    PlayerShip playerShip;
   
    private float playerHealt;
        
    void Start()
    {
        healtText = GetComponent<TextMeshProUGUI>();
        playerShip = FindObjectOfType<PlayerShip>();
        
    }
    
    void Update()
    {
        playerHealt = playerShip.GetHealt();
        healtText.text = playerShip.GetHealt().ToString();
        if (playerHealt <= 0f)
        {
            playerHealt = 0f;
            healtText.text = playerHealt.ToString(); //converte a variavel tankHealt para texto e envia pro texto


        }

    }

}
