using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ASimpleStateController : MonoBehaviour
{
    [SerializeField] protected AState initialState;
    [SerializeField] protected AState _currentState;
    [SerializeField] protected List<AState> states = new List<AState>();


    public virtual UnitStateController UnitController { get; }
    public virtual BotStateController BotController { get; }

    protected virtual void OnEnable()
    {
        for (int i = 0; i < states.Count; i++)
            states[i].Init(this);
    }

    protected virtual void Start()
    {
        _currentState = initialState;
        _currentState.StartState();
    }

    protected virtual void Update()
    {
        if(_currentState != null)
            _currentState.Execute();
    }

    public virtual void ChangeState(StateType newState)
    {
        _currentState.Stop();
        _currentState = states.Where(s => s.StateType == newState).FirstOrDefault();
        _currentState.StartState();
    }
   
    public AState GetState(StateType state)
    {
        return states.Where(s => s.StateType == state).FirstOrDefault();
    }

}
