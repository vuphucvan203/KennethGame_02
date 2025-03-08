using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAI : Controller
{
    public override void SelectSoldier(Soldier selectedSoldier)
    {
        soldier = selectedSoldier;
        soldier.SetController(ControllerType.AI);
    }
}
