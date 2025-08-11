//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class Comercio : Building
//{

//    [SerializeField]
//    public InventoryProductSO inventario;
//    //[SerializeField]
//    /*ublic currencySO currencyPlayer;*/

//    //public PlayerSO player;

//    public CityStatsSO citystats;

//    public RowProductComercio rowComprar;
//    public RowProductComercio rowVender;

//    public Transform scroll;

//    public void Comprar(ProductsSO item)
//    {

//        SerialProduct found = inventario.findItem(item);

//        if (found != null)
//        {
//            if (found.stackSize > 1 && player.playerCurrency.CurrencyQuantity >= found.precio)
//            {
//                player.playerInventory.Add(item);
//                inventario.Remove(item);
//                player.playerCurrency.CurrencyQuantity -= found.precio;
//                Debug.Log("Comprando" + found.precio);
//            }
//            //else
//        }
//        //return evento;
//    }

//    public void Vender(ProductsSO item)
//    {

//        SerialProduct found = player.playerInventory.findItem(item);

//        if (found != null)
//        {
//            if (found.stackSize > 0)
//            {
//                inventario.Add(item);
//                player.playerInventory.Remove(item);
//                player.playerCurrency.CurrencyQuantity += found.precio;
//                Debug.Log("Vendiendo" + found.precio);
                
//            }
//            //else
//        }
//    }

//    public void SetupItemListComprarUI()
//    {

//        foreach (Transform item in scroll)
//        {
//            Destroy(item.gameObject);
//        }

//        foreach (var serial in inventario.inventoryList)
//        {
//            //GameObject newRow = Instantiate(rowPrefab, scroll);
//            RowProductComercio obj = Instantiate(rowComprar, scroll);
//            obj.hooverView = hooverView;
//            obj.comercio = this;
//            obj.product = serial.itemInventory;
//            CityStatsSO.Pair pair = citystats.GetPair(obj.product);
//            obj.transform.SetParent(scroll, false);
//            obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
//            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = setPrecioCompra(pair).ToString("F2");
//        }
//    }

//    public void SetupItemListVenderUI()
//    {
//        foreach (Transform item in scroll)
//        {
//            Destroy(item.gameObject);
//        }

//        int i = 0;
//        Debug.Log("vender");
//        foreach (var serial in player.playerInventory.inventoryList)
//        {
//            //GameObject newRow = Instantiate(rowPrefab, scroll);
//            RowProductComercio obj = Instantiate(rowVender, scroll);
//            obj.comercio = this;
//            obj.product = serial.itemInventory;
//            CityStatsSO.Pair pair = citystats.GetPair(obj.product);
//            obj.transform.SetParent(scroll, false);
//            obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
//            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = setPrecioVenta(pair).ToString("F2");
//            Debug.Log("Estoy en vender "+i);
//            i++;
//        }
//    }


//    public float setPrecioVenta(CityStatsSO.Pair pair)
//    {
//        float precioFinal = pair.producto.precio;
//        float percentage = 1;
//        switch (pair.existencias)
//        {
//            case ItemStats.MuyBajo:
//                percentage += 0.33f;
//                break;
//            case ItemStats.Bajo:
//                percentage += 0.16f;
//                break;
//            case ItemStats.Alto:
//                percentage -= 0.16f;
//                break;
//            case ItemStats.MuyAlto:
//                percentage -= 0.33f;
//                break;
//            default:
//                break;
//        }

//        switch (pair.demanda)
//        {
//            case ItemStats.MuyBajo:
//                percentage += 0.33f;
//                break;
//            case ItemStats.Bajo:
//                percentage += 0.16f;
//                break;
//            case ItemStats.Alto:
//                percentage -= 0.16f;
//                break;
//            case ItemStats.MuyAlto:
//                percentage -= 0.33f;
//                break;
//            default:
//                break;
//        }
//        switch (pair.produccion)
//        {
//            case ItemStats.MuyBajo:
//                percentage += 0.50f;
//                break;
//            case ItemStats.Bajo:
//                percentage += 0.25f;
//                break;
//            case ItemStats.Alto:
//                percentage -= 0.25f;
//                break;
//            case ItemStats.MuyAlto:
//                percentage -= 0.50f;
//                break;
//            default:
//                break;
//        }

//        return (float)Math.Truncate(precioFinal * percentage * 100) / 100;
//    }


//    public float setPrecioCompra(CityStatsSO.Pair pair)
//    {
//        float precio = setPrecioVenta(pair);
//        return (float)Math.Truncate(precio * 1.25f * 100) / 100;
//    }
//}
