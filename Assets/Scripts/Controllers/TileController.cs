using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class TileController : MonoBehaviour
{
    [SerializeField] private int tilePerUpgrade;
    [SerializeField,Range(5,100)] private int maxTiles;
    [SerializeField] private int priceForTiles;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text currentTilesText;


    private UserData _data;
    private MainMenu _mainMenu;

    private void Start()
    {
        priceText.SetText(priceForTiles.ToString());
        _data = GameController.Data;
        _mainMenu = (MainMenu)GameController.Instance.UIController.Menus.Where(m => m.Type == MenuType.Menu).FirstOrDefault();

        UpdateCurrentTiles();
    }

    private void UpdateCurrentTiles()
    {
        currentTilesText.SetText(_data.MaxTiles.ToString());
    }

    public void BuyTile()
    {
        if(_data.Coins < priceForTiles)
        {
            priceText.transform.DOShakePosition(1, 5);
            priceText.DOColor(Color.red, 1).From(Color.white).OnComplete(() => priceText.color = Color.white);
            return;
        }

        if(_data.MaxTiles > maxTiles)
        {
            priceText.text = "MAX";
            currentTilesText.transform.DOShakePosition(1, 5);
            currentTilesText.DOColor(Color.red, 1).From(Color.white).OnComplete(() => currentTilesText.color = Color.white);
            return;
        }

        _data.Coins -= priceForTiles;
        _data.MaxTiles += tilePerUpgrade;
        _mainMenu.UpdateCoins();
        UpdateCurrentTiles();
        SaveController.SaveData();
    }
}
