using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public currencySO mycurrency;
    public CurrencyUI mycurrencyUI;

    public void Start()
    {
        mycurrency = AssetDatabase.LoadAssetAtPath<currencySO>("Assets/CosasCarlos/Scriptable Objects/Player/Currency.asset");
        CurrencyUI obj = Instantiate(mycurrencyUI);
        obj.currency = this;
        obj.transform.SetParent(this.gameObject.transform, false);
        obj.currencyText = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        mycurrencyUI = obj;
    }


    //public void Update()
    //{

    //    mycurrencyUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mycurrency.CurrencyQuantity.ToString();

    //}
    public void SetupItemListUI()
    {
        //GameObject newRow = Instantiate(rowPrefab, scroll);

        


    }
}
