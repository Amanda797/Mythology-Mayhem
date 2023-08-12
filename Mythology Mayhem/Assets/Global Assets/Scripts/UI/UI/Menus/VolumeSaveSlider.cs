using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSaveSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    void Start() {
        if(volumeSlider != null)
        {
            LoadVolume();
        }
    }

    public void SaveVolume() {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeValue);
        LoadVolume();
    }

    public void LoadVolume() {
        float volumeValue = PlayerPrefs.GetFloat("Volume");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }

    public void SaveVolume(float volume) {
        PlayerPrefs.SetFloat("Volume", volume/10);
        LoadVolume(volume);
    }

    public void LoadVolume(float volume) {
        AudioListener.volume = volume;
    }
    
}