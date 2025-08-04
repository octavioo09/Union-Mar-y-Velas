using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

public class Consulado : Building
{
    [SerializeField]
    //CitySO city;
    /*ublic currencySO currencyPlayer;*/

    //public PlayerSO player;

    //public CityStatsSO citystats;

    public ComercioView consuladoView;
    public CustomButton closeButton;
    public View modalView;



    public void Update()
    {
        //Debug.Log(layer);
    }
    protected override void Start()
    {
        base.Start();
        closeButton.onClick.AddListener(delegate { exitView(); });

        CustomButton[] buttons = consuladoView.GetComponentsInChildren<CustomButton>();
        buttons[0].onClick.AddListener(delegate { ComprarMapa(); });
        buttons[1].onClick.AddListener(delegate { ComprarChivatazo(); });

        modalView.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { closeModal(); });
        consuladoView.gameObject.SetActive(false);
        modalView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }

    public void showUI()
    {
        if (getEntrance() == true)
        {
            if (!active)
            {
                consuladoView.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(true);
                active = true;
            }
        }
        else
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Debes pasar primero por la aduana";
            showModal();
        }
    }



    public void exitView()
    {
        consuladoView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        active = false;
    }

    public void showModal()
    {
        modalView.gameObject.SetActive(true);
    }

    public void closeModal()
    {
        modalView.gameObject.SetActive(false);
    }

    public void ComprarChivatazo() { }

    public void ComprarMapa() { }
}
