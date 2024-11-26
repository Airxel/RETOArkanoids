using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody ballRb;

    [SerializeField]
    private float speed = 100f;

    private Vector3 ballVelocity;

    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = player.transform;

    }

    // Update is called once per frame
    void Update()
    {
        SpacePressed();
    }

    private void SpacePressed()
    {
        if (Input.GetButton("Jump"))
        {
            this.transform.parent = null;

            ballVelocity = new Vector3(Random.Range(-1f, 1f), 1f, 0f);
            this.transform.position += ballVelocity * speed * Time.deltaTime;

            //ballRb.AddForce(ballVelocity.normalized * speed * Time.deltaTime * 100);
        }
    }
}
