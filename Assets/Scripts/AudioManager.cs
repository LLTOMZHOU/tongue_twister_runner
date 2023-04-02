using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip[] musicClips;
    
    public AudioClip hurtClip;
    public AudioClip notHurtClip;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(int index)
    {
        musicSource.clip = musicClips[index];
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    
    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    public void PlayHurt()
    {
        PlaySFX(hurtClip);
    }
    
    public void PlayNotHurt()
    {
        PlaySFX(notHurtClip);
    }
    
}
