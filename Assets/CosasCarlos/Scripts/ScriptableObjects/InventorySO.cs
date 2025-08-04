using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventorySO<SerialType, ItemType> : ScriptableObject where SerialType : InventoryItem<ItemType> where ItemType : Item
{
    public List<SerialType> inventoryList;
}
