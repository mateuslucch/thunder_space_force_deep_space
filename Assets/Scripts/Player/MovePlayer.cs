//
//THIS SCRIPT WORK FOR TANK AND SPACESHIP
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    [Header("Moving Attributes")] //para organizart melhor no inspector
    [SerializeField] float moveSpeed = 10f;
    float Xpadding = 0.5f;
    float Ypadding = 0.5f;

    [Header("Others")]
    [SerializeField] int changingScenario = 0;

    float xMin = -5f;
    float xMax = 5f;
    float yMin = -4.5f;
    float yMax = 4.5f;

    void Start()
    {
        // SetUpMoveBoundaries();
    }

    void Update()
    {
        if (changingScenario == 0) { Move(); }
        else { return; }

    }

    public void ChangeScenario(int state)
    {
        changingScenario = state;
    }

    private void Move()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed; //configurar "Input" vai em Edit/Project Settings/Input. ao inves de definir teclas esoecificas, define um pr� programado
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);  //"Mathf.Clamp("posi��o do objeto", "limite minimo", "limite m�ximo")"
        transform.position = new Vector2(newXPos, newYPos);
        //Debug.Log  (newXPos);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameGamera = Camera.main;
        xMin = gameGamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + Xpadding;
        xMax = gameGamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - Xpadding;
        yMin = gameGamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + Ypadding;
        yMax = gameGamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - Ypadding;
    }
}
