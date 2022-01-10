using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private GameState endState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BattleUnit unit))
            GameController.Instance.CurrentState = endState;
    }
}
