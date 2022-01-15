using UnityEngine;

public enum StateCondition
{
    None, Executing, Stopped
}

public enum StateType
{
    None = 0,
    UnitChase = 1,
    UnitAttack = 2,
    Die = 3,
    BotFindTile = 4,
    BotTowerChoose = 5,
    Idle = 6
}

public abstract class AState : MonoBehaviour
{
    [SerializeField] protected StateCondition stateCondition;

    protected ASimpleStateController _stateController;

    public abstract StateType StateType { get; }

    public abstract void Init(ASimpleStateController stateController);

    public abstract void StartState();

    public abstract void Execute();
    public abstract void Stop();
}
