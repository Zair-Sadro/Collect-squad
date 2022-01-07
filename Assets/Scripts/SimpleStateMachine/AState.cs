using UnityEngine;

public enum StateCondition
{
    None, Executing, Stopped
}

public enum StateType
{
    None, Chase, Attack, Die
}

public abstract class AState : MonoBehaviour
{
    [SerializeField] protected StateCondition stateCondition;
    [SerializeField] protected StateType stateType;

    protected ASimpleStateController _stateController;

    public StateType StateType => stateType;

    public abstract void Init(ASimpleStateController stateController);

    public abstract void StartState();

    public abstract void Execute();
    public abstract void Stop();
}
