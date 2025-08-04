using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventario Barcos", menuName = "ScriptableObjects/Inventarios/Barcos")]

public class InventoryBarcos : Inventory<SerialBarco, Barcos>
{
    public override void Add(Barcos item)
    {
        SerialBarco newBarco = new(item);
        Debug.Log(inventoryList);
        inventoryList.Add(newBarco);
        

    }

    public override void Add(Barcos item, int quantity)
    {
        throw new System.NotImplementedException();
    }

    public override void Remove(Barcos item)
    {
        throw new System.NotImplementedException();
    }

    public override void Remove(Barcos item, int quantity)
    {
        throw new System.NotImplementedException();
    }



    public void InitList() {
        inventoryList = new();
        SerialBarco barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Bergantin.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Bote.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Brulote.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Carabela.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Carraca.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Filibote.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Galeaza.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Galeon.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Galeota.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Galera.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Laud.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Nao.asset"));
        this.inventoryList.Add(barco);
        barco = new(AssetDatabase.LoadAssetAtPath<Barcos>("Assets/Scripts/Barcos SO/Zabra.asset"));
        this.inventoryList.Add(barco);
    }

}
