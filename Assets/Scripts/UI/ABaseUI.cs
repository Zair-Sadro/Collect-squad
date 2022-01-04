using UnityEngine;


public enum MenuType
{
    Menu, Core, Win, Lose
}

public abstract class ABaseUI : MonoBehaviour
{
    [SerializeField] protected MenuType type;

    protected GameController _controller;

    public MenuType Type => type;
    public abstract void Init(GameController gameController);

}
