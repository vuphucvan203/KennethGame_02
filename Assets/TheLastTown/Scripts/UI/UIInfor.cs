using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInfor : KennMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI level;
    [SerializeField] protected Slider bar;
    [SerializeField] protected TextMeshProUGUI attack;
    [SerializeField] protected TextMeshProUGUI defense;
    [SerializeField] protected TextMeshProUGUI speed;


    protected void Update()
    {
        if (Player.Instance == null) return;
        level.text = "LVL " + Player.Instance.controller.Soldier.Level.value.ToString();
        bar.value = Player.Instance.controller.Soldier.Experience.Percent;
        attack.text = Player.Instance.controller.Soldier.AttackStats.value.ToString();
        defense.text = Player.Instance.controller.Soldier.DefenseStats.value.ToString();
        speed.text = Player.Instance.controller.Soldier.SpeedStats.value.ToString();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        level = transform.Find("Level/Title").GetComponent<TextMeshProUGUI>();
        bar = transform.Find("Level/Bar").GetComponent<Slider>();
        attack = transform.Find("Stats/Attack/StatsInfor/Stats/Text").GetComponent<TextMeshProUGUI>();
        defense = transform.Find("Stats/Defense/StatsInfor/Stats/Text").GetComponent<TextMeshProUGUI>();
        speed = transform.Find("Stats/Speed/StatsInfor/Stats/Text").GetComponent<TextMeshProUGUI>();
    }


}
