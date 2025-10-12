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
    
    public void PlayBeatTune(int index, float pitch)
    {
        if (BeatboxTunes != null && BeatboxTunes.Count > 0 && index >= 0 && index < BeatboxTunes.Count)
        {
            UISource.clip = BeatboxTunes[index];
            UISource.pitch = pitch;
            UISource.loop = false;
            UISource.Play();
        }
        
    }
}
