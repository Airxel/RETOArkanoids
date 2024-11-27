using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickState : MonoBehaviour
{
    private Renderer brickMaterial;

    private int hitPoints;

    public Material[] state;

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

        ScoreCount.instance.AddPoints(points);

        if (hitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.brickMaterial.material = this.state[hitPoints - 1];
        }
    }
}
