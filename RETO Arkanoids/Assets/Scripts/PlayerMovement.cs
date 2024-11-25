using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float playerOriginalYPosition;

    [SerializeField]
    private float mouseXPosition = 0f;

    [SerializeField]
    private float leftLimit = -2.25f;

    [SerializeField]
    private float rightLimit = 2.25f;

    private float mouseLimitPosition;

    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = Camera.main;
        playerOriginalYPosition = this.transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {
        PlatformMovement();
    }

    private void PlatformMovement()
    {
        //Posición en X del movimiento del ratón
        mouseXPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0f, 0f)).x;

        //Ponemos límites al movimiento
        mouseLimitPosition = Mathf.Clamp(mouseXPosition, leftLimit, rightLimit);

        //La nueva posición del jugador
        this.transform.position = new Vector3(mouseLimitPosition, playerOriginalYPosition, 0f);
    }
}
