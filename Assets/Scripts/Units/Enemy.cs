using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitComponent
{
    /// <summary>
    /// Выбранная цель для действий : Движение к цели; Атака цели;
    /// </summary>
    public Transform GetSelectedGoal;
}
