using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puerto : Building
{
    public enum PuertoUI
    {
        None = 0,
        Active = 1,
        Inventario = 2
    }

    public ComercioView puertoView;
    public ComercioView inventarioView;
    public Transform scroll;
    public CustomButton closeButton;
    public RowBarcosPuerto fila;
    public RowProducts rowPr;
    public RowServicios rowSe;
    public View modalView;
    GameObject barcoPuerto;


    public HooverView hooverView;

    public PuertoUI layer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        closeButton.onClick.AddListener(delegate { exitView(); });
        CustomButton[] buttons = puertoView.GetComponentsInChildren<CustomButton>();
        buttons[0].onClick.AddListener(delegate { verInventario(); });
        buttons[1].onClick.AddListener(delegate { verServicios(); });
        buttons[2].onClick.AddListener(delegate { verFlota(); });
        buttons[3].onClick.AddListener(delegate { verAlmacenes(); });
        buttons[4].onClick.AddListener(delegate { salirAMar(); });
        barcoPuerto = GameObject.FindGameObjectWithTag("PlayerShip");

        if (player.barcoActivo)
        {
            GameObject barquito = Instantiate(player.barcoActivo.carcasa, barcoPuerto.transform.position, Quaternion.identity, barcoPuerto.transform);
            barquito.transform.localScale = new Vector3(3.14f, 3.14f, 3.14f);
        }

        modalView.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { closeModal(); });
        //scroll = inventarioView.transform.Find("scrollContent");

        puertoView.gameObject.SetActive(false);
        inventarioView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        hooverView.gameObject.SetActive(false);
        modalView.gameObject.SetActive(false);
        layer = PuertoUI.None;
    }


    public void showUI()
    {
        //Debug.Log(getEntrance());
        //if (getEntrance() == true)
        //{
            if (!active)
            {
                puertoView.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(true);
                layer = PuertoUI.Active;
                active = true;
            }
        //}
        //else {
        //    modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Necesitas pagar los impuestos de entrada";
        //    showModal();
        //}
    }


    public void verInventario()
    {
        Debug.Log("Inventario");
        puertoView.gameObject.SetActive(false);
        inventarioView.gameObject.SetActive(true);
        SetupItemListInventory();
        layer = PuertoUI.Inventario;
    }


    public void verServicios()
    {
        Debug.Log("Servicios");
        puertoView.gameObject.SetActive(false);
        inventarioView.gameObject.SetActive(true);
        SetupItemListServices();
        layer = PuertoUI.Inventario;
    }

    public void verFlota()
    {
        Debug.Log("Flota");
        puertoView.gameObject.SetActive(false);
        inventarioView.gameObject.SetActive(true);
        SetupItemListBarcos();
        layer = PuertoUI.Inventario;
    }

    public void verAlmacenes()
    {
        Debug.Log("Almacenes");
        puertoView.gameObject.SetActive(false);
        inventarioView.gameObject.SetActive(true);
        SetupItemListAlmacenes();
        layer = PuertoUI.Inventario;
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
        if(layer == PuertoUI.Inventario)
        {
            inventarioView.gameObject.SetActive(false);
            puertoView.gameObject.SetActive(true);
            layer = PuertoUI.Active;
            //return;

        }else if( layer == PuertoUI.Active)
        {
            puertoView.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            layer = PuertoUI.None;
            active = false;
            //return;
        }
    }

    public void salirAMar()
    { 
        Debug.Log("Saliendo a la mar");
        puertoView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        layer = PuertoUI.None;
        active = false;
        SceneManager.LoadScene("OutdoorsScene");
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void SetupItemListInventory()
    {

        foreach (Transform item in scroll)
        {
            Destroy(item.gameObject);
        }

        foreach (var serial in player.playerInventory.inventoryList)
        {
            RowProducts obj = Instantiate(rowPr, scroll);
            obj.hooverView = hooverView;
            obj.product = serial.itemInventory;
            obj.transform.SetParent(scroll, false);
            obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.stackSize.ToString();

            if (obj.hooverView.gameObject.activeSelf)
            {
                obj.transform.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { obj.exitHoover(); });
            }
            else
            {
                obj.transform.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { obj.showHoover(); });
            }
        }
    }


    public void SetupItemListServices()
    {

        foreach (Transform item in scroll)
        {
            Destroy(item.gameObject);
        }

        foreach (var serial in player.inventoryService.inventoryList)
        {
            RowServicios obj = Instantiate(rowSe, scroll);
            obj.hooverView = hooverView;
            //obj.product = serial.itemInventory;
            obj.transform.SetParent(scroll, false);
            obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Validez: "+serial.itemInventory.time.ToString();
            if (obj.hooverView.gameObject.activeSelf)
            {
                obj.transform.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { rowSe.exitHoover(); });

            }
            else
            {

                obj.transform.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { rowSe.showHoover(); });
            }
        }
    }

    public void SetupItemListBarcos()
    {

        foreach (Transform item in scroll)
        {
            Destroy(item.gameObject);
        }
        if (player.flota != null && player.flota.inventoryList != null && player.flota.inventoryList.Count >0)
        {
            foreach (var serial in player.flota.inventoryList)
            {
                RowBarcosPuerto obj = Instantiate(fila, scroll);
                obj.hooverView = hooverView;
                obj.puerto = this;
                obj.barco = serial.itemInventory;
                obj.transform.SetParent(scroll, false);
                obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
                CustomButton[] buttons = obj.transform.GetComponentsInChildren<CustomButton>();
                //obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.existencias.ToString();

                if (obj.hooverView.gameObject.activeSelf)
                {
                    buttons[0].onClick.AddListener(delegate { obj.exitHoover(); });
                }
                else
                {
                    buttons[0].onClick.AddListener(delegate { obj.showHoover(); });
                }
                buttons[1].onClick.AddListener(delegate { obj.AccionHacerPrincipal(); });
            }
        }
    }

    public void HacerPrincipal(Barcos barcos) { 
        if(barcos != null && barcos.activo == false)
        {
            if(player.barcoActivo == true)
            {
                //player.barcoActivo.inventario = player.playerInventory;
                player.barcoActivo.activo=false;
                Destroy(barcoPuerto.transform.GetChild(0).gameObject);

            }
            barcos.activo = true;
            player.barcoActivo = barcos;
            //player.playerInventory = barcos.inventario;
            GameObject barquito = Instantiate(player.barcoActivo.carcasa, barcoPuerto.transform.position, Quaternion.identity, barcoPuerto.transform);
            barquito.transform.localScale = new Vector3(3.14f, 3.14f, 3.14f);
            //barcoPuerto = player.barcoActivo.carcasa;
        }

    }


    public void SetupItemListAlmacenes()
    {

        foreach (Transform item in scroll)
        {
            Destroy(item.gameObject);
        }

        foreach (var serial in player.inventoryService.inventoryList)
        {
            if (serial.itemInventory.ItemName.StartsWith("Almacen") && serial.hasService){
                RowServicios obj = Instantiate(rowSe, scroll);
                obj.hooverView = hooverView;
                AlmacenSO almacen = (AlmacenSO)serial.itemInventory;
                //obj.product = serial.itemInventory;
                obj.transform.SetParent(scroll, false);
                obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = almacen.ItemName;
                obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Disponible: " + ((int)almacen.occupied).ToString() + "/" + ((int)almacen.capacity).ToString();
                if (obj.hooverView.gameObject.activeSelf)
                {
                    obj.transform.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { rowSe.exitHoover(); });

                }
                else
                {

                    obj.transform.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { rowSe.showHoover(); });
                }
            }
        }
    }


}
