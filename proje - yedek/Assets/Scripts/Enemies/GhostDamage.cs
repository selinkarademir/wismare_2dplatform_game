using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDamage : MonoBehaviour
{

    private bool active = true;
    [SerializeField] private float damage;
  
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }

    }
}
