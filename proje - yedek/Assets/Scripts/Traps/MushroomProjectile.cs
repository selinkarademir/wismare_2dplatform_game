using UnityEngine;

public class MushroomProjectile : MushroomDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;

    public void ActiveProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            {
            collision.GetComponent<Health>().TakeDamage(damage);   //access health component of player and decrease it by passing in damage of current enemy,
            collision.GetComponent<PlayerMovement>().PlayBloodParticle();

            gameObject.SetActive(false);
           }
       

    }
}
