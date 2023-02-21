using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceBackgroundScroller : MonoBehaviour
{

    [Header("Background Speed")]
    [SerializeField] float backgroundScrollSpeed;

    [SerializeField] float speedBack = 10f;

    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float minSpeed = 3f;

    [Header("Background Material")]
    [SerializeField] public Material[] backMaterials;
    MeshRenderer actualMaterial;
    Vector2 offSet;
    [SerializeField] int materialNumber = 0;

    [Header("Background Movement Condition")]
    [SerializeField] bool moveToCameraCondition = false;
    [SerializeField] bool moveToBehind = false;
    int changingNumber = 0;

    void Start()
    {
        actualMaterial = GetComponent<MeshRenderer>();
        ChangeMaterial();

    }

    void Update()
    {
        if (changingNumber == 0)
        {
            IncreaseSpeed();
        }
        if (changingNumber == 1)
        {
            DecreaseSpeed();
        }

        MaterialOffset();

        if (moveToCameraCondition == true)
        {
            MoveBackgroundToCamera();
        }
        if (moveToBehind == true)
        {
            MoveBackgroundToBehind();
        }

    }

    public void ChangeSpeedConditions(int theNumber)
    {
        changingNumber = theNumber;
    }
    public void IncreaseSpeed()
    {
        if (speedBack <= maxSpeed)
        {
            speedBack += Time.deltaTime;
            backgroundScrollSpeed = speedBack;

        }
        if (speedBack >= maxSpeed - 0.2f)
        {
            backgroundScrollSpeed = maxSpeed;
        }
    }
    public void DecreaseSpeed()
    {
        if (speedBack >= minSpeed)
        {
            speedBack += -Time.deltaTime;
            backgroundScrollSpeed = speedBack;
        }
        if (speedBack <= minSpeed + 0.2f)
        {
            backgroundScrollSpeed = minSpeed;
        }
    }

    private void MaterialOffset()
    {
        actualMaterial.material.mainTextureOffset += new Vector2(backgroundScrollSpeed / 500, 0f);
    }

    public void ChangeMaterialNumber()
    {
        ChangeMaterial();
    }

    private void ChangeMaterial()
    {
        actualMaterial.material = backMaterials[materialNumber];
    }

    //TRANSLAÇÃO DO FUNDO!!!

    //condiçoes
    public void MoveBackgroundToCameraBool()
    {
        moveToCameraCondition = true;
    }

    public void MoveBackgroundBehindBool()
    {
        moveToBehind = true;
    }

    //MOVIMENTOS

    public void MoveBackgroundToCamera() //move o fundo para a posição da camera
    {

        if (transform.position.x >= 0) //faz o movimento
        {
            transform.Translate(Vector3.left * Time.deltaTime * 5);
        }
        else if (transform.position.x <= 0.01) //interrompe o movimento, posiciona em zero e move a nave para o inicio
        {
            //interrompe
            moveToCameraCondition = false;
            transform.position = new Vector2(Camera.main.transform.position.x, 0);
        }
    }

    public void MoveBackgroundToBehind() //move o fundo para trás
    {
        if (transform.position.x >= -18)
        {

            transform.Translate(Vector3.left * Time.deltaTime * 5);
        }
        else if (transform.position.x <= -17.90)
        {
            moveToBehind = false;
            MoveBackgroundToFront();
            moveToCameraCondition = false;
        }
    }

    public void MoveBackgroundToFront() //reposiciona o fundo na frente do espacial (fica escondido) após o movimento para trás
    {
        materialNumber++;
        if (materialNumber == backMaterials.Length)
        {
            materialNumber = 0;

        }

        ChangeMaterial();
        transform.position = new Vector2(18, 0);

    }
}