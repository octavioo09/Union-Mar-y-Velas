using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RowProducts : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public ProductsSO product;
    CustomButton button;
    [HideInInspector]
    public HooverView hooverView;
    [HideInInspector]
    public Puerto puerto;
    private void Start()
    {
        button = GetComponentInChildren<CustomButton>();
        if (hooverView != null)
        {
            hooverView.gameObject.SetActive(false);
        }

    }

    //public void Comprar()
    //{
    //    atarazanas.ComprarBarco(barco);
    //}

    //public void Alquilar()
    //{
    //    atarazanas.AlquilarBarco(barco);
    //}
    //public void Reparar()
    //{
    //    atarazanas.RepararBarco(barco);
    //}
    //public void Vender()
    //{
    //    atarazanas.VenderBarco(barco);
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        showHoover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        exitHoover();
    }

    public void showHoover()
    {
         TextMeshProUGUI[] texts = hooverView.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = "Descripción: " + product.ItemDescription;
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
