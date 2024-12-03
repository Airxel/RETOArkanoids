using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;

    [SerializeField]
    private float speed = 30f;

    private float mouseDelta;

    private float playerPosition;

    private float leftLimit = -2.8f;

    private float rightLimit = 2.8f;

    private void Awake()
    {
        this.playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Obtenemos el delta de la posici�n del rat�n (entre -1 y 1)
        mouseDelta = Input.GetAxis("Mouse X");

        //Se calcula la posici�n a donde se quiere llegar
        //playerPosition = transform.position.x + mouseDelta * speed * Time.fixedDeltaTime;

        //Se limita la posici�n, para que no salga de la pantalla
        //playerPosition = Mathf.Clamp(playerPosition, leftLimit, rightLimit);
        mouseDelta = Mathf.Clamp(mouseDelta, leftLimit, rightLimit);

        //Se da la velocidad al RigidBody, para llegar a la nueva posici�n
        //playerRb.velocity = Vector3.right * (playerPosition - transform.position.x) * speed;
        //playerRb.velocity = Vector3.right * mouseDelta * speed * Time.fixedDeltaTime * 100f;

        //Se a�ade una fuerza al RigidBody, en el modo donde no le afecta la masa del objeto
        playerRb.AddForce(Vector3.right * mouseDelta * speed * Time.fixedDeltaTime * 100f, ForceMode.VelocityChange);
    }
}
