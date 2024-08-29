using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

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
    [SerializeField] Resolution[] resolutions;
    float currentRefreshRate;
    float soundStart;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        resolutions = new Resolution[Screen.resolutions.Length];
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutions[i] = Screen.resolutions[resolutions.Length - i - 1];
        }
        currentRefreshRate = Screen.currentResolution.refreshRate;
    }
    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");

        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        fullscreenToggle.isOn = Screen.fullScreen;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();        

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                string resolutionOption = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(resolutionOption);
                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    resolutionDropdown.value = i;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
    }

    public void UpdateAudioSliders()
    {
        Debug.Log("UpdateAudioSliders");
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");
        masterVolumeSlider.value = gameManager.optionsData.masterVolume;
        musicVolumeSlider.value = gameManager.optionsData.musicVolume;
        ambianceVolumeSlider.value = gameManager.optionsData.ambianceVolume;
        sfxVolumeSlider.value = gameManager.optionsData.sfxVolume;
        combatVolumeSlider.value = gameManager.optionsData.enemyVolume;
        footstepVolumeSlider.value = gameManager.optionsData.footstepVolume;
        graphicsDropdown.value = gameManager.optionsData.graphics;
        fullscreenToggle.isOn = gameManager.optionsData.fullscreen;
    }

    public void MasterVolumeChanged(float sliderValue)
    {
        float value = Mathf.Round(sliderValue * 10f) / 10f;
        masterVolumeSlider.value = value;
        gameManager.optionsData.masterVolume = value;
        audioMixer.SetFloat("MasterVolume", LinearToDecibel(value));
    }
    public void MusicVolumeChanged(float sliderValue)
    {
        float value = Mathf.Round(sliderValue * 10f) / 10f;
        musicVolumeSlider.value = value;
        gameManager.optionsData.musicVolume = value;
        audioMixer.SetFloat("MusicVolume", LinearToDecibel(value));
    }
    public void AmbianceVolumeChanged(float sliderValue)
    {
        float value = Mathf.Round(sliderValue * 10f) / 10f;
        ambianceVolumeSlider.value = value;
        gameManager.optionsData.ambianceVolume = value;
        audioMixer.SetFloat("AmbianceVolume", LinearToDecibel(value));
    }
    public void SFXVolumeChanged(float sliderValue)
    {
        float value = Mathf.Round(sliderValue * 10f) / 10f;
        sfxVolumeSlider.value = value;
        gameManager.optionsData.sfxVolume = value;
        audioMixer.SetFloat("SoundEffectVolume", LinearToDecibel(value));
        audioSource.clip = sfxClips[UnityEngine.Random.Range(0, sfxClips.Length)];
        audioSource.volume = 1f;
        audioSource.outputAudioMixerGroup = sfx;
        StartCoroutine(PlayDelayed(0.2f));
    }
    public void CombatVolumeChanged(float sliderValue)
    {
        float value = Mathf.Round(sliderValue * 10f) / 10f;
        combatVolumeSlider.value = value;
        gameManager.optionsData.enemyVolume = value;
        audioMixer.SetFloat("EnemyVolume", LinearToDecibel(value));
        audioSource.clip = combatClips[UnityEngine.Random.Range(0, combatClips.Length)];
        audioSource.volume = 1f;
        audioSource.outputAudioMixerGroup = combat;
        StartCoroutine(PlayDelayed(0.2f));
    }
    public void FootstepsVolumeChanged(float sliderValue)
    {
        float value = Mathf.Round(sliderValue * 10f) / 10f;
        footstepVolumeSlider.value = value;
        gameManager.optionsData.footstepVolume = value;
        audioMixer.SetFloat("FootstepVolume", LinearToDecibel(value));
        audioSource.clip = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
        audioSource.volume = .5f;
        audioSource.outputAudioMixerGroup = footstep;
        StartCoroutine(PlayDelayed(0.2f));
    }
    public void GraphicsChanged(int value)
    {
        gameManager.optionsData.graphics = value + 1;
        QualitySettings.SetQualityLevel(value+1);
    }
    public void FullScreenChanged(bool value)
    {
        gameManager.optionsData.fullscreen = value;
        Screen.fullScreen = value;
    }
    public void ResolutionChanged(int value)
    {
        Resolution resolution = resolutions[value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    private float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }

    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20.0f);

        return linear;
    }

    private IEnumerator PlayDelayed(float input)
    {
        float SFX = gameManager.optionsData.sfxVolume;
        float footsteps = gameManager.optionsData.footstepVolume;
        float combat = gameManager.optionsData.enemyVolume;
        yield return new WaitForSecondsRealtime(input);

        if (SFX == gameManager.optionsData.sfxVolume && footsteps == gameManager.optionsData.footstepVolume && combat == gameManager.optionsData.enemyVolume)
        {
            audioSource.Play();
        }
    }
}
