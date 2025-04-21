using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : KennMonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Background Music")]
    [SerializeField] protected List<BackgroundMusic> backgroundMusics;
    public BackgroundMusicType backgroundMusicChange;
    protected BackgroundMusicType currentBackgroundMusic;
    [SerializeField] protected bool playBackgroundMusic;

    [Header("SFX Effects")]        
    [SerializeField] protected List<SFXEffect> sFXEffects;
    public SFXEffectType sFXEffectChange;
    protected SFXEffectType currentSFXEffect;
    [SerializeField] protected bool playSFXEffect;

    private void Awake()
    {
        CreateSingleton();
    }

    protected override void Start()
    {
        base.Start();
        playBackgroundMusic = true;
    }

    private void OnValidate()
    {
        transform.name = GetType().ToString();
    }

    protected void Update()
    {
        ChangeBackgroundMusic(backgroundMusicChange);
        if (playBackgroundMusic)
        {
            playBackgroundMusic = false;
            PlayBackgroundMusic(backgroundMusicChange);
        }

        ChangeSFXEffect(sFXEffectChange);
        if (playSFXEffect)
        {
            playSFXEffect = false;
            PlaySFXEffect(sFXEffectChange);
        }
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        backgroundMusics = GetComponentsInChildren<BackgroundMusic>().ToList();
        sFXEffects = GetComponentsInChildren<SFXEffect>().ToList();
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Debug.LogWarning("SoundManager already exists");
    }

    protected void ChangeBackgroundMusic(BackgroundMusicType type)
    {
        if (type != currentBackgroundMusic)
        {
            currentBackgroundMusic = type;
            playBackgroundMusic = true;
            StopBackgroundNotSelect(type);
        }
    }    

    protected void ChangeSFXEffect(SFXEffectType type)
    {
        if (type != currentSFXEffect)
        {
            currentSFXEffect = type;
            playSFXEffect = true;
        }
    }

    protected void PlayBackgroundMusic(BackgroundMusicType type)
    {
        BackgroundMusic music = backgroundMusics.Find(m => m.Type == type);
        music.Play();
    }    

    public void PlaySFXEffect(SFXEffectType type)
    {
        SFXEffect sfx = sFXEffects.Find(m => m.Type == type);
        sfx.Play();
    }

    protected void StopBackgroundNotSelect(BackgroundMusicType type)
    {
        foreach (BackgroundMusic bm in backgroundMusics)
        {
            if (bm.Type != type)
            {
                bm.Stop();
            }
        }
    }    
}
