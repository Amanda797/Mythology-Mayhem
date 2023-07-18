using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int CurrentHealth { get; set; }
    public int CurrentMana { get; set; }
    public float[] CurrentPosition { get; set; } // stores player's vector3 position
    public float NextAttackTime { get; set; }
    public bool Flipped { get; set; }
    //public int CurrentWeapon { get; set; }
    
    //public bool[] LevelOneScrollsCollected { get; set; }
    //public bool[] LevelTwoScrollsCollected { get; set; }
    //public bool[] LevelThreeScrollsCollected { get; set; }
    //public bool[] LevelFourScrollsCollected { get; set; }
    //TODO: Need to know how many scrolls will exist in each level so that the arrays are definitive. Order also needs to be determined for consistency.
    
    //public float SpecialAbility1CurrentCooldown { get; set; }


    //Optional: Achievement Stats
    
    public PlayerData(PlayerStats player) {
        //CurrentHealth = player.CurrHealth;
        //NextAttackTime = player.NextAttackTime;
    }

    
} // end Player Data class
