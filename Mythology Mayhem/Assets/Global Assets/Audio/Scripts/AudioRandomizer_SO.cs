using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioRandomizer_SO", menuName = "AudioRandomizer_SO", order = 1)]

public class AudioRandomizer_SO : ScriptableObject
{
    public List<AudioClip> audioClips = new();

    public AudioClip PlaySiblings() {
        int randomClip = Random.Range(0, audioClips.Count);
        return audioClips[randomClip];
    }
}
/* 
//[System.Serializable]
//public class AudioClipGroup
//{
 //   public AudioClip mainAudioClip;
  //  public AudioClipGroup parentAudioClip;
   // public List<AudioClipGroup> childrenAudioClips;
} */