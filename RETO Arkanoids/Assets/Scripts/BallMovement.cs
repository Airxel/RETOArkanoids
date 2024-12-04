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

    public AudioSource soundsSource;

    [SerializeField]
    public AudioClip playerSound, brickSound, wallSound, deadSound;

    public PowerUpSpawn powerUpSpawn;

    private void Awake()
    {
        this.ballRb = GetComponent<Rigidbody>();

        powerUpSpawn = GetComponent<PowerUpSpawn>();
    }

    private void FixedUpdate()
    {
        if (isGameStarted == false)
        {
            this.transform.parent = player.transform;

            this.transform.localPosition = new Vector3(0f, 0f, 1.75f);

            ballVelocity = Vector3.zero;

            if (Input.GetButton("Jump"))
            {
                SpacePressed();

                isGameStarted = true;
            }
        }
        if (powerUpSpawn.slowPower == true)
        {
            powerUpSpawn.SlowActive();
        }
        else
        {
            ballRb.velocity = ballVelocity.normalized * speed;
        }
    }

    private void SpacePressed()
    {
        this.transform.parent = null;

        //ballRb.isKinematic = false;

        ballVelocity = new Vector3(Random.Range(-1f, 1f), 1f, 0f);

        ballRb.velocity = ballVelocity * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Se calcula el tama�o de la plataforma por eltama�o del collider
            float playerWidth = collision.collider.bounds.size.x;

            //Se calcula el �ngulo de golpeo de la bola seg�n donde golpee al jugador, dando un valor entre -1 y 1
            float hitPosition = (transform.position.x - collision.transform.position.x) / playerWidth;

            //Si golpea a la izquierda, rebota en esa direcci�n y viceversa
            ballVelocity = new Vector3(hitPosition, Mathf.Abs(ballVelocity.y), 0f);

            soundsSource.clip = playerSound;
            soundsSource.Play();
        }
        else if (collision.gameObject.CompareTag("Left Wall"))
        {
            ballVelocity = new Vector3(Mathf.Abs(ballVelocity.x), ballVelocity.y, 0f);

            soundsSource.clip = wallSound;
            soundsSource.Play();
        }
        else if (collision.gameObject.CompareTag("Right Wall"))
        {
            ballVelocity = new Vector3(-Mathf.Abs(ballVelocity.x), ballVelocity.y, 0f);

            soundsSource.clip = wallSound;
            soundsSource.Play();
        }
        else if (collision.gameObject.CompareTag("Top Wall"))
        {
            ballVelocity = new Vector3(ballVelocity.x, -Mathf.Abs(ballVelocity.y), 0f);

            soundsSource.clip = wallSound;
            soundsSource.Play();
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            ballVelocity = new Vector3(ballVelocity.x, -ballVelocity.y, 0f);

            soundsSource.clip = brickSound;
            soundsSource.Play();
        }
        else if (collision.gameObject.CompareTag("Bottom Wall"))
        {
            soundsSource.clip = deadSound;
            soundsSource.Play();

            GameManager.instance.LifesCount();

            isGameStarted = false;
        }
    }
}