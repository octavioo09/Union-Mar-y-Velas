using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.GraphView.GraphView;



public class Almacen : Building
{

    //Data
    [SerializeField]
    //CitySO city;
    //public PlayerSO player;
    public bool inProperty = false;
    public AlmacenSO almacen;
    InventoryProductSO inventory;
    double capacity = 3840;
    double occupied;

    // UI 
    public ComercioView almacenView;
    public View modalView;
    public CustomButton closeButton;

    //UI methods
    protected override void Start()
    {

        base.Start();
        CustomButton[] buttons = almacenView.GetComponentsInChildren<CustomButton>();
        buttons[0].onClick.AddListener(delegate { Comprar(almacen); });
        buttons[1].onClick.AddListener(delegate { Alquilar(almacen); });
        buttons[2].onClick.AddListener(delegate { Construir(almacen); });
        buttons[3].onClick.AddListener(delegate { Vender(almacen); });
        //buttons[4].onClick.AddListener(delegate { verAlmacen(almacen); });



        closeButton.onClick.AddListener(delegate { exitView(); });

        almacenView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        modalView.gameObject.SetActive(false);

    }

    public void showUI()
    {
        if (getEntrance() == true)
        {
            if (!active)
            {
                almacenView.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(true);
                modalView.gameObject.SetActive(false);
                active = true;
            }
        }
        else
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Debes pasar primero por la aduana";
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

    public void exitView()
    {
        //Debug.Log(layer);

        almacenView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        modalView.gameObject.SetActive(false);
        active = false;
    }





    //Transaction methods
    public void Comprar(ServiceSO service)
    {
        SerialService found = city.services.findItem(service);

        if (found != null && found.hasService && !inProperty)
        {
            if(player.playerCurrency.CurrencyQuantity > almacen.precio)
            {
                player.inventoryService.Add(almacen);
                city.services.Add(almacen);
                player.playerCurrency.CurrencyQuantity -= almacen.precio;
                inProperty = true;
                string path = "Assets/Scriptable Objects/" + city.cityName + "/Almacen"+city.AlmacenCount;
                inventory = Instantiate(inventory);
                AssetDatabase.CreateAsset(inventory, path);
                city.AlmacenCount++;
            }
        }else if(found == null || (found != null && !found.hasService)){
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡No tienes la licencia para comerciar!";
            showModal();
        }
        else if(inProperty){
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡Ya tienes este almacen!";
            showModal();
        }   
    }

    public void Vender(ServiceSO service)
    {
        SerialService found = city.services.findItem(almacen);
        SerialService license = city.services.findItem(service);


        if (found != null && license != null  && inProperty && license.hasService)
        {
            if (player.playerCurrency.CurrencyQuantity > found.precio)
            {
                player.inventoryService.Add(found.itemInventory);
                city.services.Add(found.itemInventory);
                player.playerCurrency.CurrencyQuantity -= found.precio;
                inProperty = true;
            }
        }
    }
    public void Alquilar(ServiceSO service)
    {
        SerialService found = city.services.findItem(service);

        if (found != null && found.hasService && !inProperty)
        {
            if (player.playerCurrency.CurrencyQuantity > (almacen.precio * 0.3f))
            {
                player.inventoryService.Add(almacen);
                city.services.Add(almacen);
                player.playerCurrency.CurrencyQuantity -= (almacen.precio*0.3f);
                inProperty = true;
                almacen.time = 1;
                string path = "Assets/Scriptable Objects/" + city.cityName + "/Almacen" + city.AlmacenCount;
                inventory = Instantiate(inventory);
                AssetDatabase.CreateAsset(inventory, path);
                city.AlmacenCount++;
            }
        }
        else if (found == null || (found != null && !found.hasService))
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡No tienes la licencia para comerciar!";
            showModal();
        }
        else if (inProperty)
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡Ya tienes este almacen!";
            showModal();
        }
    }

    public void Construir(ServiceSO service)
    {
        SerialService found = city.services.findItem(service);

        if (found != null && found.hasService && !inProperty)
        {
            if (player.playerCurrency.CurrencyQuantity > (almacen.precio * 0.75f))
            {
                player.inventoryService.Add(almacen);
                city.services.Add(almacen);
                player.playerCurrency.CurrencyQuantity -= (almacen.precio * 0.75f);
                inProperty = true;
                almacen.time = 0.5; // ver como gestionar la construccion
                almacen.available = false;
                string path = "Assets/Scriptable Objects/" + city.cityName + "/Almacen" + city.AlmacenCount;
                inventory = Instantiate(inventory);
                AssetDatabase.CreateAsset(inventory, path);
                city.AlmacenCount++;
            }
        }
        else if (found == null || (found != null && !found.hasService))
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡No tienes la licencia para comerciar!";
            showModal();
        }
        else if (inProperty)
        {
            if (!almacen.available) {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡Este almacen esta ocupado por un padre y un hijo!";
                showModal();
            }
            else
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡Ya tienes este almacen!";
                showModal();
            }
        }
    }
}
