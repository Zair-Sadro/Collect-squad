using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<ABaseUI> menus = new List<ABaseUI>();

    private GameController _gameController;
    private UserData _data;

    public List<ABaseUI> Menus => menus;
    public GameController GameController => _gameController;

    public void Init(GameController controller, UserData data)
    {
        _gameController = controller;
        _data = data;

        foreach (var menu in menus)
            menu.Init(this, _data);
    }

    public void ToggleMenu(MenuType type)
    {
        for (int i = 0; i < menus.Count; i++)
        {
            if (menus[i].Type == type)
                menus[i].gameObject.SetActive(true);
            else
                menus[i].gameObject.SetActive(false);
        }
            
    }

}
