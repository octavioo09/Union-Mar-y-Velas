using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Currency", menuName = "ScriptableObjects/Currency")]
public class currencySO : ScriptableObject
{
    public string CurrencyName;

    public float CurrencyQuantity;

    public string CurrencyDescription;
}
