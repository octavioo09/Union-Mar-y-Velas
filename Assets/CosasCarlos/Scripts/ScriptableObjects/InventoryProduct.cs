using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "Inventario Producto", menuName = "ScriptableObjects/Inventarios/Productos")]

public class InventoryProductSO : Inventory<SerialProduct, ProductsSO>
{
    //[SerializeField]
    //public List<ProductsSO> inventoryProducts;

    public override void Add(ProductsSO item)
    {
        SerialProduct found = findItem(item);

        if (found != null)
        {
            found.AddToStack();
        }
        else
        {
            found = new(item);
            inventoryList.Add(found);
        }

    }

    public override void Add(ProductsSO item, int quantity)
    {
        SerialProduct found = findItem(item);

        if (found != null)
        {
            found.AddStack(quantity);
        }
        else
        {
            found = new(item);
            inventoryList.Add(found);
            found.AddStack(quantity);
        }
    }

    public override void Remove(ProductsSO item)
    {
        SerialProduct found = findItem(item);
        if (found != null)
        {
            found.RemoveFromStack();
            if (found.stackSize == 0)
                inventoryList.Remove(found);
        }
    }

    public override void Remove(ProductsSO item, int quantity)
    {
        SerialProduct found = findItem(item);
        if (found != null)
        {
            found.RemoveStack(quantity);
            if (found.stackSize == 0)
                inventoryList.Remove(found);
        }
    }


    //public void OnInit()
    //{
    //    foreach(var product in inventoryProducts)
    //    {
    //        SerialProduct newSerial = new SerialProduct(product);
    //        inventory.inventoryList.Add(newSerial);
    //    }
    //}
}
