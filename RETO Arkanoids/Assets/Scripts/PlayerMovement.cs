using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;

    [SerializeField]
    private float speed = 30f;

    private Vector3 playerOriginalPosition;
    private float mouseXPosition;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        mouseXPosition = Input.GetAxis("Mouse X");

        if (mouseXPosition > 0)
        {
            playerOriginalPosition = Vector3.right;
        }
        else if (mouseXPosition < 0)
        {
            playerOriginalPosition = Vector3.left;
        }
        else
        {
            playerOriginalPosition = Vector3.zero;
        }

        playerRb.AddForce(playerOriginalPosition * speed * Time.deltaTime * 100);
    }
}
