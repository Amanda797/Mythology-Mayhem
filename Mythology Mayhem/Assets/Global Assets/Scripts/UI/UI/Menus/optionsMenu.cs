using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class optionsMenu : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider ambianceVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] Slider combatVolumeSlider;
    [SerializeField] Slider footstepVolumeSlider;
    [SerializeField] TMP_Dropdown graphicsDropdown;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] AudioMixer audioMixer;

    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");

        masterVolumeSlider.value = gameManager.optionsData.masterVolume;
        musicVolumeSlider.value = gameManager.optionsData.musicVolume;
        ambianceVolumeSlider.value = gameManager.optionsData.sfxVolume;
        sfxVolumeSlider.value = gameManager.optionsData.sfxVolume;
        combatVolumeSlider.value = gameManager.optionsData.enemyVolume;
        footstepVolumeSlider.value = gameManager.optionsData.footstepVolume;
        graphicsDropdown.value = gameManager.optionsData.graphics;
        fullscreenToggle.isOn = gameManager.optionsData.fullscreen;
        resolutionDropdown.value = gameManager.optionsData.resolution;
    }

    public void MasterVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.masterVolume = sliderValue;
        audioMixer.SetFloat("MasterVolume", sliderValue);
        gameManager.SaveOptionsData();
    }
    public void MusicVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.musicVolume = sliderValue;
        audioMixer.SetFloat("MusicVolume", sliderValue);
        gameManager.SaveOptionsData();
    }
    public void AmbianceVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.ambianceVolume = sliderValue;
        audioMixer.SetFloat("AmbianceVolume", sliderValue);
        gameManager.SaveOptionsData();
    }
    public void SFXVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.sfxVolume = sliderValue;
        audioMixer.SetFloat("SoundEffectVolume", sliderValue);
        gameManager.SaveOptionsData();
    }
    public void CombatVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.enemyVolume = sliderValue;
        audioMixer.SetFloat("EnemyVolume", sliderValue);
        gameManager.SaveOptionsData();
    }
    public void FootstepsVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.footstepVolume = sliderValue;
        audioMixer.SetFloat("FootstepVolume", sliderValue);
        gameManager.SaveOptionsData();
    }
    public void GraphicsChanged(int value)
    {
        gameManager.optionsData.graphics = graphicsDropdown.value;
        gameManager.SaveOptionsData();
    }
    public void FullScreenChanged(bool value)
    {
        gameManager.optionsData.fullscreen = fullscreenToggle.isOn;
        gameManager.SaveOptionsData();
    }
    public void ResolutionChanged(int value)
    {
        gameManager.optionsData.resolution = resolutionDropdown.value;
        gameManager.SaveOptionsData();
    }


}
