using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickState : MonoBehaviour
{
    private Renderer brickMaterial;

    public Material[] state;
    public GameObject[] bricksCount;
    public GameObject[] powerUps;

    [SerializeField]
    float powerUpBaseSpawnChance = 75f;
    float powerUpSpawnChance;

    private int hitPoints;

    public int bricksAmount;

    public float points = 100f;

    private void Awake()
    {
        this.brickMaterial = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        hitPoints = state.Length;
    }

    // Update is called once per frame
    void Update()
    {
        bricksCount = GameObject.FindGameObjectsWithTag("Brick");

        bricksAmount = bricksCount.Length;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            brickHit();
        }
    }

    public void brickHit()
    {
        hitPoints = hitPoints - 1;

        powerUpSpawnChance = Random.Range(0, 100);

        Debug.Log("Es:" + powerUpSpawnChance);

        ScoreCount.instance.AddPoints(points);

        if (hitPoints <= 0)
        {
            Destroy(this.gameObject);

            if (powerUpSpawnChance >= powerUpBaseSpawnChance)
            {
                int powerUpSelection = Random.Range(0, powerUps.Length);
                
                GameObject newPowerUp = Instantiate(powerUps[powerUpSelection], this.transform.position, Quaternion.identity);

                Debug.Log("Spawned");
            }
        }
        else
        {
            this.brickMaterial.material = this.state[hitPoints - 1];
        }
    }
}
