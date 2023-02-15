using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] UIMeterScript healthMeter;
    [SerializeField] UIMeterScript manaMeter;
    [SerializeField] PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        print(playerStats.CurrHealth + "/" + playerStats.MaxHealth);
        healthMeter.SetMeter(playerStats.MaxHealth, playerStats.CurrHealth);
        //manaMeter.SetMeter(playerStats.MaxMana, playerStats.CurrMana);
    }

    // Update is called once per frame
    void Update()
    {
        healthMeter.UpdateMeter(playerStats.CurrHealth);
        //manaMeter.UpdateMeter(playerStats.CurrMana);
    }
}
