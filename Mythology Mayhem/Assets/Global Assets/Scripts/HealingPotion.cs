using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealingPotion : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] bool smallPotion;
    [SerializeField] int healthAmount = 2;

    private void Start()
    {
        if (!smallPotion) healthAmount = 4;
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing");
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.GetComponent<PlayerStats>() != null)
        {
            if (gameManager.gameData.curHealth < gameManager.gameData.maxHealth)
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
            if (gameManager.gameData.curHealth < gameManager.gameData.maxHealth)
            {
                other.gameObject.GetComponent<FPSHealth>().Heal(healthAmount, true);
                gameObject.SetActive(false);
            }
        }
    }
}
