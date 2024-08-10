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
    [SerializeField] AudioMixerGroup sfx, combat, footstep;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] sfxClips, combatClips, footstepClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
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
    }
    public void MusicVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.musicVolume = sliderValue;
        audioMixer.SetFloat("MusicVolume", sliderValue);
    }
    public void AmbianceVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.ambianceVolume = sliderValue;
        audioMixer.SetFloat("AmbianceVolume", sliderValue);
    }
    public void SFXVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.sfxVolume = sliderValue;
        audioMixer.SetFloat("SoundEffectVolume", sliderValue);
        audioSource.clip = sfxClips[UnityEngine.Random.Range(0, sfxClips.Length)];
        audioSource.volume = gameManager.sfxAudioSource.volume;
        audioSource.outputAudioMixerGroup = sfx;
        audioSource.Play();
    }
    public void CombatVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.enemyVolume = sliderValue;
        audioMixer.SetFloat("EnemyVolume", sliderValue);
        audioSource.clip = combatClips[UnityEngine.Random.Range(0, combatClips.Length)];
        audioSource.volume = 1f;
        audioSource.outputAudioMixerGroup = combat;
        audioSource.Play();
    }
    public void FootstepsVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.footstepVolume = sliderValue;
        audioMixer.SetFloat("FootstepVolume", sliderValue);
        audioSource.clip = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
        audioSource.volume = .5f;
        audioSource.outputAudioMixerGroup = footstep;
        audioSource.Play();
    }
    public void GraphicsChanged(int value)
    {
        gameManager.optionsData.graphics = graphicsDropdown.value;
    }
    public void FullScreenChanged(bool value)
    {
        gameManager.optionsData.fullscreen = fullscreenToggle.isOn;
    }
    public void ResolutionChanged(int value)
    {
        gameManager.optionsData.resolution = resolutionDropdown.value;
    }
}
