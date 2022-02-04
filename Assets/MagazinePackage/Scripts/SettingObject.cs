using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StateProduct
{
    Locked,
    Use,
    InUse,
    Price
}

public class SettingObject : MonoBehaviour
{
    [Header("ID Object Product")]
    public int objectID;
    [Header("Object Product")]
    public GameObject objectProduct;
    [Header("State Product")]
    public StateProduct stateProduct;
    [Header("PriceProduct")]
    public int priceProduct;


    [SerializeField] private StorePriceObject priceObj;
    [SerializeField] private GameObject particle;
    [SerializeField] public bool skin = true;

    private Vector3 vectorRotateObject = new Vector3(0.0f, 10.0f, 0.0f);
    private Transform pointObjectProduct;
    private MarketManager _market;


    public void Init(MarketManager market)
    {
        _market = market;
    }

    void Start()
    {
        pointObjectProduct = transform.Find("PointObject");

        if (objectProduct != null)
        {
            objectProduct.transform.position = pointObjectProduct.transform.position;
        }

       // if (objectID == 1)
       //     _market.ChangeProduct(this.gameObject);
    }

    void Update()
    {
        RotateObject();
        UpdateText();
    }

    public void SetNewInUseObj()
    {
        particle.SetActive(true);

        if (skin)
            PlayerPrefs.SetInt("BodySkin_ID", objectID);
        else
            PlayerPrefs.SetInt("GunSkin_ID", objectID);
    }

    public void DeactiveParticle(SettingObject obj)
    {
        if (skin == obj.skin)
            particle.SetActive(false);
    }

    /// <summary>
    /// обновление текста на плашках
    /// </summary>
    void UpdateText()
    {
        string currentStateProduct;

        if (stateProduct == StateProduct.Price)
        {
            priceObj.ToggleCoinImg(true);
            currentStateProduct = priceProduct.ToString();
        }
        else
        {
            priceObj.ToggleCoinImg(false);
            currentStateProduct = stateProduct.ToString();
        }
        priceObj.SetStatusText(currentStateProduct);
    }

    /// <summary>
    /// вращение обьекта над плашкой
    /// </summary>
    void RotateObject()
    {
        if (objectProduct != null)
        {
            objectProduct.transform.Rotate(vectorRotateObject * Time.deltaTime);
        }
    }


    private void OnMouseDown()
    {
        if (_market)
            _market.ChangeProduct(this.gameObject);
    }

}
