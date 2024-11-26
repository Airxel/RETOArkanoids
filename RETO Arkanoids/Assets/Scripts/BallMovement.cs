using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody ballRb;

    private bool isGameStarted = false;

    [SerializeField]
    private float speed = 500f;

    private Vector3 ballVelocity;

    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted == false)
        {
            this.transform.parent = player.transform;
            this.transform.localPosition = new Vector3(0f, 1.5f, 0f);

            if (Input.GetButton("Jump"))
            {
                SpacePressed();

                isGameStarted = true;
            }
        }
        else
        {
            speed = 5f;
            this.transform.position = this.transform.position + ballVelocity.normalized * speed * Time.deltaTime;
            
            //ballRb.AddForce(ballVelocity.normalized * speed * Time.deltaTime);
        }
    }

    private void SpacePressed()
    {
        this.transform.parent = null;

        ballRb.isKinematic = false;

        ballVelocity = new Vector3(Random.Range(-1f, 1f), 1f, 0f);

        //this.transform.position = this.transform.position + ballVelocity.normalized * speed;

        ballRb.AddForce(ballVelocity.normalized * speed * Time.deltaTime * 100);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");

        if (collision.gameObject.CompareTag("Player"))
        {
            //ballVelocity = new Vector3(Random.Range(-1f, 1f), -1f, 0f);
            ballVelocity = new Vector3(Random.Range(-1f, 1f), -ballVelocity.y, 0f);
        }
        else if (collision.gameObject.CompareTag("Left Wall"))
        {
            //ballVelocity = new Vector3(Random.Range(1f, -1f), 1f, 0f);
            ballVelocity = new Vector3(-ballVelocity.x, ballVelocity.y, 0f);
        }
        else if (collision.gameObject.CompareTag("Right Wall"))
        {
            //ballVelocity = new Vector3(Random.Range(1f, -1f), 1f, 0f);
            ballVelocity = new Vector3(-ballVelocity.x, ballVelocity.y, 0f);
        }
        else if (collision.gameObject.CompareTag("Top Wall"))
        {
            //ballVelocity = new Vector3(Random.Range(1f, -1f), -1f, 0f);
            ballVelocity = new Vector3(-ballVelocity.x, -ballVelocity.y, 0f);
        }
        else if (collision.gameObject.CompareTag("Bottom Wall"))
        {
            Debug.Log("Deadge");
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            //ballVelocity = new Vector3(Random.Range(1f, -1f), -1f, 0f);
            ballVelocity = new Vector3(Random.Range(-1f, 1f), -ballVelocity.y, 0f);
            Debug.Log("Points");
        }
    }
}
