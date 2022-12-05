using UnityEngine;

public class MushroomDamage : MonoBehaviour
{
    //script purpose: damage the player once you touch it

    [SerializeField] protected float damage;


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);   //access health component of player and decrease it by passing in damage of current enemy,
            collision.GetComponent<PlayerMovement>().PlayBloodParticle();
        }
    }
}
