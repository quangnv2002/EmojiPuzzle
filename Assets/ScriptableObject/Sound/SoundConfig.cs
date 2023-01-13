using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundConfig : ScriptableObject
{
    public SoundMaps[] sound_maps;
}

public enum SoundType
{
    ClickButton,
    Win,
    Lose,
    Background
}

[System.Serializable]
public class SoundMaps
{
    public SoundType sound_type;
    public AudioClip audio_clip;

    public SoundType GetSoundType()
    {
        return sound_type;
    }
    public AudioClip GetAudioClip()
    {
        return audio_clip;
    }

}