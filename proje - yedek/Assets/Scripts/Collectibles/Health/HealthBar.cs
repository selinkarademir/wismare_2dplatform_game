using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //fillAmount library


public class HealthBar : MonoBehaviour
{

    [SerializeField] private Health playerHealth; //unity'den player classýný çekebilmek için
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;


    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
   
}


