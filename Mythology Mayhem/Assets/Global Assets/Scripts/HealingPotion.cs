using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealingPotion : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] AudioClip clip;
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
                AudioSource source = gameManager.GetComponent<AudioSource>();
                source.clip = clip;
                source.Play();
                other.gameObject.GetComponent<PlayerStats>().Heal(healthAmount, true);
                gameObject.SetActive(false);
            }
        }  
    }
}
