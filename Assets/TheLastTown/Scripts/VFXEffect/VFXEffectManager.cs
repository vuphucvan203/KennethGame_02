using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum VFXEffectType
{
    Explosion,
}

public class VFXEffectManager : KennMonoBehaviour
{
    private static VFXEffectManager instance;
    public static VFXEffectManager Instance => instance;
    [SerializeField] protected List<VFXEffect> activeEffects;
    [SerializeField] protected List<VFXEffect> poolingEffects;
    [SerializeField] protected Transform holder;

    private void Awake()
    {
        CreateSingleton();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        activeEffects = Resources.LoadAll<VFXEffect>("Prefabs/VFXEffects").ToList();
        holder = transform.Find("Holder").GetComponent<Transform>();
    }

    protected void CreateSingleton()
    {
        if (instance == null) instance = this;
        else Debug.LogWarning("VFXEffectManager already exists");
    }

    public void SpawnEffect(VFXEffectType type, Vector3 pos)
    {
        VFXEffect effect = activeEffects.Find(e => e.VFXEffectType == type);
        VFXEffect newEffect = GetEffectFromPooling(effect);
        newEffect.gameObject.SetActive(true);
        newEffect.transform.position = pos;
        
        if (newEffect != null)
        {
            newEffect.Play();
        }
    }   

    public void ReturnEffectToPooling(VFXEffect effect)
    {
        if (poolingEffects.Contains(effect)) return;
        poolingEffects.Add(effect);
        effect.transform.SetParent(holder);
        effect.gameObject.SetActive(false);
    }

    protected VFXEffect GetEffectFromPooling(VFXEffect effect)
    {
        if (poolingEffects.Count > 0)
        {
            VFXEffect vfxEffect = poolingEffects.Find(e => e.VFXEffectType == effect.VFXEffectType);
            poolingEffects.Remove(vfxEffect);
            return vfxEffect;
        }

        VFXEffect newEffect = Instantiate(effect);
        newEffect.transform.SetParent(holder);
        newEffect.name = effect.name;
        return newEffect;
    }
}
