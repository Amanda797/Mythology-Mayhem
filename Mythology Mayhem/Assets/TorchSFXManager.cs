using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSFXManager : MonoBehaviour
{
    [SerializeField] AudioSource[] torchAudioSources;

    public void ToggleAudioSources(bool activate)
    {
        foreach (AudioSource source in torchAudioSources)
        {
            if(activate) source.Play();
            else source.Stop();
        }
    }
}
