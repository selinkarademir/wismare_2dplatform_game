using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform player;
    public bool movementPermit;
    public float speed;

    private GameObject ghostDamage;
    [SerializeField] private Vector3 firstPos;
    [SerializeField] private BoxCollider2D other;


    void Start()
    {
        ghostDamage = transform.GetChild(0).gameObject; 
        firstPos = transform.position;
    }

    void Update()
    {
        if (movementPermit)
        {
            CharacterChase();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            movementPermit = true;
        }
    }
    void CharacterChase()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            movementPermit = false;
        }
    }

    public void GhostFirstPosition()
    {
        gameObject.transform.position = firstPos;

    }
}


