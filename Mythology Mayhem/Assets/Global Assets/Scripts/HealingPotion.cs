using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    //[SerializeField] private PlayerStats player;
    [SerializeField] bool smallPotion;
    [SerializeField] int healthAmount = 2;

    // Start is called before the first frame update
    void Start()
    {
        if (!smallPotion) healthAmount = 4;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.GetComponent<PlayerStats>() != null)
        {
            PlayerStats playerStats = other.gameObject.GetComponent<PlayerStats>();
            PlayerStats_SO ps = playerStats.ps;

            if (ps.CurrHealth < ps.MaxHealth)
            {
                playerStats.Heal(healthAmount, true);
                gameObject.SetActive(false);
            }
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FPSHealth>() != null)
        {
            FPSHealth fPSHealth = other.gameObject.GetComponent<FPSHealth>();
            PlayerStats_SO ps = fPSHealth.ps;

            if (ps.CurrHealth < ps.MaxHealth)
            {
                fPSHealth.Heal(healthAmount);
                gameObject.SetActive(false);
            }
        }
    }
}
