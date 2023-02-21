using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TankHealtDisplay : MonoBehaviour
{

    TextMeshProUGUI healtText;
    PlayerTank playerTank;
    float tankHealt;

    void Start()
    {
        healtText = GetComponent<TextMeshProUGUI>();
        playerTank = FindObjectOfType<PlayerTank>();
        FindObjectOfType<GameControl>().StartDisableTank(); //desativar tanque no gamecontrol.cs. evitar bug no compilado
        Debug.Log("healt display lido");
    }

    void Update()
    {
        healtText.text = playerTank.GetHealt().ToString();
        tankHealt = playerTank.GetHealt();

        if (tankHealt <= 0f)
        {
            tankHealt = 0f;
            healtText.text = tankHealt.ToString(); //converte a variavel tankHealt para texto e envia pro texto

        }

    }

}
