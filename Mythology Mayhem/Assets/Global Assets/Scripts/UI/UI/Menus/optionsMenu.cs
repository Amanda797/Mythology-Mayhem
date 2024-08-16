using TMPro;
using System.Collections;
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

    float soundStart;

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
        audioMixer.SetFloat("MasterVolume", LinearToDecibel(sliderValue));
    }
    public void MusicVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.musicVolume = sliderValue;
        audioMixer.SetFloat("MusicVolume", LinearToDecibel(sliderValue));
    }
    public void AmbianceVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.ambianceVolume = sliderValue;
        audioMixer.SetFloat("AmbianceVolume", LinearToDecibel(sliderValue));
    }
    public void SFXVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.sfxVolume = sliderValue;
        audioMixer.SetFloat("SoundEffectVolume", LinearToDecibel(sliderValue));
        audioSource.clip = sfxClips[UnityEngine.Random.Range(0, sfxClips.Length)];
        audioSource.volume = 1f;
        audioSource.outputAudioMixerGroup = sfx;
        StartCoroutine(PlayDelayed(0.2f));
    }
    public void CombatVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.enemyVolume = sliderValue;
        audioMixer.SetFloat("EnemyVolume", LinearToDecibel(sliderValue));
        audioSource.clip = combatClips[UnityEngine.Random.Range(0, combatClips.Length)];
        audioSource.volume = 1f;
        audioSource.outputAudioMixerGroup = combat;
        StartCoroutine(PlayDelayed(0.2f));
    }
    public void FootstepsVolumeChanged(float sliderValue)
    {
        gameManager.optionsData.footstepVolume = sliderValue;
        audioMixer.SetFloat("FootstepVolume", LinearToDecibel(sliderValue));
        audioSource.clip = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
        audioSource.volume = .5f;
        audioSource.outputAudioMixerGroup = footstep;
        StartCoroutine(PlayDelayed(0.2f));
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
        print(footsteps.ToString() + " " + gameManager.optionsData.footstepVolume.ToString());
        if (SFX == gameManager.optionsData.sfxVolume && footsteps == gameManager.optionsData.footstepVolume && combat == gameManager.optionsData.enemyVolume)
        {
            audioSource.Play();
        }
    }
}
