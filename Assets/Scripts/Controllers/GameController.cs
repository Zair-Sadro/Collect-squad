using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

public enum GameState
{
   None, Menu, Core, Win, Lose
}

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private UserData data;
    [SerializeField] private GameObject winConfetti;
    [SerializeField] private GameState currentState;
    [SerializeField] private PlayerController player;
    [SerializeField] private TowerPlatformsMain playerMainTowersController;
    [SerializeField] private TowerPlatformsMain botMainTowersController;
    [SerializeField] private UIController uiController;
    [SerializeField] private int defCoinsForWin;
    [SerializeField] private int defCoinsForLose;



    private event Action<GameState> OnStateChange;

    private int _sessionScore;

    #region Properties

    public static UserData Data { get => Instance.data;}
    public static int SessionCoins => Instance._sessionScore;
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

       // DontDestroyOnLoad(this);

        #endregion

        InitUI();
    }

    private void InitUI()
    {
        uiController.Init(this, data);
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
        uiController.ToggleMenu(MenuType.Menu);
    }

    private void OnCoreState()
    {
        uiController.ToggleMenu(MenuType.Core);
    }

    private void OnWinState()
    {
        var winPanel = (WinMenu)uiController.Menus.Where(m => m.Type == MenuType.Win).FirstOrDefault();

        uiController.ToggleMenu(MenuType.Win);
        playerMainTowersController.StopTowerActivity();
        botMainTowersController.StopTowerActivity();
        winConfetti.SetActive(true);

        _sessionScore += defCoinsForWin;
        data.Coins += _sessionScore;
        data.CurrentLevel++;
        data.WinsToNextRank++;

        winPanel.SetSessionScore(_sessionScore);
        SaveController.SaveData();
    }

    private void OnLoseState()
    {
        var losePanel = (LoseMenu)uiController.Menus.Where(m => m.Type == MenuType.Lose).FirstOrDefault();

        uiController.ToggleMenu(MenuType.Lose);
        playerMainTowersController.StopTowerActivity();
        botMainTowersController.StopTowerActivity();

        _sessionScore += defCoinsForLose;
        data.Coins += _sessionScore;

        losePanel.SetSessionScore(_sessionScore);
        SaveController.SaveData();
    }

    #endregion

    public void OnMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnLevelLoad(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void AddSessionCoins(int value)
    {
        Instance._sessionScore += value;
    }

    public static int GetSessionScore()
    {
        return Instance._sessionScore;
    }
}