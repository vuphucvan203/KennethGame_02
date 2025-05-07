using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuUI : KennMonoBehaviour
{
    [SerializeField] protected Transform panel;
    [SerializeField] protected TextMeshProUGUI windowTitle;
    [SerializeField] protected SkillsUI skillsView;
    [SerializeField] protected ShopUI shopView;

    protected void OnEnable()
    {
        GoToShop();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        panel = transform.Find("Panel").GetComponent<Transform>();
        windowTitle = transform.Find("Panel/WindowPanel/WindowTitle").GetComponentInChildren<TextMeshProUGUI>();
        if (skillsView != null || shopView != null) return;
        skillsView = Transform.FindAnyObjectByType<SkillsUI>();
        shopView = Transform.FindAnyObjectByType<ShopUI>();
    }

    public void GoToShop()
    {
        shopView.gameObject.SetActive(true);
        skillsView.gameObject.SetActive(false);
        windowTitle.SetText("SHOP");
    }    

    public void GoToSkills()
    {
        skillsView.gameObject.SetActive(true);
        shopView.gameObject.SetActive(false);
        windowTitle.SetText("SKILLS");
    }

    public void GoToBank()
    {
        windowTitle.SetText("BANK");
    }   
    
    public void GoToSetting()
    {
        windowTitle.SetText("SETTING");
    }    

    public void OpenMenu()
    {
        panel.gameObject.SetActive(true);
    }  
    
    public void CloseMenu()
    {
        panel.gameObject.SetActive(false);
    }
}

