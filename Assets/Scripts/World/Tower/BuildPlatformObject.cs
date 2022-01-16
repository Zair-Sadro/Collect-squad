using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildPlatformObject : ATowerObject, IDamageable, ITeamChangeable, IBattleUnit
{
    [SerializeField] private float maxHealth;

    private UnitTeam _team;
    private float _currentHealth;

    #region Properties

    public bool IsSpotable => true;

    public Transform Transform => this.transform;

    public UnitType Type => UnitType.Tower;

    public IDamageable Damageable => this;

    public ITeamChangeable TeamObject => this;

    public UnitTeam MyTeam => _team;

    public bool IsDamageable => true;

    #endregion

    public override void Init(TowerBuildPlatform buildPlatform)
    {
        base.Init(buildPlatform);
        _currentHealth = maxHealth;
        _team = buildPlatform.CurrentTeam;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
            _currentBuildPlatform.DestroyByUnits?.Invoke();
    }
}
