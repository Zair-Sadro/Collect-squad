﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleUnit
{
    bool IsSpotable { get; }
    Transform Transform { get; }
    UnitType Type { get; }
    IDamageable Damageable { get; }
    ITeamChangeable TeamObject { get; }

}
