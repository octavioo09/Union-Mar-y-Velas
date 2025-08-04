using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Atarazanas : Building
{

    public enum AtarazanasUI
    {
        None = 0,
        Active = 1,
        List = 2
    }

    ServiceSO licencia;
    public ComercioView atarazanasView;
    public ComercioView listView;
    public CustomButton closeButton;
    public View modalView;
    public HooverView hooverView;
    GameObject barcoPuerto;


    [HideInInspector]
    public InventoryBarcos listaBarcos;

    public RowBarcos rowComprar;
    public Transform scroll;

    AtarazanasUI layer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CustomButton[] buttons = atarazanasView.GetComponentsInChildren<CustomButton>();
        buttons[0].onClick.AddListener(delegate { comprarView(); });
        buttons[1].onClick.AddListener(delegate { alquilarView(); });
        buttons[2].onClick.AddListener(delegate { venderView(); });
        buttons[3].onClick.AddListener(delegate { repararView(); });

        listaBarcos = (InventoryBarcos)ScriptableObject.CreateInstance(className: "InventoryBarcos");
        listaBarcos.InitList();
        closeButton.onClick.AddListener(delegate { exitView(); });
        barcoPuerto = GameObject.FindGameObjectWithTag("PlayerShip");


        licencia = AssetDatabase.LoadAssetAtPath<ServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Licencia_Comercio.asset");
        modalView.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { closeModal(); });


        atarazanasView.gameObject.SetActive(false);
        listView.gameObject.SetActive(false);
        modalView.gameObject.SetActive(false);
        hooverView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        layer = 0;
        
    }

    public void showUI()
    {
        if (getEntrance() == true)
        {
            SerialService found = player.inventoryService.findItem(licencia);
            if(found != null)
            {
                if (!active)
                {
                    atarazanasView.gameObject.SetActive(true);
                    closeButton.gameObject.SetActive(true);
                    layer = AtarazanasUI.Active;
                    active = true;
                }

            }
            else
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Necesitas la licencia para comerciar con barcos. Ve al palacio del gobernador";
                showModal();
            }
        }
        else
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Debes pasar primero por la aduana";
            showModal();
        }

    }

    public void comprarView()
    {
        atarazanasView.gameObject.SetActive(false);
        listView.gameObject.SetActive(true);
        layer = AtarazanasUI.List;
        SetupListComprar();
    }

    public void alquilarView()
    {
        atarazanasView.gameObject.SetActive(false);
        listView.gameObject.SetActive(true);
        layer = AtarazanasUI.List;
        SetupListAlquilar();

    }

    public void venderView()
    {
        atarazanasView.gameObject.SetActive(false);
        listView.gameObject.SetActive(true);
        layer = AtarazanasUI.List;
        SetupListVender();
    }

    public void repararView()
    {
        atarazanasView.gameObject.SetActive(false);
        listView.gameObject.SetActive(true);
        layer = AtarazanasUI.List;
        SetupListReparar();
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
        if(layer == AtarazanasUI.List)
        {
            listView.gameObject.SetActive(false);
            atarazanasView.gameObject.SetActive(true);
            layer = AtarazanasUI.Active;

        }else if(layer == AtarazanasUI.Active)
        {
            atarazanasView.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            layer = AtarazanasUI.None;
            active = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupListComprar()
    {
        foreach (Transform item in scroll)
        {
            Destroy(item.gameObject);
        }

        foreach (var serial in listaBarcos.inventoryList)
        {
            RowBarcos obj = Instantiate(rowComprar, scroll);
            obj.hooverView = hooverView;
            obj.atarazanas = this;
            obj.barco = serial.itemInventory;
            obj.transform.SetParent(scroll, false);
            obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.precio.ToString();
            obj.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { obj.Comprar(); });
            obj.GetComponentInChildren<CustomButton>().GetComponentInChildren<TextMeshProUGUI>().text = "Comprar";
        }
    }

    public void SetupListAlquilar()
    {
        foreach (Transform item in scroll)
        {
            Destroy(item.gameObject);
        }

        foreach (var serial in listaBarcos.inventoryList)
        {
            RowBarcos obj = Instantiate(rowComprar, scroll);
            obj.hooverView = hooverView;
            obj.barco = serial.itemInventory;
            obj.atarazanas = this;

            float precio = serial.itemInventory.precio * 0.25f;
            obj.transform.SetParent(scroll, false);
            obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = precio.ToString("F2");
            obj.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { obj.Alquilar(); });
            obj.GetComponentInChildren<CustomButton>().GetComponentInChildren<TextMeshProUGUI>().text = "Alquilar";
        }
    }

    public void SetupListReparar()
    {
        foreach (Transform item in scroll)
        {
            Destroy(item.gameObject);
        }

        foreach (var serial in player.flota.inventoryList)
        {
            RowBarcos obj = Instantiate(rowComprar, scroll);
            obj.hooverView = hooverView;
            obj.atarazanas = this;
            obj.barco = serial.itemInventory;
            float estado = serial.itemInventory.vida / serial.itemInventory.vidaTotal;
            float precio = (serial.itemInventory.precio * (1 - estado));
            obj.transform.SetParent(scroll, false);
            obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = precio.ToString("2F");
            obj.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { obj.Comprar(); });
            obj.GetComponentInChildren<CustomButton>().GetComponentInChildren<TextMeshProUGUI>().text = "Reparar";
        }
    }

    public void SetupListVender()
    {
        foreach (Transform item in scroll)
        {
            Destroy(item.gameObject);
        }

        foreach (var serial in player.flota.inventoryList)
        {
            RowBarcos obj = Instantiate(rowComprar, scroll);
            obj.hooverView = hooverView;
            obj.barco = serial.itemInventory;

            float precio = (serial.itemInventory.precio * 0.75f);// * (item.vida / item.vidaTotal)
            obj.transform.SetParent(scroll, false);
            obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = serial.itemInventory.ItemName;
            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = precio.ToString("2F");
            obj.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { obj.Comprar(); });
            obj.GetComponentInChildren<CustomButton>().GetComponentInChildren<TextMeshProUGUI>().text = "Vender";
        }
    }
    public void ComprarBarco(Barcos item)
    {
        SerialBarco found = listaBarcos.findItem(item);

        if (found != null)
        {

            if (player.playerCurrency.CurrencyQuantity >= found.precio)
            {
                Debug.Log(found.itemInventory.GetType());
                player.flota.Add(found.itemInventory);
                player.playerCurrency.CurrencyQuantity -= found.precio;
                if(player.flota.inventoryList.Count == 1)
                {
                    item.activo = true;
                    player.barcoActivo = item;
                    player.playerInventory = item.inventario;
                    GameObject barquito = Instantiate(player.barcoActivo.carcasa, barcoPuerto.transform.position, Quaternion.identity, barcoPuerto.transform);
                    barquito.transform.localScale = new Vector3(3.14f, 3.14f, 3.14f);
                }
            }
            else 
            { 
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No tienes suficiente oro";
                showModal();
            
            }
        }
    }

    public void VenderBarco(Barcos item)
    {

        SerialBarco found = player.flota.findItem(item);

        if (found != null)
        {
            string barcoName = item.ItemName;
            player.flota.Remove(item);
            float precio = (found.precio * 0.75f);// * (item.vida / item.vidaTotal);
            player.playerCurrency.CurrencyQuantity += precio;

            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Has vendido "+ barcoName;
            showModal();
            
        }
    }

    public void AlquilarBarco(Barcos item)
    {

        SerialBarco found = listaBarcos.findItem(item);

        if (found != null)
        {

            if (player.playerCurrency.CurrencyQuantity >= (found.precio*0.25f))
            {
                Debug.Log(found.precio);
                found.itemInventory.time = 1;
                player.flota.Add(found.itemInventory);
                player.playerCurrency.CurrencyQuantity -= (found.precio * 0.25f);
            }
            else
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No tienes suficiente oro";
                showModal();

            }
        }
    }

    public void RepararBarco(Barcos item)
    {

        SerialBarco found = player.flota.findItem(item);

        if (found != null)
        {
            float estado = item.vida / item.vidaTotal;
            if (estado < 1 && player.playerCurrency.CurrencyQuantity >= (found.precio*(1-estado)))
            {
                found.itemInventory.vida = found.itemInventory.vidaTotal;
                player.playerCurrency.CurrencyQuantity -= found.precio * (1 - estado);
            }
            else if( estado == 1)
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No puedes reparar el barco, está en pleno estado";
                showModal();
            }
            else if(player.playerCurrency.CurrencyQuantity < (found.precio * (1 - estado)))
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No puedes reparar el barco, no tienes suficiente dinero";
                showModal();
            }
        }
    }
}
