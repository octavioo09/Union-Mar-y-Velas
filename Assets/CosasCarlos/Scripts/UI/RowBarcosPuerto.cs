using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RowBarcosPuerto : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [HideInInspector]
    public Barcos barco;
    CustomButton button1;
    CustomButton button2;
    [HideInInspector]
    public HooverView hooverView;
    [HideInInspector]
    public Puerto puerto;
    private void Start()
    {
        CustomButton[] buttons = GetComponentsInChildren<CustomButton>();
        button1 = buttons[0];
        button1.GetComponentInChildren<TextMeshProUGUI>().text = "Ver más";
        button2 = buttons[1];
        button2.GetComponentInChildren<TextMeshProUGUI>().text = "Hacer barco principal";

        if (hooverView != null)
        {
            hooverView.gameObject.SetActive(false);
        }

    }

    public void AccionHacerPrincipal()
    {
        puerto.HacerPrincipal(barco);
    }

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
        texts[0].text = "Descripción: " + barco.descripcion.ToString();

        texts[1].text = "Capacidad: " + barco.capacity + " barriles";

        texts[2].text = "Vida máxima: " + barco.vidaTotal;
        hooverView.transform.position = new Vector3(button1.transform.position.x + 350, button1.transform.position.y + 200, hooverView.transform.position.z);
        hooverView.gameObject.SetActive(true);
    }

    public void exitHoover()
    {
        hooverView.gameObject.SetActive(false);
    }
}
