using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SaveData
{
    public int[] priceProduct = new int[9];
    public StateProduct[] stateProduct = new StateProduct[9];
}


public class MarketManager : MonoBehaviour
{
    [SerializeField] private UserData data;

    private RaycastHit hit;
    private Ray MyRay;
    private Vector3 cameraPos;
    private GameObject currentObjectInUse;  
    private int ID;             

    [SerializeField]
    private Transform cameraPosition;   
    [SerializeField]
    private TextMeshProUGUI textAllPoints;         


    [Header("Array Bars")]
    [SerializeField]
    private GameObject[] Bars;          

    [Header("Array Sprites Background")]
    [SerializeField]
    private Sprite[] backGround;


    private void Awake()
    {
        for (int i = 0; i < Bars.Length; i++)
            Bars[i].GetComponent<SettingObject>().Init(this);
    }

    void Start()
    {
        LoadData();

        if (cameraPosition != null) cameraPos = new Vector3(-7.0f, cameraPosition.position.y, cameraPosition.position.z);

        foreach (GameObject gObject in Bars)
        {
            

            if (GetStateProduct(gObject) == StateProduct.InUse)
            {
                currentObjectInUse = gObject;         
                ID = GetIDObject(currentObjectInUse);

               break;
            }
        }

        if (!PlayerPrefs.HasKey("NewTargetPrice"))
            PlayerPrefs.SetInt("NewTargetPrice", 50);

        foreach (GameObject gObject in Bars)
        {
            if (gObject.GetComponent<SettingObject>().priceProduct > data.Coins)
            {
                PlayerPrefs.SetInt("NewTargetPrice", gObject.GetComponent<SettingObject>().priceProduct);
                break;
            }
        }
    }

    void Update()
    {
        UpdateTextAllPoints();

        if (cameraPosition != null) cameraPosition.position = Vector3.Lerp(cameraPosition.position, cameraPos, 0.05f);
    }

  
    public void ReturnScene(int indexScene)
    {
        SaveData();
        SceneManager.LoadScene(indexScene);  
    }
    

 
    private int GetIDObject(GameObject _gameObject)
    {
        int result = 0;
        result = _gameObject.GetComponent<SettingObject>().objectID;
        _gameObject.GetComponent<SettingObject>().SetNewInUseObj();
        return result;
    }

  
    public void ChangeProduct(GameObject gObject)
    {
        StateProduct stateProduct = GetStateProduct(gObject);

        if (stateProduct == StateProduct.InUse || stateProduct == StateProduct.Locked) // если используем или заблокирован то выход
        {
            Debug.Log("StateObject " + stateProduct);
            return;
        }


        if (stateProduct == StateProduct.Price)                                       // если есть деньги то покупаем
        {
            int currentPriceObject = gObject.GetComponent<SettingObject>().priceProduct;

            if (data.Coins >= currentPriceObject)
            {
                data.Coins -= currentPriceObject;
                //gObject.GetComponent<SettingObject>().stateProduct = StateProduct.Use;

                SetStateProduct(gObject, StateProduct.Use);

            }
            Debug.Log("StateObject " + stateProduct);

            UpdateSpritesBackGround(Bars, backGround);         // обновление подложки текста
            return;
        }

        if (stateProduct == StateProduct.Use)
        {
            SetStateProduct(gObject, StateProduct.InUse);               //если мона использовать то используем 

            if (currentObjectInUse)
            {
                SetStateProduct(currentObjectInUse, StateProduct.Use);
            }
            currentObjectInUse = gObject;                          // установим текущий обьект InUse
            ID = GetIDObject(currentObjectInUse);                 // установим ID текущего InUse
            UpdateSpritesBackGround(Bars, backGround);         // обновление подложки текста
        }
    }


    /// <summary>
    /// возврат состо€ни€ продукта
    /// </summary>
    /// <param name="gObject"></param>
    /// <returns></returns>
    private StateProduct GetStateProduct(GameObject gObject)
    {
        StateProduct result;

        result = gObject.GetComponent<SettingObject>().stateProduct;
        return result;
    }

    /// <summary>
    /// ”становка статуса плашки
    /// </summary>
    /// <param name="gObject"></param>
    /// <param name="stateProduct"></param>
    private void SetStateProduct(GameObject gObject, StateProduct stateProduct)
    {
        gObject.GetComponent<SettingObject>().stateProduct = stateProduct;
        gObject.GetComponent<SettingObject>().DeactiveParticle(gObject.GetComponent<SettingObject>());
    }

    /// <summary>
    /// —мена подложки текста на плашках (барах)
    /// </summary>
    /// <param name="arrayObjects"></param>
    /// <param name="arraySprites"></param>
    void UpdateSpritesBackGround(GameObject[] arrayObjects, Sprite[] arraySprites)
    {
       // foreach (GameObject gameObject in arrayObjects)
       // {
       //     int indexArraySprites = (int)gameObject.GetComponent<SettingObject>().stateProduct;
       //
       //     gameObject.GetComponentInChildren<SpriteRenderer>().sprite = arraySprites[indexArraySprites];
       //
       // }

    }

    /// <summary>
    /// «агрузка данных магазина
    /// </summary>
    void LoadData()
    {
        string key = "SavedStateProduct";

        if (PlayerPrefs.HasKey(key))
        {
            string value = PlayerPrefs.GetString(key);
            SaveData data = JsonUtility.FromJson<SaveData>(value);

            for (int index = 0; index < Bars.Length; index++)
            {
                Bars[index].GetComponent<SettingObject>().stateProduct = data.stateProduct[index];
                Bars[index].GetComponent<SettingObject>().priceProduct = data.priceProduct[index];

            }
        }

        key = "AllPoint";
    }

    /// <summary>
    /// сохранение данных магазина
    /// </summary>
    void SaveData()
    {
        string key = "SavedStateProduct";
        //currentIDInUse = GetIDObject(currentObjectInUse);
        SaveData data = new SaveData();

        for (int index = 0; index < Bars.Length; index++)
        {
            data.stateProduct[index] = Bars[index].GetComponent<SettingObject>().stateProduct;
            data.priceProduct[index] = Bars[index].GetComponent<SettingObject>().priceProduct;
        }

        string value = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, value);

        key = "AllPoint";
        PlayerPrefs.SetInt("SavedID", ID);
        PlayerPrefs.Save();
        SaveController.SaveData();
        Debug.Log("Data saved!");
    }

    
    void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data reset complete");
    }



    void UpdateTextAllPoints()
    {
        textAllPoints.text = data.Coins.ToString();
    }

}

