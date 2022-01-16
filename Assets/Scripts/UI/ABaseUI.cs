using UnityEngine;


public enum MenuType
{
    Menu, Core, Win, Lose
}

public abstract class ABaseUI : MonoBehaviour
{
    [SerializeField] protected MenuType type;

    protected UIController _controller;
    protected UserData _data;

    public MenuType Type => type;
    public virtual void Init(UIController Controller, UserData data)
    {
        _controller = Controller;
        _data = data;
    }


}
