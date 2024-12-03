using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            Debug.Log("ACTIVADO");
        }
        else if (collision.gameObject.CompareTag("Bottom Wall"))
        {
            Destroy(this.gameObject);
            Debug.Log("PERDIDO");
        }
    }
}
