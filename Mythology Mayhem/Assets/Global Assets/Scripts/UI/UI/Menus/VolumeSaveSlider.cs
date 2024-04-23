using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSaveSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float currentVolume;
    [SerializeField] private Slider volumeSlider;

    public bool loaded;

    void Update() {
        if(volumeSlider != null)
        {
            if (GameManager.instance != null)
            {
                if (GameManager.instance.gameData.loaded)
                {
                    if (!loaded)
                    {
                        LoadVolume();
                    }
                }
            }
        }

        if(gameObject.scene.name == "MainMenu" || gameObject.scene.name == "TitleSequence")
        {
            //SaveVolume(18);
        }
    }

    public void SaveVolume() {
        audioMixer.GetFloat("MasterVolume", out currentVolume);
        //PlayerPrefs.SetFloat("Volume", volumeValue);
        //LoadVolume();
    }

    public void LoadVolume() {
        //float volumeValue = PlayerPrefs.GetFloat("Volume");
        currentVolume = GameManager.instance.gameData.masterVolume;
        audioMixer.SetFloat("MasterVolume", currentVolume);
        volumeSlider.value = currentVolume;
        loaded = true;
    }

    public void SaveVolume(float volume) {
        //PlayerPrefs.SetFloat("Volume", volume);
        //LoadVolume(volume);
    }

    public void LoadVolume(float volume) {
        //AudioListener.volume = volume;
    }

    public void UpdateVolume() 
    {
        currentVolume = volumeSlider.value;
        audioMixer.SetFloat("MasterVolume", currentVolume);
        if (GameManager.instance != null) 
        {
            GameManager.instance.gameData.masterVolume = currentVolume;
        }
    }
    
}