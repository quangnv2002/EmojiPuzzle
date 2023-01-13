using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public SoundConfig sound_config;
    public AudioSource music_source;
    public AudioSource sfx_source;

    [SerializeField] int channelCount;
    [SerializeField] AudioSource[] channels;

    [SerializeField] AudioClip[] audioClips;

    public float SFXVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("-SFX-", 1f);
        }
        set
        {
            PlayerPrefs.SetFloat("-SFX-", value);
        }
    }

    public float MusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("-Music-", 1f);
        }
        set
        {
            PlayerPrefs.SetFloat("-Music-", value);
        }
    }


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        PlayMusic("Background");
        InitChannel();
    }
    private void Update()
    {
        music_source.volume = MusicVolume;
    }


    void InitChannel()
    {
        channels = new AudioSource[channelCount];
        for (int i = 0; i < channelCount; i++)
        {
            GameObject channel = new GameObject("Channel_" + i);
            AudioSource source = channel.AddComponent<AudioSource>();
            channels[i] = source;
            channel.transform.parent = gameObject.transform;
        }
    }


    public void PlaySFX(int index)
    {
        int channelPlayingCount = 0;
        for (int i = 0; i < channels.Length; i++)
        {
            if (channels[i].isPlaying)
            {
                channelPlayingCount += 1;
            }
            else
            {
                channels[i].clip = audioClips[index];
                channels[i].Play();
                break;
            }
        }
    }

    public void PlayMusic(string _music_name)
    {
        foreach (SoundMaps sm in sound_config.sound_maps)
        {
            if (sm.sound_type.ToString() == _music_name)
            {
                music_source.volume = MusicVolume;
                music_source.clip = sm.audio_clip;
                music_source.Play();
            }
        }
    }


    public void PlaySFX(string _sfx_name)
    {
        foreach (SoundMaps sm in sound_config.sound_maps)
        {
            if (sm.sound_type.ToString() == _sfx_name)
            {
                sfx_source.volume = SFXVolume;
                sfx_source.clip = sm.audio_clip;
                sfx_source.PlayOneShot(sfx_source.clip);
            }
        }
    }

    public void ToggleMusic()
    {
        music_source.mute = !music_source.mute;
        MusicVolume = music_source.mute ? 0 : PlayerPrefs.GetFloat("-Music");
    }
    public void ToggleSFX()
    {
        sfx_source.mute = !sfx_source.mute;
        SFXVolume = sfx_source.mute ? 0 : PlayerPrefs.GetFloat("-SFX-");
    }
    public void ChangeMusicVolume(float _volume)
    {
        //music_source.volume = _volume;
        MusicVolume = _volume;
    }
    public void ChangeSFXVolume(float _volume)
    {
        //sfx_source.volume = _volume;
        SFXVolume = _volume;

    }
    public int CheckMusicMute()
    {
        if (music_source.mute)
        {
            return 1;
            //PlayerPrefs.SetFloat("-Music-", 0f);
        }
        else
        {
            return 0;
            //PlayerPrefs.SetFloat("-Music-", 0f);
        }
    }
    public int CheckSFXMute()
    {
        if (sfx_source.mute)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
