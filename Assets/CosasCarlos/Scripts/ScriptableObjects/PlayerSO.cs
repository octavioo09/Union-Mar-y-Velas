using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class PlayerSO : ScriptableObject
{
    public currencySO playerCurrency;
    public InventoryProductSO playerInventory;
    public InventoryServiceSO inventoryService;
    public InventoryBarcos flota;
    public Barcos barcoActivo;
    private List<CitySO> visitedCities;
    //private List<Barcos> flota;
}
