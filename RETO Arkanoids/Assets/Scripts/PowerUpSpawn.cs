using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    [SerializeField]
    public float points = 500f;

    [SerializeField]
    public GameObject ball;

    public bool slowPower = false;

    private float slowTimer = 0f;

    public Rigidbody ballRb;

    private void Awake()
    {
        ballRb = ball.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        slowPower = false;
    }

    private void FixedUpdate()
    {
        if (slowPower == true)
        {
            Debug.Log("slowPower es true");

            slowTimer = slowTimer + Time.deltaTime;

            Debug.Log("Timer:" + slowTimer);

            if (slowTimer <= 10f)
            {
                ballRb.velocity = ballRb.velocity / 2f;

                Debug.Log("Velocidad reducida");
            }
            else
            {
                slowPower = false;
                slowTimer = 0f;

                Debug.Log("Velocidad normal");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("Power Destruction"))
            {
                Debug.Log("DESTRUCTION");
            }
            else if (this.gameObject.CompareTag("Power Slow"))
            {
                slowPower = true;
                Debug.Log("SLOW");
            }
            else if (this.gameObject.CompareTag("Power Lifes"))
            {
                GameManager.instance.AddLifes();
                Debug.Log("LIFES");
            }
            else if (this.gameObject.CompareTag("Power Points"))
            {
                ScoreCount.instance.AddPoints(points);
                Debug.Log("POINTS");
            }

            Destroy(this.gameObject);
            Debug.Log("ACTIVADO");
        }
        else if (collision.gameObject.CompareTag("Bottom Wall"))
        {
            Destroy(this.gameObject);
            Debug.Log("PERDIDO");
        }
    }

    public void SlowActive()
    {
        Debug.Log("slowPower es true");

        slowTimer = slowTimer + Time.deltaTime;

        Debug.Log("Timer:" + slowTimer);

        if (slowTimer <= 10f)
        {
            ballRb.velocity = ballRb.velocity / 2f;

            Debug.Log("Velocidad reducida");
        }
        else
        {
            slowPower = false;
            slowTimer = 0f;

            Debug.Log("Velocidad normal");
        }
    }
}
