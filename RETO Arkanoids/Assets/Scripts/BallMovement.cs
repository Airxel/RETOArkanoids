using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody ballRb;

    private bool isGameStarted = false;

    [SerializeField]
    private float speed = 5f;

    private Vector3 ballVelocity;

    [SerializeField]
    GameObject player;

    private void Awake()
    {
        this.ballRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isGameStarted == false)
        {
            this.transform.parent = player.transform;

            this.transform.localPosition = new Vector3(0f, 0f, 0.25f);

            ballVelocity = Vector3.zero;

            if (Input.GetButton("Jump"))
            {
                SpacePressed();

                isGameStarted = true;
            }
        }
        else
        {
            ballRb.velocity = ballVelocity.normalized * speed;
        }
    }

    private void SpacePressed()
    {
        this.transform.parent = null;

        ballRb.isKinematic = false;

        ballVelocity = new Vector3(Random.Range(-1f, 1f), 1f, 0f);

        ballRb.velocity = ballVelocity * speed;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Se calcula el tamaño de la plataforma por eltamaño del collider
            float playerWidth = collision.collider.bounds.size.x;

            //Se calcula el ángulo de golpeo de la bola según donde golpee al jugador, dando un valor entre -1 y 1
            float hitPosition = (transform.position.x - collision.transform.position.x) / playerWidth;

            //Si golpea a la izquierda, rebota en esa dirección y viceversa
            ballVelocity = new Vector3(hitPosition, Mathf.Abs(ballVelocity.y), 0f);
        }
        else if (collision.gameObject.CompareTag("Left Wall"))
        {
            ballVelocity = new Vector3(Mathf.Abs(ballVelocity.x), ballVelocity.y, 0f);
        }
        else if (collision.gameObject.CompareTag("Right Wall"))
        {
            ballVelocity = new Vector3(-Mathf.Abs(ballVelocity.x), ballVelocity.y, 0f);
        }
        else if (collision.gameObject.CompareTag("Top Wall"))
        {
            ballVelocity = new Vector3(ballVelocity.x, -Mathf.Abs(ballVelocity.y), 0f);
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            ballVelocity = new Vector3(ballVelocity.x, -ballVelocity.y, 0f);
        }
        else if (collision.gameObject.CompareTag("Bottom Wall"))
        {
            GameManager.instance.LivesCount();

            isGameStarted = false;
        }
    }
}