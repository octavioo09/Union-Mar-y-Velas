using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MercadoUI
{
    None    = 0,
    Mercado = 1,
    Comprar = 2,
    Vender  = 3
}
//[Serializable]
public class Mercado : Building
{

    [SerializeField]
   
    public InventoryProductSO inventario;


    public CityStatsSO citystats;

    public RowProductComercio rowComprar;
    public RowProductComercio rowVender;

    public Transform scroll;
    ServiceSO licencia;

    public ComercioView mercadoView;
    public ComercioView comercioView;
    public CustomButton closeButton;
    public View modalView;
    public HooverView hooverView;
    private MercadoUI layer = MercadoUI.None;



    public void Update()
    {
    }
    protected override void Start()
    {
        base.Start();
        string sceneName = SceneManager.GetActiveScene().name;
        string cityName = sceneName + "_Stats.asset";
        citystats = AssetDatabase.LoadAssetAtPath<CityStatsSO>("Assets/CosasCarlos/Scriptable Objects/CityStats/" + cityName);
        closeButton.onClick.AddListener(delegate { ExitView(); });


        CustomButton[] buttons = mercadoView.GetComponentsInChildren<CustomButton>();
        buttons[0].onClick.AddListener(delegate { AccionComprar(); });
        buttons[1].onClick.AddListener(delegate { AccionVender(); });


        modalView.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { closeModal(); });
        mercadoView.gameObject.SetActive(false);
        comercioView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        hooverView.gameObject.SetActive(false);
        modalView.gameObject.SetActive(false);  


        licencia = AssetDatabase.LoadAssetAtPath<ServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Licencia_Venta.asset");
        inventario = Instantiate(AssetDatabase.LoadAssetAtPath<InventoryProductSO>("Assets/CosasCarlos/Scriptable Objects/Inventarios/Productos_Mercado.asset"));
        layer = MercadoUI.None;
    }

    public void showUI()
    {
        if (getEntrance() == true)
        {
            SerialService found = player.inventoryService.findItem(licencia);
            if (found != null)
            {
                if (!active)
                {
                    mercadoView.gameObject.SetActive(true);
                    closeButton.gameObject.SetActive(true);
                    layer = MercadoUI.Mercado;

                    active = true;
                }
            }
            else
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Necesitas la licencia para comprar en el mercado. Ve al ayuntamiento";
                showModal();
            
            }
        }
        else
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Debes pasar primero por la aduana";
            showModal();
        }
    }
    public void AccionComprar() { 
        mercadoView.gameObject.SetActive(false);
        comercioView.gameObject.SetActive(true);
        SetupItemListComprarUI();
        layer = MercadoUI.Comprar;
    }

    public void AccionVender() {

        SerialService found = player.inventoryService.findItem(licencia);
        if (found != null)
        {  
            mercadoView.gameObject.SetActive(false);
            comercioView.gameObject.SetActive(true);
            SetupItemListVenderUI();
            layer = MercadoUI.Vender;
        }
        else
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Necesitas la licencia para poder vender tu mercancía. Ve al palacio del gobernador";
            showModal();
        }
    }

    public void showModal()
    {
        modalView.gameObject.SetActive(true);
    }

    public void closeModal()
    {
        modalView.gameObject.SetActive(false);
    }


    public void ExitView() {
        //Debug.Log(layer);


        if (layer == MercadoUI.Mercado) { 
            mercadoView.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            layer = MercadoUI.None;
            active = false;
        }
        
        else if(layer == MercadoUI.Comprar || layer == MercadoUI.Vender){
            comercioView.gameObject.SetActive(false);
            mercadoView.gameObject.SetActive(true);
            layer = MercadoUI.Mercado;
            
        }
       
    }
    public void Comprar(ProductsSO item)
    {

        SerialProduct found = inventario.findItem(item);

        if (found != null)
        {
            CityStatsSO.Pair pair = citystats.GetPair(item);
            float price = setPrecioCompra(pair);
            if (found.stackSize > 1 && player.playerCurrency.CurrencyQuantity >= price)
            {
                player.playerInventory.Add(item);
                inventario.Remove(item);
                player.playerCurrency.CurrencyQuantity -= price;
                Debug.Log("Comprando" + found.precio);
            }
            //else
        }
        //return evento;
    }

    public void Vender(ProductsSO item)
    {

        SerialProduct found = player.playerInventory.findItem(item);

        if (found != null)
        {
            CityStatsSO.Pair pair = citystats.GetPair(item);
            float price = setPrecioVenta(pair);
            if (found.stackSize > 0)
            {
                inventario.Add(item);
                player.playerInventory.Remove(item);
                player.playerCurrency.CurrencyQuantity += price;
                Debug.Log("Vendiendo" + found.precio);

            }
            //else
        }
    }

    public void SetupItemListComprarUI()
    {

        foreach (Transform item in scroll)
        {
            Destroy(item.gameObject);
        }

        foreach (var serial in inventario.inventoryList)
        {
            //GameObject newRow = Instantiate(rowPrefab, scroll);
            RowProductComercio obj = Instantiate(rowComprar, scroll);
            obj.hooverView = hooverView;
            obj.comercio = this;
            obj.product = serial.itemInventory;
            CityStatsSO.Pair pair = citystats.GetPair(obj.product);
            obj.transform.SetParent(scroll, false);
            obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = setPrecioCompra(pair).ToString("F2");
        }
    }

    public void SetupItemListVenderUI()
    {
        foreach (Transform item in scroll)
        {
            Destroy(item.gameObject);
        }

        int i = 0;
        Debug.Log("vender");
        foreach (var serial in player.playerInventory.inventoryList)
        {
            //GameObject newRow = Instantiate(rowPrefab, scroll);
            RowProductComercio obj = Instantiate(rowVender, scroll);
            obj.hooverView = hooverView;
            obj.comercio = this;
            obj.product = serial.itemInventory;
            CityStatsSO.Pair pair = citystats.GetPair(obj.product);
            obj.transform.SetParent(scroll, false);
            obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = setPrecioVenta(pair).ToString("F2");
            Debug.Log("Estoy en vender " + i);
            i++;
        }
    }


    public float setPrecioVenta(CityStatsSO.Pair pair)
    {
        float precioFinal = pair.producto.precio;
        float percentage = 1;
        switch (pair.existencias)
        {
            case ItemStats.MuyBajo:
                percentage += 0.33f;
                break;
            case ItemStats.Bajo:
                percentage += 0.16f;
                break;
            case ItemStats.Alto:
                percentage -= 0.16f;
                break;
            case ItemStats.MuyAlto:
                percentage -= 0.33f;
                break;
            default:
                break;
        }

        switch (pair.demanda)
        {
            case ItemStats.MuyBajo:
                percentage += 0.33f;
                break;
            case ItemStats.Bajo:
                percentage += 0.16f;
                break;
            case ItemStats.Alto:
                percentage -= 0.16f;
                break;
            case ItemStats.MuyAlto:
                percentage -= 0.33f;
                break;
            default:
                break;
        }
        switch (pair.produccion)
        {
            case ItemStats.MuyBajo:
                percentage += 0.50f;
                break;
            case ItemStats.Bajo:
                percentage += 0.25f;
                break;
            case ItemStats.Alto:
                percentage -= 0.25f;
                break;
            case ItemStats.MuyAlto:
                percentage -= 0.50f;
                break;
            default:
                break;
        }

        return (float)Math.Truncate(precioFinal * percentage * 100) / 100;
    }


    public float setPrecioCompra(CityStatsSO.Pair pair)
    {
        float precio = setPrecioVenta(pair);
        return (float)Math.Truncate(precio * 1.25f * 100) / 100;
    }

}


