using UnityEngine;
using System.Collections;

public class SmartEnemy : MonoBehaviour
{

    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header("Attack Parameters")]

   [SerializeField] private float attackCoolDown;
   [SerializeField] private int damage; 
   [SerializeField] private float range;

    [Header("Movement parameter")]
    [SerializeField] private float speed;
    private bool movingLeft;

    [Header("Idle behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer; //for how long has the enemy been idle


    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    [Header("Health")]
    [SerializeField] private int enemyHealth;

    [SerializeField] private ParticleSystem bloodParticle;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    [Header("DropObjects")]
    [SerializeField] private GameObject CollectibleSpell;
    [SerializeField] private GameObject CollectibleHealth;
    
    [SerializeField] private Health playerHealth;
    [SerializeField] private GameObject player;

    GameController GC;
    
    public State state;
    public enum State
    {
        walk,
        attack,
        die,
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    private void Start()
    {
        GC = GameController.instance;

        StartCoroutine(AIMovement());
    }


    #region Movement


    IEnumerator AIMovement()
    {
        while (state == State.walk)
        {
            if (movingLeft)
            {
                if (gameObject.transform.position.x >= leftEdge.position.x)
                {
                    MoveInDirection(-1);
                }

                else
                {
                    StartCoroutine(DirectionChange()); //change direction
                    yield return new WaitForSeconds(2.2f);
                }

            }

            else
            {
                
                if (gameObject.transform.position.x <= rightEdge.position.x)
                {
                    MoveInDirection(1);
                }

                else
                {
                    StartCoroutine(DirectionChange()); //change direction
                    yield return new WaitForSeconds(2.2f);
                }

            }


            yield return new WaitForEndOfFrame(); 
        }

        if(state == State.attack)
        {
            StartCoroutine(AIAttack());
            yield break;
        }
    }

    #endregion

    #region Attack

    IEnumerator AIAttack()
    {
        while(state == State.attack)
        {
            yield return new WaitForSeconds(0.2f);

            DamagePlayer();

            yield return new WaitForSeconds(1f);
        }

        if (state == State.walk)
        {
            StartCoroutine(AIMovement());
         
        }
    }

    #endregion


    private void FixedUpdate()
    {
        PlayerInSight();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            anim.SetTrigger("hurt");
            enemyHealth -= GC.data.playerData.DamageList[GC.data.playerData.currentDamageIndex];
            bloodParticle.Play();

            if (enemyHealth <= 0)
            {
                StartCoroutine(DieAction());
            }
        }
    }

    IEnumerator DieAction()
    {
        anim.SetTrigger("die");
        gameObject.GetComponent<BoxCollider2D>().enabled = false; //artýk collisioný çalýþmayacak.

        InstantiateObject();

        yield return new WaitForSeconds(0.4f);

        gameObject.SetActive(false);
    } //ölüyo ve sonra diriliyo


    private void PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        
        
        
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
        
            if (state != State.attack)
            {
                state = State.attack;
            }
          
        }
        else
        {
            if (state != State.walk)
            {
                state = State.walk;
                anim.SetBool("moving", true);
            }
        }
    }

    private void OnDrawGizmos() //visualizing range of enemy
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //if player in range, damage her
        anim.SetBool("moving", false);
        anim.SetTrigger("attack");
            playerHealth.TakeDamage(damage);
            player.GetComponent<PlayerMovement>().PlayBloodParticle();
            //state = State.walk;
        
    }
    private IEnumerator DirectionChange()
    {
        anim.SetBool("moving", false);

        yield return new WaitForSeconds(2f);

     
            idleTimer = 0;
            movingLeft = !movingLeft;
        

    }
    private void MoveInDirection(int _direction) //sor
    {
        anim.SetBool("moving", true);

        gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x) * _direction, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

        gameObject.transform.position = new Vector3(gameObject.transform.position.x + Time.deltaTime * _direction * speed,
            gameObject.transform.position.y, gameObject.transform.position.z); 

    }

    private void InstantiateObject()
    {
        if (Random.Range(0,2) < 1)
        {
            if(Random.Range(0,2) < 1)
            {
                Instantiate(CollectibleSpell, transform.position + Vector3.down * 2f, Quaternion.identity, null); // new Vector3(0,-1,0)
            }
            else
            {
                Instantiate(CollectibleHealth, transform.position + Vector3.down * 2f, Quaternion.identity, null);
            }
        }
    }

}
