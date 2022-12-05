using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    //script purpose: damage the player once you touch it

    [SerializeField] private float damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            collision.GetComponent<PlayerMovement>().PlayBloodParticle();
       
        }
    }
}
