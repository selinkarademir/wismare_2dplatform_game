using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; } 
    private Animator anim;
    [Header("Components")]
    private bool dead;
    public Respawn currentRespawn;
    public Transform firstPos;
    public List<GameObject> respawnPos = new List<GameObject>(); 
    public CanvasController score;
    public Ghost ghost;
    GameController GC;

    private void Start() 
    {
        GC = GameController.instance;
        startingHealth = GC.data.playerData.HealthCount[GC.data.playerData.currentHealthIndex];
        Debug.Log(GC.data.playerData.HealthCount[GC.data.playerData.currentHealthIndex]);
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); 

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");

            if (currentRespawn != null) 
            {

                currentRespawn.PlayerRespawn(gameObject);

                if (ghost != null)
                {
                    ghost.GhostFirstPosition();
                }

            }

            else
            {
                LevelRestart(); 
            }

        }

        else
        {
            StartCoroutine(DeadAction());

        }
    }

    IEnumerator DeadAction()
    {
        if (!dead) //if statement to make sure dead animation wont be repeated
        {
            dead = true;
            anim.SetTrigger("die");

            //player cant move if dead
            if (GetComponent<PlayerMovement>() != null)
            {
                GetComponent<PlayerMovement>().enabled = false;
            }

            if (GetComponent<SmartEnemy>() != null) 
            {

                GetComponent<SmartEnemy>().enabled = false;

            }

            yield return new WaitForSeconds(0.8f);
            AddHealth(GC.data.playerData.currentHealthIndex+1);
            LevelRestart();


            anim.ResetTrigger("die");
            if (GetComponent<PlayerMovement>() != null)
            {
                GetComponent<PlayerMovement>().enabled = true;
            }

            //enemy
            if (GetComponent<SmartEnemy>() != null)
            {

                GetComponent<SmartEnemy>().enabled = true;

            }
            
            for (int i =0; i< respawnPos.Count; i++)
            {
                respawnPos[i].GetComponent<Collider2D>().enabled = true;
            }

            dead = false;
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void LevelRestart()
    {
        gameObject.transform.position = firstPos.position;
        currentRespawn = null;
        
        if(ghost != null)
        {
            ghost.GhostFirstPosition(); 
        }


    }

}
