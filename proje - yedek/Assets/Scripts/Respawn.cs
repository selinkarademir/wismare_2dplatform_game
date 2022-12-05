using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Transform currentCheckpoint; //store last checkpoint here
    public Health playerHealth;
    public bool isTriggered;


    public void PlayerRespawn(GameObject go) 
    {
        go.transform.position = gameObject.transform.position;  

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player") 
        {

            isTriggered = true;
            playerHealth.currentRespawn = this;
            currentCheckpoint = collision.transform; 
            gameObject.GetComponent<Collider2D>().enabled = false;

        }

    }
}

