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
public class Mercado : Comercio
{
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

}


