using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
   None, Menu, Core, Win, Lose
}

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private UserData data;
    [SerializeField] private GameState currentState;
    [SerializeField] private PlayerController player;
    [SerializeField] private UIController uiController;

    private event Action<GameState> OnStateChange;

    #region Properties

    public static UserData Data { get => Instance.data;}

    public PlayerController Player => player;

    public GameState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            OnStateChange?.Invoke(value);
        }
    }

    #endregion

    private void Awake()
    {
        #region Singleton

        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance.gameObject);

        DontDestroyOnLoad(this);

        #endregion

        InitUI();
    }

    private void InitUI()
    {
        uiController.Init(this);
    }

    private void OnEnable()
    {
        OnStateChange += ChangeState;
    }

    private void OnDestroy()
    {
        OnStateChange -= ChangeState;
    }

    private void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                OnMenuState();
                break;

            case GameState.Core:
                OnCoreState();
                break;

            case GameState.Win:
                OnWinState();
                break;

            case GameState.Lose:
                OnLoseState();
                break;

            case GameState.None:
                Debug.LogError($"GameState is none");
                break;
        }
    }

    #region State Methods

    private void OnMenuState()
    {

    }

    private void OnCoreState()
    {

    }

    private void OnWinState()
    {

    }

    private void OnLoseState()
    {

    }

    #endregion
}