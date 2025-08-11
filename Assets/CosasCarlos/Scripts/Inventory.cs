using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[Serializable]
public abstract class Inventory<SerialType, ItemType> : ScriptableObject where SerialType : InventoryItem<ItemType> where ItemType : Item
{

    [SerializeField]
    public List<SerialType> inventoryList; /*= new List<SerialType>();*/ //por redundancia de tipos, ya que C# lo permite, se puede poner new(); ya que tanto a la izq como a der tenemos List<T>

    

    public abstract void Add(ItemType item);
    public abstract void Add(ItemType item, int quantity);





    public abstract void Remove(ItemType item);

    public abstract void Remove(ItemType item, int quantity);


    public SerialType findItem(ItemType item)
    {
        SerialType found = null;
        if (inventoryList != null)
        {
            foreach (SerialType serial in inventoryList)
            {
                if (serial.itemInventory == item)
                {
                    found = serial;
                    break;
                }
            }
        }
        return found;
    }
}
