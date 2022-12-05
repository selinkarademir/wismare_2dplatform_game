using System.Collections; //needed for IEnumerator
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("FireTrap Timers")]
    [SerializeField] private float activationDelay;  //this variable indicates how much time the trap needs to activate after the player has stepped on it.
    [SerializeField] private float activateTime;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRend;

    private bool isTriggered; //when trap gets triggered. baþta güncel halleri false. intler de 0
    private bool active; //when the trap is active and can hurt the player

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
          
            if (active) 
               collision.GetComponent<Health>().TakeDamage(damage); //ilk deðeri false, þu an false. ilk etkileþimde aktifleþmedi 
        }
    }

    //to trigger the firetrap, we'll use IEnumerator (we'll deal with delays)
    
    public void ActivateAction()
    {
        StartCoroutine(ActivateFiretrap());

    }

    private IEnumerator ActivateFiretrap()
    {
        isTriggered = true; 
        spriteRend.color = Color.red; //turn the sprite red to notify the player
        transform.GetComponent<BoxCollider2D>().isTrigger = true;

        //wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; //turn back to original state

        anim.SetBool("activated", true);
        active = true; //2. if çalýþýyor, collision health mwtoduna gitti, damage yedik.


        //wait until x seconds, deactivate trap and reset all variables and animator
        yield return new WaitForSeconds(activateTime);

        active = false;
        transform.GetComponent<BoxCollider2D>().isTrigger = false;
        isTriggered = false; //objeye tekrar yaklaþtýðýmýzda her þey baþtan baþlayabilecek. (etkileþime girdikçe tekrar saracak)
        anim.SetBool("activated", false);
    }
}





