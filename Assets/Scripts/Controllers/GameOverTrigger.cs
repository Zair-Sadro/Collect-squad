using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private Transform mainTarget;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private GameState endState;
    [SerializeField] private UnityEvent OnFinishPointChecked;

    public GameState EndState => endState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BattleUnit unit))
        {
            OnFinishPointChecked?.Invoke();
            var controller = unit.GetComponent<UnitStateController>();

            unit.FinishInit(mainTarget, chaseSpeed);
            controller.FinishInit(mainTarget, chaseSpeed);
        }

        
    }
}
