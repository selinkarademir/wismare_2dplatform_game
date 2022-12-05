using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ActivateTriggerArea());
        }
    }

    IEnumerator ActivateTriggerArea()
    {

        transform.parent.GetComponent<FireTrap>().ActivateAction();
        transform.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        transform.GetComponent<BoxCollider2D>().enabled = true;


    }
}
