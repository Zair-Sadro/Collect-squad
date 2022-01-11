using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuilderUnit : MonoBehaviour, IBattleUnit, ITeamChangeable, IDamageable
{
    [SerializeField] private UnitTeam team;
    [SerializeField] private float health;
    [SerializeField] private GameOverTrigger overTrigger;
    [SerializeField] private UnityEvent OnDieEvent;

    private float _currentHealth;

    private bool _isSpotable = false;

    #region Properties

    public ITeamChangeable TeamObject => this;

    public UnitTeam MyTeam => team;

    public Transform Transform => transform;

    public UnitType Type => UnitType.Builder;

    public IDamageable Damageable => this;

    public bool IsSpotable => _isSpotable;


    #endregion

    private void Start()
    {
        _currentHealth = health;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            OnDieEvent?.Invoke();
            GameController.Instance.CurrentState = overTrigger.EndState;
        }
    }

    public void AllowGetAttacked()
    {
        _isSpotable = true;
    }
}
