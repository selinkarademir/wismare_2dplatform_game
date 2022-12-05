using UnityEngine;

public class MushroomTrap : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform poisonPoint;
    [SerializeField] private GameObject[] poison;
    private float cooldownTimer;
    private int poisonIndex;

    private void Attack()
    {
        cooldownTimer = 0;
        FindPoison();
     
    }
    private void FindPoison()
    {
        for (int i = 0; i < poison.Length; i++)
        {
            if (!poison[i].activeInHierarchy)
            {
                poisonIndex = i;
                poison[poisonIndex].transform.position = poisonPoint.position;
                poison[poisonIndex].GetComponent<MushroomProjectile>().ActiveProjectile();
                break;
            }
        }

    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCoolDown)
            Attack();
    }
}
