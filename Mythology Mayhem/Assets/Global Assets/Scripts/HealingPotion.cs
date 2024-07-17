using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] bool smallPotion;
    [SerializeField] int healthAmount = 2;

    private void Start()
    {
        if (!smallPotion) healthAmount = 4;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.GetComponent<PlayerStats>() != null)
        {
            if (GameManager.instance.gameData.saveData.playerData.curHealth < GameManager.instance.gameData.saveData.playerData.maxHealth)
            {
                other.gameObject.GetComponent<PlayerStats>().Heal(healthAmount, true);
                gameObject.SetActive(false);
            }
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FPSHealth>() != null)
        {
            if (GameManager.instance.gameData.saveData.playerData.curHealth < GameManager.instance.gameData.saveData.playerData.maxHealth)
            {
                other.gameObject.GetComponent<FPSHealth>().Heal(healthAmount, true);
                gameObject.SetActive(false);
            }
        }
    }
}
