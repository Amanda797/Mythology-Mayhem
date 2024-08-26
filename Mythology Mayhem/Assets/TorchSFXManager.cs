using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSFXManager : MonoBehaviour
{
    [SerializeField] AudioSource[] torchAudioSources;

    public void ToggleAudioSources(bool activate, bool is3D)
    {
        foreach (AudioSource source in torchAudioSources)
        {
            if (activate)
            {
                if (is3D) source.volume = 1f;
                else source.volume = .1f;
                source.Play();
            }

            else source.Stop();
        }
    }
}
