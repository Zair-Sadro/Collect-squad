using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private Transform mainTarget;
    [SerializeField] private BuilderUnit builderUnit;
    [SerializeField] private UnitTeam oppositeTeam;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private GameState endState;
    [SerializeField] private UnityEvent OnFinishPointChecked;

    public GameState EndState => endState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BattleUnit unit))
        {
            if(unit.MyTeam == oppositeTeam)
            {
                OnFinishPointChecked?.Invoke();
                builderUnit.AllowGetAttacked();
                var controller = unit.GetComponent<UnitStateController>();

                unit.FinishInit(mainTarget, chaseSpeed);
                controller.FinishInit(mainTarget, chaseSpeed);
            }
        }
    }
}
