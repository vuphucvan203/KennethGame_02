using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUI : KennMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI point;
    [SerializeField] protected List<Image> reactiveArmorPoints;
    [SerializeField] protected List<Image> rengerationPoints;
    [SerializeField] protected List<Image> muscleBootsPoints;
    [SerializeField] protected List<Image> rapidFirePoints;
    [SerializeField] protected List<Image> flashStepPoints;
    [SerializeField] protected List<Sprite> skillPointSprites;
    [SerializeField] protected Button acceptButton;
    [SerializeField] protected Button declineButton;
    [SerializeField] protected bool isActive = false;
    protected int pointTotal;
    [SerializeField] protected int reactiveArmorPoint;
    [SerializeField] protected int rengerationPoint;
    [SerializeField] protected int muscleBootsPoint;
    [SerializeField] protected int rapidFirePoint;
    [SerializeField] protected int flashStepPoint;

    protected void Update()
    {
        if (isActive)
        {
            isActive = false;
            pointTotal = Player.Instance.controller.Soldier.Skills.skillPointTotal;
            HiddenButton();
        }
        point.SetText(pointTotal.ToString());
    }

    protected void OnEnable()
    {
        isActive = true;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        point = transform.Find("Point").GetComponentInChildren<TextMeshProUGUI>();
        reactiveArmorPoints = transform.Find("Container/ReactiveArmor/Points").GetComponentsInChildren<Image>().ToList();
        rengerationPoints = transform.Find("Container/Rengeration/Points").GetComponentsInChildren<Image>().ToList();
        muscleBootsPoints = transform.Find("Container/MuscleBoots/Points").GetComponentsInChildren<Image>().ToList();
        rapidFirePoints = transform.Find("Container/RapidFire/Points").GetComponentsInChildren<Image>().ToList();
        flashStepPoints = transform.Find("Container/FlashStep/Points").GetComponentsInChildren<Image>().ToList();
        skillPointSprites = Resources.LoadAll<Sprite>("Sprite/SkillPoint").ToList();
        acceptButton = transform.Find("Accept").GetComponent<Button>();
        declineButton = transform.Find("Decline").GetComponent<Button>();
    }

    public void UpReactiveArmorSkill()
    {
        Skills skills = Player.Instance.controller.Soldier.Skills;
        int maxPoint = skills.reactiveArmor.maxSkillPoints;
        int min = Player.Instance.controller.Soldier.Skills.reactiveArmor.skillPoint;
        ShowButton();
        if (pointTotal > 0 && (min + reactiveArmorPoint) != maxPoint)
        {
            pointTotal--;
            reactiveArmorPoint++;
            if (reactiveArmorPoint > maxPoint) reactiveArmorPoint = maxPoint;
            for (int i = min; i < min + reactiveArmorPoint; i++)
            {
                reactiveArmorPoints[i].sprite = skillPointSprites[0];
            }
        }
    }

    public void UpRengerationSkill()
    {
        Skills skills = Player.Instance.controller.Soldier.Skills;
        int maxPoint = skills.rengeration.maxSkillPoints;
        int min = Player.Instance.controller.Soldier.Skills.rengeration.skillPoint;
        ShowButton();
        if (pointTotal > 0 && (min + rengerationPoint) != maxPoint)
        {
            pointTotal--;
            rengerationPoint++;
            if (rengerationPoint > maxPoint) rengerationPoint = maxPoint;
            for (int i = min; i < min + rengerationPoint; i++)
            {
                rengerationPoints[i].sprite = skillPointSprites[0];
            }
        }
    }

    public void UpMuscleBootsSkill()
    {
        Skills skills = Player.Instance.controller.Soldier.Skills;
        int maxPoint = skills.muscleBoots.maxSkillPoints;
        int min = Player.Instance.controller.Soldier.Skills.muscleBoots.skillPoint;
        ShowButton();
        if (pointTotal > 0 && (min + muscleBootsPoint) != maxPoint)
        {
            pointTotal--;
            muscleBootsPoint++;
            if (muscleBootsPoint > maxPoint) muscleBootsPoint = maxPoint;
            for (int i = min; i < min + muscleBootsPoint; i++)
            {
                muscleBootsPoints[i].sprite = skillPointSprites[0];
            }
        }
    }

    public void UpRapidFireSkill()
    {
        Skills skills = Player.Instance.controller.Soldier.Skills;
        int maxPoint = skills.rapidFire.maxSkillPoints;
        int min = Player.Instance.controller.Soldier.Skills.rapidFire.skillPoint;
        ShowButton();
        if (pointTotal > 0 && (min + rapidFirePoint) != maxPoint)
        {
            pointTotal--;
            rapidFirePoint++;
            if (rapidFirePoint > maxPoint) rapidFirePoint = maxPoint;
            for (int i = min; i < min + rapidFirePoint; i++)
            {
                rapidFirePoints[i].sprite = skillPointSprites[0];
            }
        }
    }

    public void UpFlashStepSkill()
    {
        Skills skills = Player.Instance.controller.Soldier.Skills;
        int maxPoint = skills.flashStep.maxSkillPoints;
        int min = Player.Instance.controller.Soldier.Skills.flashStep.skillPoint;
        ShowButton();
        if (pointTotal > 0 && (min + flashStepPoint) != maxPoint)
        {
            pointTotal--;
            flashStepPoint++;
            if (flashStepPoint > maxPoint) flashStepPoint = maxPoint;
            for (int i = min; i < min + flashStepPoint; i++)
            {
                flashStepPoints[i].sprite = skillPointSprites[0];
            }
        }
    }

    protected void ShowButton()
    {
        if (pointTotal > 0)
        {
            acceptButton.interactable = true;
            declineButton.interactable = true;
        }    
    }    

    protected void HiddenButton()
    {
        acceptButton.interactable = false;
        declineButton.interactable = false;
    }

    public void AcceptHandle()
    {
        HiddenButton();
        Player.Instance.controller.Soldier.Skills.skillPointTotal = pointTotal;
        Player.Instance.controller.Soldier.Skills.reactiveArmor.UpdateSkill(Player.Instance.controller.Soldier, reactiveArmorPoint);
        Player.Instance.controller.Soldier.Skills.rengeration.UpdateSkill(Player.Instance.controller.Soldier, rengerationPoint);
        Player.Instance.controller.Soldier.Skills.muscleBoots.UpdateSkill(Player.Instance.controller.Soldier, muscleBootsPoint);
        Player.Instance.controller.Soldier.Skills.rapidFire.UpdateSkill(Player.Instance.controller.Soldier, rapidFirePoint);
        Player.Instance.controller.Soldier.Skills.flashStep.UpdateSkill(Player.Instance.controller.Soldier, flashStepPoint);
        ResetUISkillPoint();

    }

    public void DeclineHandle()
    {
        int min_1, min_2, min_3, min_4, min_5;
        int max_1, max_2, max_3, max_4, max_5;
        min_1 = Player.Instance.controller.Soldier.Skills.reactiveArmor.skillPoint;
        max_1 = Player.Instance.controller.Soldier.Skills.reactiveArmor.maxSkillPoints;
        min_2 = Player.Instance.controller.Soldier.Skills.rengeration.skillPoint;
        max_2 = Player.Instance.controller.Soldier.Skills.rengeration.maxSkillPoints;
        min_3 = Player.Instance.controller.Soldier.Skills.muscleBoots.skillPoint;
        max_3 = Player.Instance.controller.Soldier.Skills.muscleBoots.maxSkillPoints;
        min_4 = Player.Instance.controller.Soldier.Skills.rapidFire.skillPoint;
        max_4 = Player.Instance.controller.Soldier.Skills.rapidFire.maxSkillPoints;
        min_5 = Player.Instance.controller.Soldier.Skills.flashStep.skillPoint;
        max_5 = Player.Instance.controller.Soldier.Skills.flashStep.maxSkillPoints;
        isActive = true;
        HiddenButton();
        for (int i = min_1; i < max_1; i++) reactiveArmorPoints[i].sprite = skillPointSprites[1];
        for (int i = min_2; i < max_2; i++) rengerationPoints[i].sprite = skillPointSprites[1];
        for (int i = min_3; i < max_3; i++) muscleBootsPoints[i].sprite = skillPointSprites[1];
        for (int i = min_4; i < max_4; i++) rapidFirePoints[i].sprite = skillPointSprites[1];
        for (int i = min_5; i < max_5; i++) flashStepPoints[i].sprite = skillPointSprites[1];
        ResetUISkillPoint();
    }

    protected void ResetUISkillPoint()
    {
        reactiveArmorPoint = 0;
        rengerationPoint = 0;
        muscleBootsPoint = 0;
        rapidFirePoint = 0;
        flashStepPoint = 0;
    }    
}

