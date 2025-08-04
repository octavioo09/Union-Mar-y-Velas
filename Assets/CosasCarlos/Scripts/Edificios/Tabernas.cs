using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public enum TabernasUI
{
    None = 0,
    Tabernas = 1,
    Comprar = 2,
    Tripulacion = 3,
    Soldados = 4
}

public class Tabernas : Building
{
    [SerializeField]
    InventoryProductSO inventario;
    //[SerializeField]
    /*ublic currencySO currencyPlayer;*/


    CityStatsSO citystats;

    ServiceSO licencia;

    public RowProductComercio rowComprar;

    public Transform scroll;


    public ComercioView posadaView; 
    public ComercioView marinerosView;
    public ComercioView soldadosView;

    public ComercioView comercioView;
    public CustomButton closeButton;
    public HooverView hooverView;
    public View modalView;
    private TabernasUI layer = TabernasUI.None;



    public void Update()
    {
        //Debug.Log(layer);
    }
    protected override void Start()
    {
        base.Start();
        string sceneName = SceneManager.GetActiveScene().name;
        string cityName = sceneName + "_Stats.asset";
        citystats = AssetDatabase.LoadAssetAtPath<CityStatsSO>("Assets/CosasCarlos/Scriptable Objects/CityStats/" + cityName);
        closeButton.onClick.AddListener(delegate { exitView(); });
        //scroll = comercioView.transform.Find("scrollContent");

        CustomButton[] posadaButtons = posadaView.GetComponentsInChildren<CustomButton>();
        posadaButtons[0].onClick.AddListener(delegate { showSoldados(); });
        posadaButtons[1].onClick.AddListener(delegate { showTripulantes(); });
        posadaButtons[2].onClick.AddListener(delegate { AccionComprar(); });

        ProductsSO soldado = AssetDatabase.LoadAssetAtPath<ProductsSO>("Assets/CosasCarlos/Scriptable Objects/Items/Products/Soldados.asset");
        CustomButton[] soldadoButtons = soldadosView.GetComponentsInChildren<CustomButton>();
        soldadoButtons[0].onClick.AddListener(delegate { ComprarUno(soldado); });
        soldadoButtons[1].onClick.AddListener(delegate { ComprarCinco(soldado); });
        soldadoButtons[2].onClick.AddListener(delegate { ComprarDiez(soldado); });

        ProductsSO marinero = AssetDatabase.LoadAssetAtPath<ProductsSO>("Assets/CosasCarlos/Scriptable Objects/Items/Products/Marineros.asset");
        CustomButton[] marineroButtons = marinerosView.GetComponentsInChildren<CustomButton>();
        marineroButtons[0].onClick.AddListener(delegate { ComprarUno(marinero); });
        marineroButtons[1].onClick.AddListener(delegate { ComprarCinco(marinero); });
        marineroButtons[2].onClick.AddListener(delegate { ComprarDiez(marinero); });

        licencia = AssetDatabase.LoadAssetAtPath<ServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Licencia_Taberna.asset");
        inventario = Instantiate(AssetDatabase.LoadAssetAtPath<InventoryProductSO>("Assets/CosasCarlos/Scriptable Objects/Inventarios/Productos_Taberna.asset"));
        modalView.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { closeModal(); });




        posadaView.gameObject.SetActive(false);
        comercioView.gameObject.SetActive(false);
        soldadosView.gameObject.SetActive(false);
        marinerosView.gameObject.SetActive(false);
        modalView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        hooverView.gameObject.SetActive(false);
        layer = TabernasUI.None;
    }

    public void showUI()
    {
        if (getEntrance() == true)
        {
            SerialService found = player.inventoryService.findItem(licencia);
            if(found != null)
            {
                if (!Building.active)
                {
                    posadaView.gameObject.SetActive(true);
                    closeButton.gameObject.SetActive(true);
                    layer = TabernasUI.Tabernas;
                    Building.active = true;
                }

            }
            else
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Necesitas la licencia para comprar en tabernas. Ve al ayuntamiento";
                showModal();
            }
        }
        else
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Debes pasar primero por la aduana";
            showModal();
        }
    }

    public void AccionComprar()
    {
        SerialService found = player.inventoryService.findItem(licencia);

        posadaView.gameObject.SetActive(false);
        comercioView.gameObject.SetActive(true);
        SetupItemListComprarUI();
        layer = TabernasUI.Comprar;
    }

    public void showTripulantes()
    {
        posadaView.gameObject.SetActive(false);
        marinerosView.gameObject.SetActive(true) ;
        layer = TabernasUI.Tripulacion;
    }


    public void showSoldados()
    {
        posadaView.gameObject.SetActive(false);
        soldadosView.gameObject.SetActive (true) ;
        layer = TabernasUI.Soldados;
    }

    public void showModal()
    {
        modalView.gameObject.SetActive(true);
    }

    public void closeModal()
    {
        modalView.gameObject.SetActive(false);
    }

   


    public void exitView()
    {

        if (layer == TabernasUI.Tabernas)
        {
            posadaView.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            layer = TabernasUI.None;
            Building.active = false;
        }

        else if (layer == TabernasUI.Comprar)
        {
            comercioView.gameObject.SetActive(false);
            posadaView.gameObject.SetActive(true);
            layer = TabernasUI.Tabernas;
        }
        else if (layer == TabernasUI.Tripulacion)
        {
            marinerosView.gameObject.SetActive(false);
            posadaView.gameObject.SetActive(true);
            layer = TabernasUI.Tripulacion;
        }
        else if (layer == TabernasUI.Soldados)
        {
            soldadosView.gameObject.SetActive(false);
            posadaView.gameObject.SetActive(true);
            layer = TabernasUI.Soldados;
        }

    }


    public void ComprarProducto(ProductsSO item)
    {

        SerialProduct found = inventario.findItem(item);

        if (found != null)
        {
            CityStatsSO.Pair pair = citystats.GetPair(item);
            float price = setPrecioCompra(pair);
            if (found.stackSize > 1 && player.playerCurrency.CurrencyQuantity >= price)
            {
                Debug.Log(price);
                player.playerInventory.Add(found.itemInventory);
                inventario.Remove(item);
                player.playerCurrency.CurrencyQuantity -= price;
            }
            else
            {
                if (found.stackSize < 1)
                {
                    modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No hay suficientes " + item.name;
                    showModal();
                }
                else if (player.playerCurrency.CurrencyQuantity < (price * 1))
                {
                    modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No tienes suficiente oro";
                    showModal();
                }
            }
        }

    }

    public void ComprarUno(ProductsSO item)
    {
        SerialProduct found = inventario.findItem(item);

        if (found != null)
        {

            CityStatsSO.Pair pair = citystats.GetPair(item);
            float price = setPrecioCompra(pair);
            if (found.stackSize > 1 && player.playerCurrency.CurrencyQuantity >= price)
            {
                player.playerInventory.Add(found.itemInventory);
                inventario.Remove(item);
                player.playerCurrency.CurrencyQuantity -= price;
            }
            else
            {
                if (found.stackSize < 1)
                {
                    modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No hay suficientes " + item.name;
                    showModal();
                }
                else if (player.playerCurrency.CurrencyQuantity < (price * 1))
                {
                    modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No tienes suficiente oro";
                    showModal();
                }
            }
        }
    }

    public void ComprarCinco(ProductsSO item)
    {
        SerialProduct found = inventario.findItem(item);

        if (found != null)
        {

            CityStatsSO.Pair pair = citystats.GetPair(item);
            float price = setPrecioCompra(pair);
            if (found.stackSize >= 5 && player.playerCurrency.CurrencyQuantity >= (price*5))
            {
                player.playerInventory.Add(found.itemInventory, 5);
                inventario.Remove(item, 5);
                player.playerCurrency.CurrencyQuantity -= (price*5);
            }
            else
            {
                if(found.stackSize < 5)
                {
                    modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No hay suficientes "+item.name;
                    showModal();
                }else if(player.playerCurrency.CurrencyQuantity < (price * 5))
                {
                    modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No tienes suficiente oro";
                    showModal();
                }
            }
        }
    }
    public void ComprarDiez(ProductsSO item)
    {
        SerialProduct found = inventario.findItem(item);

        if (found != null)
        {
            CityStatsSO.Pair pair = citystats.GetPair(item);
            float price = setPrecioVenta(pair);
            if (found.stackSize > 10 && player.playerCurrency.CurrencyQuantity >= (price * 10))
            {
                player.playerInventory.Add(found.itemInventory, 10);
                inventario.Remove(item, 10);
                player.playerCurrency.CurrencyQuantity -= (price * 10);
            }
            else
            {
                if (found.stackSize < 10)
                {
                    modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No hay suficientes " + item.name;
                    showModal();
                }
                else if (player.playerCurrency.CurrencyQuantity < (price * 10))
                {
                    modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No tienes suficiente oro";
                    showModal();
                }
            }
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
            RowProductComercio obj = Instantiate(rowComprar, scroll);
            obj.hooverView = hooverView;
            obj.product = serial.itemInventory;
            obj.taberna = this;
            CityStatsSO.Pair pair = citystats.GetPair(obj.product);
            obj.transform.SetParent(scroll, false);
            obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = setPrecioCompra(pair).ToString("F2");
            obj.GetComponentInChildren<CustomButton>().GetComponentInChildren<TextMeshProUGUI>().text = "Comprar";
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
        //float final_price = precioFinal * percentage * 100;
        //int aux = (int)final_price;
        //final_price = aux / 100;
        return (float)Math.Truncate(precioFinal * percentage * 100) / 100;
    }


    public float setPrecioCompra(CityStatsSO.Pair pair)
    {
        float precio = setPrecioVenta(pair);
        return (float)Math.Truncate(precio * 1.25f * 100) / 100;
    }
}
