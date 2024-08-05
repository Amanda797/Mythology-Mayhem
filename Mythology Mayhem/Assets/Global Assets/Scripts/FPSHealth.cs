using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSHealth : MonoBehaviour
{ // --------------------------
  // ***PROPERTIES***
  // --------------------------
    GameManager gameManager;
    [Header("Player Stats")]
    [SerializeField] HealthUIController huic;
    [SerializeField] public PlayerStats_SO ps;

    [SerializeField] Vector3 spawnPoint = Vector3.zero;
    bool isDead;
    public AudioSource healSource;
    public AudioSource hurtAS;
    public AudioSource dieAS;

    // --------------------------
    // ***METHODS***
    // --------------------------

    void Awake()
    {        
        huic = GameObject.FindGameObjectWithTag("huic").GetComponent<HealthUIController>();
        huic.UpdateHealth();
        if(huic != null) { 
            ps = huic.ps;
            ps.CanAttack = true;
            ps.NextAttackTime = 0;
        }
        else Debug.LogWarning("Can't find huic's player stats so");
    }


    private void Start()
    {
        spawnPoint = gameObject.transform.position;
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing");
    }

    public void SetHealth(float h) {
        gameManager.gameData.curHealth = h;
    }

    public void TakeDamage(float damage) {
        if (gameManager.gameData.curHealth > 0)
        {
            gameManager.gameData.curHealth = Mathf.Clamp(gameManager.gameData.curHealth -= damage, 0, gameManager.gameData.maxHealth);
            huic.UpdateHealth();

            if (gameManager.gameData.curHealth <= 0) Death();
            else hurtAS.Play();
        }
    }

    public void Heal(float h, bool potion)
    {
        gameManager.gameData.curHealth = Mathf.Clamp(gameManager.gameData.curHealth += h, 0 , gameManager.gameData.maxHealth);

        huic.UpdateHealth();
    }

    public void Death()
    {
        if (!isDead)
        {
            isDead = true;
            dieAS.Play();
            GetComponent<PlayerMovement3D>().enabled = false;
            StartCoroutine(Respawn());
        }
    }
    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        isDead = false;
        gameObject.transform.position = spawnPoint;
        Heal(gameManager.gameData.maxHealth, false);
        GetComponent<PlayerMovement3D>().enabled = true;
    }
}
