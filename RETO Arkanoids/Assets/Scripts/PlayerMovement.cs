using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;

    [SerializeField]
    private float speed = 30f;

    private Vector3 playerInitialPosition;

    private float mouseXPosition;
    private float mouseDelta;
    private float mouseXOldPosition;
    private float screenWidth = Screen.width;

    private void Awake()
    {
        this.playerRb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        mouseXOldPosition = Input.mousePosition.x;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Obtenemos el delta de la posición del ratón (entre -1 y 1)
        mouseDelta = Input.GetAxis("Mouse X");

        //Multiplicamos por el ancho de la pantalla, para que el movimiento sea constante
        //en modo ventana y pantalla completa y parecido a Input.mousePosition.x (en píxeles)
        mouseDelta *= screenWidth;

        //Se añade una fuerza al RigidBody, en el modo donde no le afecta la masa del objeto
        playerRb.AddForce(Vector3.right * mouseDelta * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    private void Wait()
    {
        mouseXPosition = Input.mousePosition.x;

        mouseDelta = mouseXPosition - mouseXOldPosition;

        playerRb.AddForce(Vector3.right * mouseDelta * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        mouseXOldPosition = mouseXPosition;

        //

        mouseXPosition = Input.GetAxis("Mouse X");

        if (mouseXPosition > 0)
        {
            playerInitialPosition = Vector3.right;
        }
        else if (mouseXPosition < 0)
        {
            playerInitialPosition = Vector3.left;
        }
        else
        {
            playerInitialPosition = Vector3.zero;
        }

        playerRb.AddForce(playerInitialPosition.normalized * mouseXPosition * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
