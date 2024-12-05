using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickState : MonoBehaviour
{
    //Referencia a los materiales de los bricks
    private Renderer brickMaterial;

    //Lista con los diferentes estados (materiales/vidas) de los bricks
    public Material[] state;

    private int hitPoints;

    //Lista para llevar la cuenta de los bricks en escena
    public GameObject[] bricksCount;

    public int bricksAmount;

    //Lista de los power-ups disponibles
    public GameObject[] powerUps;

    [SerializeField]
    float powerUpSpawnChance = 20f;
    float powerUpRandomChance;

    [SerializeField]
    public float points = 100f;

    //Pruebas para el power-up de destrucci�n
    private bool destructionPower = false;
    float destructionTimer = 0f;

    private void Awake()
    {
        //Buscamos el la referencia del renderer en el objeto
        this.brickMaterial = GetComponent<Renderer>();
    }

    private void Start()
    {
        //La vida de cada brick equivale a la longitud de la lista que tengan asignados
        hitPoints = state.Length;
    }

    private void Update()
    {
        //Buscamos todos los objetos con la etiqueta "Brick"
        bricksCount = GameObject.FindGameObjectsWithTag("Brick");

        //Y los contamos
        bricksAmount = bricksCount.Length;

        //Pruebas para el power-up de destrucci�n
        if (destructionPower == true)
        {
            destructionTimer = destructionTimer + Time.deltaTime;

            if (destructionTimer >= 10f)
            {
                destructionPower = false;
                destructionTimer = 0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BrickHit();

            if (hitPoints <= 0)
            {
                PowerUpSpawner();
            }
        }
    }

    public void BrickHit()
    {
        //Al golpear un brick se a�aden puntos desde la funci�n llamada de ScoreCount
        //ScoreCount.instance.AddPoints(points);

        //El brick pierde vida
        hitPoints = hitPoints - 1;

        //Y se genera un n�mero aleatorio para la generaci�n del power-up
        powerUpRandomChance = Random.Range(0f, 100f);

        if (hitPoints <= 0)
        {
            //Al destruir un brick se a�aden puntos (seg�n el tipo que sean) desde la funci�n llamada de ScoreCount
            ScoreCount.instance.AddPoints(points);
            Destroy(this.gameObject);
        }
        else
        {
            //Se cambia el material del brick al siguiente de la lista que tenga asignada
            this.brickMaterial.material = this.state[hitPoints - 1];
        }
    }

    private void PowerUpSpawner()
    {
        //Cuando la probabilidad base es mayor que el n�mero aleatorio generado
        if (powerUpSpawnChance >= powerUpRandomChance)
        {
            //Se genera un n�mero aleatorio entre 0 y el tama�o de la lista de power-ups
            int powerUpSelection = Random.Range(0, powerUps.Length);

            //Se escoge el power-up correspondiente a ese n�mero
            GameObject newPowerUp = powerUps[powerUpSelection];

            //Y se instancia en la escena en la posici�n del brick destru�do
            Instantiate(newPowerUp, this.transform.position, newPowerUp.transform.rotation);
        }
    }

    public void DestructionActive()
    {
        //Pruebas para el power-up de destrucci�n
        destructionPower = true;
        destructionTimer = 0f;
    }
}
