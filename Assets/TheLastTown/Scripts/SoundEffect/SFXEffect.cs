using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum SFXEffectType
{
    FuelExplosion,
    GunShot,
    RiffleShot,
    Flame,
    BatHit,
    ZombiePain,
    KnifeCut,
    ZombieBreath,
    MonsterGrowl,
}

public class SFXEffect : KennMonoBehaviour
{
    [SerializeField] protected SFXEffectType SFXEffectType;
    public SFXEffectType Type { get => SFXEffectType; set => SFXEffectType = value; }
    [SerializeField] protected AudioSource audioSource;
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

    public bool isPlaying;

    private void OnValidate()
    {
        transform.name = SFXEffectType.ToString();
    }

    protected void Update()
    {
        if (isPlaying)
        {
            isPlaying = false;
            Play();
        }
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
        }
    }
    
    public void Stop()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
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
