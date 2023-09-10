using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeartState : MonoBehaviour
{
    //Fields

    [SerializeField] private int _playerMaxHealth;
    [SerializeField] private int _playerCurrHealth;

    [SerializeField] Image heartPrefab;
    [SerializeField] Transform heartsContainer;
    [SerializeField] List<Image> hearts = new List<Image>();
    [SerializeField] Sprite[] heart_states;
    // Necessary order of sprites in editer array:
        // heart_states[0] = heart1 (full heart)
        // heart_states[1] = heart2 (1 quarter-to full heart)
        // heart_states[2] = heart3 (half-empty heart)
        // heart_states[3] = heart4 (1 quarter-to empty heart)
        // heart_states[4] = heart5 (empty heart)
    [SerializeField] PlayerStats ps;

    //Properties

    public int PlayerCurrHealth{
        get {
           return _playerCurrHealth; 
        }
        set {
            _playerCurrHealth = Mathf.Clamp(value, 0, PlayerMaxHealth);
            SetHealthBar(PlayerCurrHealth);
        }
    }

    public int PlayerMaxHealth{
        get {
            return _playerMaxHealth;
        }
        set {
            _playerMaxHealth = Mathf.Max(0, value);
            UpdateHealthBarCount(PlayerMaxHealth);
        }
    }

    //Methods

    void Awake() {
        //ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        ps = FindObjectOfType<PlayerStats>();

        PlayerCurrHealth = PlayerMaxHealth;
        PlayerMaxHealth = PlayerMaxHealth;
    }//end awake

    void OnValidate() {
        PlayerCurrHealth = PlayerCurrHealth;
        PlayerMaxHealth = PlayerMaxHealth;
    }

    private void SetHealthBar(int health) {
        for (int i = 0; i < hearts.Count; i++)
        {
            int remainderHealth = Mathf.Clamp(health - (i * 4), 0, 4);
    
            switch (remainderHealth)
            {
                case 0:
                    hearts[i].sprite = heart_states[4];
                    break;
                case 1:
                    hearts[i].sprite = heart_states[3];
                    break;
                case 2:
                    hearts[i].sprite = heart_states[2];
                    break;
                case 3:
                    hearts[i].sprite = heart_states[1];
                    break;
                case 4:
                    hearts[i].sprite = heart_states[0];
                    break;
            };
        }
    }// end set health bar

     //Used to update the amount of Heart sprites in the Health Bar automatically.
    public void UpdateHealthBarCount(int maxHealth)
    {
        //Convert maxHealth "quater hearts" value into whole hearts value.
        //Ceil to ensure less than a whole heart still spawns heart object
        maxHealth = Mathf.CeilToInt(maxHealth / 4.0f);
        
        int heartCount = hearts.Count;
    
        if(maxHealth > heartCount)
        {
            int heartsToAdd = maxHealth - heartCount;
    
            for(int i = 0; i < heartsToAdd; i++)
            {
                hearts.Add(Instantiate(heartPrefab.gameObject, heartsContainer).GetComponent<Image>());
            }
            SetHealthBar(PlayerMaxHealth); //Since we added hearts, update heart graphics so the new hearts respect current health value.
        }
        else if(maxHealth < heartCount)
        {
            int heartsToRemove = heartCount - maxHealth;
            for(int i = 0; i < heartsToRemove; i++)
            {
                int lastHeartIndex = hearts.Count - 1;
                Destroy(hearts[lastHeartIndex].gameObject);
                hearts.RemoveAt(lastHeartIndex);
            }
        }
    }// end update health bar count


} // PlayerHeartState
