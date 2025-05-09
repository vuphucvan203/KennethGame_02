using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuUI : KennMonoBehaviour
{
    [SerializeField] protected RectTransform functionPage;
    [SerializeField] protected RectTransform mainPage;
    [SerializeField] protected Transform panel;
    [SerializeField] protected TextMeshProUGUI windowTitle;
    [SerializeField] protected SkillsUI skillsView;
    [SerializeField] protected ShopUI shopView;

    protected override void Start()
    {
        OnMainPage();
    }

    protected void OnEnable()
    {
        GoToShop();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        functionPage = transform.Find("Panel/FunctionPage").GetComponent<RectTransform>();
        mainPage = transform.Find("Panel/MainPage").GetComponent<RectTransform>();
        panel = transform.Find("Panel").GetComponent<Transform>();
        windowTitle = transform.Find("Panel/FunctionPage/WindowTitle").GetComponentInChildren<TextMeshProUGUI>();
        if (skillsView != null || shopView != null) return;
        skillsView = Transform.FindAnyObjectByType<SkillsUI>();
        shopView = Transform.FindAnyObjectByType<ShopUI>();
    }

    public void OnMainPage()
    {
        mainPage.gameObject.SetActive(true);
        functionPage.gameObject.SetActive(false);
    }

    public void OnFunctionPage()
    {
        mainPage.gameObject.SetActive(false);
        functionPage.gameObject.SetActive(true);
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

