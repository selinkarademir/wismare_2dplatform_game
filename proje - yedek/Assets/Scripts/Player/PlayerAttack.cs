using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
   [SerializeField] private float attackCoolDown;
   private Animator anim;
   private PlayerMovement playerMovement;
   private float cooldownTimer = Mathf.Infinity; // has enough time passed to fire the next shot?
   public GameObject sword;
   [SerializeField] Animator swordAnim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        sword = transform.GetChild(0).gameObject;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer > attackCoolDown && playerMovement.CanAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack"); 
        cooldownTimer = 0;
        StartCoroutine(SwordAction());
    }

    IEnumerator SwordAction()
    {
        sword.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        sword.SetActive(false);

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

    }

}
