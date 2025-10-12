using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("References")]
    public AudioSource BackgroundSource;
    public AudioSource PlayerSource;
    public AudioSource UISource;
    public AudioSource EffectSource;

    [Header("UI")]
    public List<AudioClip> BeatboxTunes;
    public AudioClip ButtonClick;

    [Header("Player")]
    public AudioClip PlayerWalk;
    public AudioClip PlayerJump;


    public void PlayPlayerWalk(bool isActive)
    {
        if (isActive && !PlayerSource.isPlaying)
        {
            PlayerSource.clip = PlayerWalk;
            PlayerSource.loop = true;
            PlayerSource.Play();
        }
        else if (!isActive && PlayerSource.isPlaying)
        {
            PlayerSource.Stop();
        }
    }

    public void PlayPlayerJump()
    {
        PlayerSource.clip = PlayerJump;
        PlayerSource.loop = false;
        PlayerSource.Play();
    }

    public void PlayButtonClick()
    {
        UISource.clip = ButtonClick;
        UISource.loop = false;
        UISource.Play();
    }
    
    public void PlayBeatTune(AudioClip beatTune, float pitch)
    {
        UISource.Stop();
        UISource.clip = beatTune;
        UISource.pitch = pitch;
        UISource.loop = false;
    
    }
}
