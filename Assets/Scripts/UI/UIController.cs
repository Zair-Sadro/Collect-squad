using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<ABaseUI> menus = new List<ABaseUI>();

    private GameController _gameController;

    public void Init(GameController controller)
    {
        _gameController = controller;

        foreach (var menu in menus)
            menu.Init(_gameController);
    }

    public void ToggleMenu(MenuType type)
    {
        for (int i = 0; i < menus.Count; i++)
            menus[i].gameObject.SetActive(menus[i].Type == type);
    }

}
