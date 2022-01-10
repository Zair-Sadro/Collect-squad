using UnityEngine;

public enum StateCondition
{
    None, Executing, Stopped
}

public enum StateType
{
    None, UnitChase, UnitAttack, Die, BotFindTile, BotTowerChoose
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
