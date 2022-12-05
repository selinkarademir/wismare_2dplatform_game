using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private bool isLevelDone;
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private ParticleSystem runTrail;
    [SerializeField] private ParticleSystem dashTrail;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 15f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 2f;
    [SerializeField] private int jumpCounter;
    [SerializeField] private TrailRenderer tr;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider; 
    private float horizontalInput;

    public static PlayerMovement instance;
    GameController GC;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        //grab references for rigifbody and animator from player object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        GC = GameController.instance;
    }

    private void Update()
    {
        if (!isLevelDone && !isDashing)
        {

            float horizontalInput = Input.GetAxis("Horizontal");
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);


            if (GC.data.playerData.isDoubleJumpActive)
            {
                if (Input.GetKeyDown(KeyCode.W) && jumpCounter < 1)
                {
                    Jump();
                    jumpCounter++;

                }
               
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
                {
                    Jump();

                }
            }
           

            if (IsGrounded())
            {
                runTrail.Play();
                jumpCounter = 0;
            }
            else
            {
                runTrail.Stop();
            }

            //make player flip when moving horizontally
            if (horizontalInput > 0.01f)       //sa�a giderken              
                transform.localScale = new Vector3(25, 25, 1);
            else if (horizontalInput < -0.01f)       //sola giderken              
                transform.localScale = new Vector3(-25, 25, 1);

            //set animator parameters 
            anim.SetBool("run", horizontalInput != 0); //code is shorter and clearer
            anim.SetBool("grounded", IsGrounded());

            if (Input.GetKeyDown(KeyCode.LeftShift) &&  canDash)
            {
                Debug.Log("update dash");
                StartCoroutine(Dash());
            }

        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
      
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;

    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && IsGrounded();
    }

    public void PlayBloodParticle()
    {
        bloodParticle.Play();
    }

    private IEnumerator Dash()
    {
        Debug.Log("method dash");
        dashTrail.Play();
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        body.gravityScale = originalGravity;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        isDashing = false;
        dashTrail.Stop();
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        Debug.Log("method dash end ");


    }

    public void EndGameAction()
    {
       body.velocity = Vector2.zero;
       isLevelDone = true;
    }


}
