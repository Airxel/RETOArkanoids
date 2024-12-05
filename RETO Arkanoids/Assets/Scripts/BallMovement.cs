using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    //Referencia al RigidBody de la bola
    public Rigidbody ballRb;

    private bool isGameStarted = false;

    [SerializeField]
    private float speed = 5f;

    private Vector3 ballVelocity;

    [SerializeField]
    GameObject player;

    //Referencia al audio
    public AudioSource soundsSource;

    [SerializeField]
    public AudioClip playerSound, brickSound, wallSound, deadSound;

    //Valores para el power-up de slow
    private bool slowPower = false;
    float slowTimer = 0f;

    private void Awake()
    {
        //Se busca la referencia del RigidBody de la bola
        this.ballRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Se emparenta la bola al jugador mientras no empiece la partida
        if (isGameStarted == false)
        {
            this.transform.parent = player.transform;

            this.transform.localPosition = new Vector3(0f, 0f, 1.75f);

            ballVelocity = Vector3.zero;

            //Al pulsar el espacio
            if (Input.GetButton("Jump"))
            {
                SpacePressed();

                isGameStarted = true;
            }
        }
        else if (slowPower == true)
        {
            //Comienzan los 10 segundos
            slowTimer = slowTimer + Time.deltaTime;

            if (slowTimer <= 10f)
            {
                //Se ralentiza la bola
                ballRb.velocity = ballVelocity.normalized * speed / 2f;
            }
            else
            {
                //Vuelve a su velocidad
                slowPower = false;
                slowTimer = 0f;
            }
        }
        else
        {
            //Velocidad cont�nua para que se mueva al cambiar la direcci�n de los vectores
            ballRb.velocity = ballVelocity.normalized * speed;
        }
    }

    private void SpacePressed()
    {
        //Al pulsar espacio, se quita el emparentado, se la da un vector y una velocidad a la bola
        this.transform.parent = null;

        ballVelocity = new Vector3(Random.Range(-1f, 1f), 1f, 0f);

        ballRb.velocity = ballVelocity * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Al colisionar la bola con distintos elementos, va cambiando su vector velocidad.
        //Se usa el absoluto en algunos casos para asegurar que la direcci�n sea la contraria
        if (collision.gameObject.CompareTag("Player"))
        {
            //Se calcula el tama�o de la plataforma por el tama�o del collider
            float playerWidth = collision.collider.bounds.size.x;

            //Se calcula el �ngulo de golpeo de la bola seg�n donde golpee al jugador, dando un valor entre -1 y 1
            float hitPosition = (transform.position.x - collision.transform.position.x) / playerWidth;

            //Si golpea a la izquierda, rebota en esa direcci�n y viceversa
            ballVelocity = new Vector3(hitPosition, Mathf.Abs(ballVelocity.y), 0f);

            //Se escoge el sonido y se hace que suene
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

            //Se llama a la funci�n que cuenta las vidas en el GameManager
            GameManager.instance.LifesCount();

            isGameStarted = false;
        }
    }

    public void SlowActive()
    {
        //Se activa el power-up de slow
        slowPower = true;
        slowTimer = 0f;
    }
}