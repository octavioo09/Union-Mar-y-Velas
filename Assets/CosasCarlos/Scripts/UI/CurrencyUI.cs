using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [HideInInspector]
    public Currency currency;
    [HideInInspector]
    public TextMeshProUGUI currencyText;


    //public void Start()
    //{
    //    currency.SetupItemListUI();
    //}
    public void Update()
    {
        currencyText.text = currency.mycurrency.CurrencyQuantity.ToString();
    }
}
