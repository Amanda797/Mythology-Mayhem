using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FountainScript : MonoBehaviour
{
    GameManager gameManager;
    AudioSource audioSource;
    PlayerStats playerStats;
    bool canHeal = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");
    }
    void Update()
    {
        if (playerStats != null)
        {
            float distance = Vector2.Distance(transform.position, playerStats.transform.position);

            float per = ((5 - distance) / distance) + 1;

            audioSource.volume = per;

            if (distance < 6 && !canHeal)
            {
                if (gameManager.gameData.curHealth < gameManager.gameData.maxHealth)
                {
                    gameManager.Popup("Press E to Heal", true);
                    canHeal = true;
                }
                else gameManager.Popup("Full Health", true);
            }
            else if (distance > 6 && canHeal)
            {
                gameManager.Popup("", false);
                canHeal = false;
            }
        }
        if (!canHeal) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            canHeal = false;
            gameManager.Popup("", false);
            playerStats.Heal(100, false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            playerStats = other.GetComponent<PlayerStats>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.Stop();
            playerStats = null;
        }
    }
}
