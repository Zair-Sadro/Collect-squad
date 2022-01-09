﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleUnit
{
    Transform Transform { get; }
    UnitType Type { get; }
    IDamageable Damageable { get; }
    ITeamChangeable TeamObject { get; }

}
