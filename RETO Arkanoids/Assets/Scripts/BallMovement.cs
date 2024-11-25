using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    [SerializeField]
    GameObject ball;

    GameObject initialBall;

    public static BallMovement instance;

    Vector3 startingBallPosition;

    private void Awake()
    {
        if (BallMovement.instance == null)
        {
            BallMovement.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BallInitialPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BallInitialPosition()
    {
        //startingBallPosition = new Vector3(PlayerMovement.instance.gameObject.transform.position.x, PlayerMovement.instance.gameObject.transform.position.y + 5f, 0f);
        //initialBall = Instantiate(ball, startingBallPosition, Quaternion.identity);
    }
}
