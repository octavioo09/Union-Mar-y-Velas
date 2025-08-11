using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RowProductComercio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public ProductsSO product;
    [HideInInspector]
    public Barcos barco;
    [SerializeField]
    CustomButton button;
    [HideInInspector]
    public Mercado comercio;
    [HideInInspector]
    public Tabernas taberna;
    [HideInInspector]
    public HooverView hooverView;
    [HideInInspector]
    //public Barcos barco;
    private void Start()
    {
        if (hooverView != null)
        {
            hooverView.gameObject.SetActive(false);
        }

    }

    public void Comprar()
    {
        comercio.Comprar(product);
    }

    public void Vender()
    {
        comercio.Vender(product);
    }

    public void ComprarTaberna()
    {
        taberna.ComprarProducto(product);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.gameObject.activeSelf) {
            showHoover();
        }
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        exitHoover();
    }

    public void showHoover()
    {
        TextMeshProUGUI[] texts = hooverView.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = "Descripci�n: " + product.ItemDescription;
        if (product.soporte == ItemContainerEnum.Barril)
        {
            texts[1].text = "Volumen: Ocupa 1 barril";
        }
        else if (product.soporte == ItemContainerEnum.Espacio)
        {
            texts[1].text = "Volumen: Ocupa 48 barriles";
        }
        texts[2].text = "Cantidad: Cada barril contiene " + product.cantidad;
        hooverView.transform.position = new Vector3(button.transform.position.x + 350, button.transform.position.y + 200, hooverView.transform.position.z);
        hooverView.gameObject.SetActive(true);
    }

    public void exitHoover()
    {
        hooverView.gameObject.SetActive(false);
    }
}
