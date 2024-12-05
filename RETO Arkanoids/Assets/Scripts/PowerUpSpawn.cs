using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    [SerializeField]
    public float points = 500f;

    //Referencia al script BallMovement
    private BallMovement ballMovement;

    //Referencia al script BrickState
    private BrickState brickState;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        //Buscamos el script en el objeto con la etiqueta "Ball"
        ballMovement = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMovement>();

        //Buscamos el script en el objeto con la etiqueta "Brick"
        brickState = GameObject.FindGameObjectWithTag("Brick").GetComponent<BrickState>();

        //Buscamos el script en el objeto con la etiqueta "Player"
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent <PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("Power Destruction"))
            {
                //Llamamos a una función de BrickState
                brickState.DestructionActive();
            }
            else if (this.gameObject.CompareTag("Power Slow"))
            {
                //Llamamos a una función de BallMovement
                ballMovement.SlowActive();
            }
            else if (this.gameObject.CompareTag("Power Lifes"))
            {
                //Llamamos a una función de GameManager (Singleton)
                GameManager.instance.AddLifes();
            }
            else if (this.gameObject.CompareTag("Power Points"))
            {
                //Llamamos a una función de ScoreCount (Singleton)
                ScoreCount.instance.AddPoints(points);
            }
            else if (this.gameObject.CompareTag("Invert Movement"))
            {
                //Llamamos a una función de PlayerMovement
                playerMovement.InvertPlayerMovement();
            }

            Destroy(this.gameObject);

        }
        else if (collision.gameObject.CompareTag("Bottom Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
