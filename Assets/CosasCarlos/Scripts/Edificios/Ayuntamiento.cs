using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static Aduana;

public class Ayuntamiento : Building
{
    public enum AyuntamientoUI
    {
        None = 0,
        Active = 1,
        Arrendar = 2,
        Licencia = 3
    }

    [SerializeField]
    //CitySO city;
    //public PlayerSO player;

    public ComercioView licenciasView;
    public ComercioView ayuntamientoView;
    public ComercioView arrendarView;
    public CustomButton closeButton;
    public View modalView;
    //public InputRow input;

    //private CustomButton button;
    //public ServiceSO licencia_tiendas;
    //public ServiceSO licencia_tabernas;

    public bool arrendado;

    private AyuntamientoUI layer;
    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();
        closeButton.onClick.AddListener(delegate { exitView(); });

        CustomButton[] aytoButtons = ayuntamientoView.GetComponentsInChildren<CustomButton>();
        aytoButtons[0].onClick.AddListener(delegate { showLicencias(); });
        aytoButtons[1].onClick.AddListener(delegate { showArrendar(); });

        CustomButton[] licenciaButtons = licenciasView.GetComponentsInChildren<CustomButton>();
        ServiceSO licencia = AssetDatabase.LoadAssetAtPath<ServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Licencia_Tienda.asset");
        licenciaButtons[0].onClick.AddListener(delegate { comprarLicencia(licencia); });
        ServiceSO licencia2 =AssetDatabase.LoadAssetAtPath<ServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Licencia_Taberna.asset");
        licenciaButtons[1].onClick.AddListener(delegate { comprarLicencia(licencia2); });

        ServiceSO impuestos = AssetDatabase.LoadAssetAtPath<ServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Impuestos.asset");
        CustomButton arrendarButton = arrendarView.GetComponentInChildren<CustomButton>();
        arrendarButton.onClick.AddListener(delegate { ArrendarImpuestos(impuestos); });

        modalView.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { closeModal(); });

        //button.onClick.AddListener(delegate { showUI(); });


        if (city.impuestos_arrendados > 0)
        {
            arrendado = true;
        }
        licenciasView.gameObject.SetActive(false);
        ayuntamientoView.gameObject.SetActive(false);
        arrendarView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        modalView.gameObject.SetActive(false);
        layer = AyuntamientoUI.None;  
    }

    public void showUI()
    {
        if (getEntrance() == true)

        {
            if (!active)
            {
                licenciasView.gameObject.SetActive(false);
                ayuntamientoView.gameObject.SetActive(true);
                arrendarView.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(true);
                layer = AyuntamientoUI.Active;
                active = true;
            }
        }
        else
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Debes pasar primero por la aduana";
            showModal();
        }
    }

    public void showLicencias()
    {
        licenciasView.gameObject.SetActive(true);
        ayuntamientoView.gameObject.SetActive(false);
        arrendarView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        layer = AyuntamientoUI.Licencia;
    }

    public void showArrendar()
    {
        licenciasView.gameObject.SetActive(false);
        ayuntamientoView.gameObject.SetActive(false);
        arrendarView.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        layer = AyuntamientoUI.Arrendar;
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


        if (layer == AyuntamientoUI.Active)
        {
            ayuntamientoView.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            layer = AyuntamientoUI.None;
            active = false;
        }

        else if (layer == AyuntamientoUI.Licencia)
        {
            licenciasView.gameObject.SetActive(false);
            ayuntamientoView.gameObject.SetActive(true);
            layer = AyuntamientoUI.Active;
        }

        else if(layer == AyuntamientoUI.Arrendar)
        {
            arrendarView.gameObject.SetActive(false);
            ayuntamientoView.gameObject.SetActive(true);
            layer = AyuntamientoUI.Active;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void comprarLicencia(ServiceSO service)
    {
        SerialService found = player.inventoryService.findItem(service);
        Debug.Log("hola");
        if (found == null)
        {
            Debug.Log("hola2");

            if (player.playerCurrency.CurrencyQuantity > service.precio)
            {
                player.playerCurrency.CurrencyQuantity -= service.precio;
               
                service.city = city.cityName;
                player.inventoryService.Add(service);
            }
        }
        else if (found != null && found.hasService == true)
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡Ya tienes esta licencia!";
            showModal();
        }
    }


    public void ArrendarImpuestos(ServiceSO service)
    {
        SerialService found = city.services.findItem(service);

        if (found != null && found.hasService == false)
        {
            if (player.playerCurrency.CurrencyQuantity > found.precio)
            {
                player.playerCurrency.CurrencyQuantity -= found.precio;
                found.hasService = true;
                service.city = city.cityName;
                player.inventoryService.Add(service);
            }
        }
        else if (found != null && found.hasService == true)
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡Ya has arrendado impuestos!";
            showModal();
        }
    }

    //public void ArrendarImpuestos() {

    //    input.getText();
    //    if (arrendado)
    //    {
    //        modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡Ya has arrendado impuestos!";
    //        showModal();
    //    }
    //    else if(input.text != "")
    //    {
    //        addImpuestos();
    //    }
    //}

    //public void addImpuestos()
    //{
        
    //    //string s = transform.Find("CustomInput").GetComponent<TMP_InputField>().text.ToString();
    //    if(double.TryParse(input.text, out var quantity) && player.playerCurrency.CurrencyQuantity > quantity)
    //    {
    //        arrendado = true;
    //        city.impuestos_arrendados = quantity;
    //        city.time_impuestos = 1;
    //        player.playerCurrency.CurrencyQuantity -= (float)quantity;
    //        modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Has arrendado " + quantity + " reales";
    //        showModal();
    //    }
    //    else {
    //        modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No es válido";
    //        showModal();
    //    }
    //}
}

