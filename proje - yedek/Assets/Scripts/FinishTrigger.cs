using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{

    GameController GC;
    // Start is called before the first frame update
    void Start()
    {
        StartMethods();
    }

    #region StartMethods

    void StartMethods()
    {
        GetManagers();
    }

    void GetManagers()
    {
        GC = GameController.instance; 
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GC.EndGameActions();
        }
    }

}
