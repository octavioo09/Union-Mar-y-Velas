using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventario Servicios", menuName = "ScriptableObjects/Inventarios/Servicios")]

public class InventoryServiceSO : Inventory<SerialService, ServiceSO>
{
    //public InventoryServiceSO() { }

    public override void Add(ServiceSO item)
    {
        SerialService found = findItem(item);

        if (found != null || (found != null && found.hasService == true ))
        {
            // modal
        }
        else
        {
            found = new(item);
            inventoryList.Add(found);
        }

    }

    public override void Add(ServiceSO item, int quantity)
    {
        throw new System.NotImplementedException();
    }

    public override void Remove(ServiceSO item) { }


    public override void Remove(ServiceSO item, int quantity)
    {
        throw new System.NotImplementedException();
    }

}
