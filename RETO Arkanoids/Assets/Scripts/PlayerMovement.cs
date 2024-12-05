using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Referencia al RigidBody de la bola
    public Rigidbody playerRb;

    [SerializeField]
    private float speed = 30f;

    private float mouseDelta;

    //Valor para pruebas del movimiento del jugador
    private float playerPosition;

    //Limites de movimientos del jugador
    private float leftLimit = -2.8f;
    private float rightLimit = 2.8f;


    private bool invertMovement = false;
    private float invertMovementTimer = 0f;

    private void Awake()
    {
        //Se busca la referencia del RigidBody de la bola
        this.playerRb = GetComponent<Rigidbody>();  
    }

    private void FixedUpdate()
    {

        //Obtenemos el delta de la posici�n del rat�n (entre -1 y 1)
        mouseDelta = Input.GetAxis("Mouse X");

        //Se limita la posici�n, para que no salga de la pantalla
        mouseDelta = Mathf.Clamp(mouseDelta, leftLimit, rightLimit);


        if (invertMovement == true)
        {
            //Comienzan los 10 segundos
            invertMovementTimer = invertMovementTimer + Time.deltaTime;

            if (invertMovementTimer <= 10f)
            {
                //Se invierte el valor de la fuerza al RigidBody, en el modo donde no le afecta la masa del objeto
                playerRb.AddForce(Vector3.left * mouseDelta * speed * Time.fixedDeltaTime * 100f, ForceMode.VelocityChange);
            }
            else
            {
                //Cuando se acaba el tiempo, se desactiva
                invertMovement = false;
                invertMovementTimer = 0f;
            }          
        }
        else
        {
            //Se a�ade una fuerza al RigidBody, en el modo donde no le afecta la masa del objeto
            playerRb.AddForce(Vector3.right * mouseDelta * speed * Time.fixedDeltaTime * 100f, ForceMode.VelocityChange);
        }



        //Pruebas de movimiento del jugador

        //Se calcula la posici�n a donde se quiere llegar
        //playerPosition = transform.position.x + mouseDelta * speed * Time.fixedDeltaTime;

        //Se limita la posici�n, para que no salga de la pantalla
        //playerPosition = Mathf.Clamp(playerPosition, leftLimit, rightLimit);

        //Se da la velocidad al RigidBody, para llegar a la nueva posici�n
        //playerRb.velocity = Vector3.right * (playerPosition - transform.position.x) * speed;
        //playerRb.velocity = Vector3.right * mouseDelta * speed * Time.fixedDeltaTime * 100f;
    }


    public void InvertPlayerMovement()
    {
        invertMovement = true;
        invertMovementTimer = 0f;

    }
}
