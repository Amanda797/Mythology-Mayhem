using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    [Tooltip("Number between 0 and 10")]
    [SerializeField] float volume;

    [Tooltip("Drag the correct Player Stats scriptable object")]
    [SerializeField] PlayerStats_SO ps;
    [SerializeField] VolumeSaveSlider vs;

    // Start is called before the first frame update
    void Awake()
    {
        //Set default audio volume
        if(vs == null) {
            AudioListener.volume = volume;
            PlayerPrefs.SetInt("Volume", (int) volume);
        }
        //Set Player Health to 100%
        if(ps != null) {
            ps.CurrHealth = ps.MaxHealth;
        }
    }
    
}
