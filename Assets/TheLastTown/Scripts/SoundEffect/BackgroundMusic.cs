using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum BackgroundMusicType
{
    Desert,
    Forest,
    Town,
}

public class BackgroundMusic : KennMonoBehaviour
{
    [SerializeField] protected BackgroundMusicType backgroundMusicType;
    public BackgroundMusicType Type { get => backgroundMusicType; set => backgroundMusicType = value; }
    [SerializeField] protected AudioSource audioSource;
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

    private void OnValidate()
    {
        transform.name = backgroundMusicType.ToString();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.2f;
    }

    public void Play()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            audioSource.loop = true;
        }
    }
    
    public void Stop()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }
    }

    public void ChangeVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume += volume;
            if (audioSource.volume > 1) audioSource.volume = 1;
            else if (audioSource.volume < 0) audioSource.volume = 0;
        }
    }
}
