using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Almacen")]

public class AlmacenSO : ServiceSO
{
    InventoryProductSO almacenInventory;
    public double occupied;
    public double capacity;
    public Boolean available = true;
}
