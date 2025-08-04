using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Aduana : Building
{


    [SerializeField]
    //CitySO city;
    //public PlayerSO player = AssetDatabase.get;
    private float impuestoEntrada;
    private float impuestoSalida;

    // UI 
    public ComercioView aduanaView;
    //public View modalView;
    public CustomButton closeButton;
    public View modalView;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        closeButton.onClick.AddListener(delegate { exitView(); });

        CustomButton[] buttons = aduanaView.GetComponentsInChildren<CustomButton>();
        buttons[0].onClick.AddListener(delegate { PagarEntrada(); });
        buttons[1].onClick.AddListener(delegate { PagarSalida(); });
        buttons[2].onClick.AddListener(delegate { PagarTramitacion(); });

        modalView.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { closeModal(); });
        impuestoEntrada = 0;
        impuestoSalida = 0;
        aduanaView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        modalView.gameObject.SetActive(false);

    }

    public void showUI()
    {
        if (!active)
        {
            aduanaView.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(true);
            active = true;
        }
        //modalView.gameObject.SetActive(false);
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
        
        aduanaView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        //modalView.gameObject.SetActive(false);
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PagarEntrada()
    {
        if (!city.entrancePaid)
        {
            Debug.Log(player.playerInventory);
            Debug.Log(player.playerInventory.inventoryList);
            if (player.playerInventory != null && player.playerInventory.inventoryList != null && player.playerInventory.inventoryList.Count > 0)
            {
                foreach (var product in player.playerInventory.inventoryList)
                {

                    impuestoEntrada += (product.precio * 0.1f) * product.stackSize;

                }
                if (player.playerCurrency.CurrencyQuantity > impuestoEntrada)
                {
                    player.playerCurrency.CurrencyQuantity -= impuestoEntrada;
                    city.entrancePaid = true;
                    city.exitPaid = false;
                    modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Has pagado los impuestos de entrada: " + impuestoEntrada;
                    showModal();
                }
                else
                {
                    modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No tienes dinero suficiente, necesitas " + impuestoEntrada + " reales";
                    showModal();
                }
            }
            else
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No tienes ninguna mercancía, puedes entrar";
                city.entrancePaid = true;
                showModal();
            }

            
            


        }
        else
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ya has pagado los impuestos de entrada.";
            showModal();
        }
        //Hace falta el inventario del barco
        //Si ha pagado city.entrancePaid = true;
    }

    public void PagarSalida()
    {
        if (city.entrancePaid && !city.exitPaid)
        {
            foreach (var barcos in player.flota.inventoryList)
            {
                foreach (var p in barcos.itemInventory.inventario.inventoryList)
                {

                    impuestoSalida += (p.precio * 0.1f) * p.stackSize;
                }
            }
            if (player.playerCurrency.CurrencyQuantity > impuestoSalida)
            {
                player.playerCurrency.CurrencyQuantity -= impuestoSalida;
                city.exitPaid = true;
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Has pagado los impuestos de salida: " + impuestoSalida;
                showModal();
            }
            else
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No tienes dinero suficiente, necesitas " + impuestoSalida + " reales";
                showModal();
            }

        }
        else if(city.entrancePaid && city.exitPaid)
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ya has pagado los impuestos de salida.";
            showModal();
        }else if (!city.entrancePaid)
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No puedes pagar los impuestos de salida, primero has de pagar los de entrada.";
            showModal();
        }

    
    }

    public void PagarTramitacion()
    {
        //Hace falta inventario del barco
    }
}
