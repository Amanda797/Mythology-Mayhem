using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSHealth : MonoBehaviour
{ // --------------------------
    // ***PROPERTIES***
    // --------------------------

    [Header("Player Stats")]
    [SerializeField] HealthUIController huic;
    [SerializeField] public PlayerStats_SO ps;

    [SerializeField] Vector3 spawnPoint = Vector3.zero;
    public AudioSource healSource;

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
            //ps.CurrHealth = ps.MaxHealth;
        }
        else Debug.LogWarning("Can't find huic's player stats so");
    }//end on awake


    private void Start()
    {
        spawnPoint = gameObject.transform.position;
    }

    public void SetHealth(float h) {
        GameManager.instance.gameData.saveData.playerData.curHealth = h;
    }// end set health

    public float GetHealth() {
        return GameManager.instance.gameData.saveData.playerData.curHealth;
    }//end get health

    public void TakeDamage(float d) {
        GameManager.instance.gameData.saveData.playerData.curHealth -= d;
        huic.UpdateHealth();
        if (GameManager.instance.gameData.saveData.playerData.curHealth <= 0) {
            Death();
        }
    }//end take damage

    public void Heal(float h, bool potion) {
        if (potion)
        {
            healSource.Play();
        }

        GameManager.instance.gameData.saveData.playerData.curHealth = Mathf.Clamp(GameManager.instance.gameData.saveData.playerData.curHealth += h, 0 , GameManager.instance.gameData.saveData.playerData.maxHealth);

        huic.UpdateHealth();
    }

    public void Death()
    {
        if (GetHealth() <= 0) {
            GetComponent<PlayerMovement3D>().enabled = false;
            gameObject.transform.position = spawnPoint;
            Heal(GameManager.instance.gameData.saveData.playerData.maxHealth, false);
            GetComponent<PlayerMovement3D>().enabled = true;
        }
    }
}
