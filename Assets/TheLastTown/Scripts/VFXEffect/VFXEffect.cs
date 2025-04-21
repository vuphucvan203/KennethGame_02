using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXEffect : KennMonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected VFXEffectType vfxEffectType;
    public VFXEffectType VFXEffectType => vfxEffectType;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        animator = GetComponent<Animator>();
    }

    public void Play()
    {
        if (animator != null)
        {
            animator.Play("Explode");
        }
    }    

    public void Stop()
    {
        VFXEffectManager.Instance.ReturnEffectToPooling(this);
    }
}
