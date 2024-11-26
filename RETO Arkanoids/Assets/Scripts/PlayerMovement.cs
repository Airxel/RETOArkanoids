using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;

    [SerializeField]
    private float speed = 30f;

    private Vector3 playerInitialPosition;
    private float mouseXPosition;


    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime * speed, 0f, 0f);
    }

    private void Wait()
    {
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

        playerRb.AddForce(playerInitialPosition.normalized * speed * Time.deltaTime * 100);
    }
}
