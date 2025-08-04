using TMPro;
using UnityEditor;


public class Iglesia : Building
{
    public ComercioView iglesiaView;
    public View modalView;
    public CustomButton closeButton;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        closeButton.onClick.AddListener(delegate { exitView(); });

        RowServicios[] rows = iglesiaView.GetComponentsInChildren<RowServicios>();
        ChurchServiceSO servicio = AssetDatabase.LoadAssetAtPath<ChurchServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Misas_Difuntos.asset");
        servicio.city = city.name;
        rows[0].GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { comprarServicio(servicio); });

        ChurchServiceSO servicio2 = AssetDatabase.LoadAssetAtPath<ChurchServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Misas_Futuro.asset");
        servicio2.city = city.name;
        rows[1].GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { comprarServicio(servicio2); });


        ChurchServiceSO servicio3 = AssetDatabase.LoadAssetAtPath<ChurchServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Donacion_Pobres.asset");
        servicio3.city = city.name;
        rows[2].GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { comprarServicio(servicio3); });


        ChurchServiceSO servicio4 = AssetDatabase.LoadAssetAtPath<ChurchServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Donacion_Enfermos.asset");
        servicio4.city = city.name;
        rows[3].GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { comprarServicio(servicio4); });


        ChurchServiceSO servicio5 = AssetDatabase.LoadAssetAtPath<ChurchServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Capellania.asset");
        servicio5.city = city.name;
        rows[4].GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { comprarServicio(servicio5); });


        ChurchServiceSO servicio6 = AssetDatabase.LoadAssetAtPath<ChurchServiceSO>("Assets/CosasCarlos/Scriptable Objects/Items/Servicios/Mejorar_Iglesia.asset");
        servicio6.city = city.name;
        rows[5].GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { comprarServicio(servicio6); });

        modalView.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { closeModal(); });



        iglesiaView.gameObject.SetActive(false);
        modalView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }

    public void showUI()
    {
        if (getEntrance() == true)
        {
            if (!active)
            {
                iglesiaView.gameObject.SetActive(true);
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
        if (!modalView.isActiveAndEnabled)
        {
            iglesiaView.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            active = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void comprarServicio(ChurchServiceSO service)
    {
        SerialService found = player.inventoryService.findItem(service);

        if (found == null)
        {
            if (player.playerCurrency.CurrencyQuantity > service.precio)
            {
                player.inventoryService.Add(service);
                //city.services.Remove(service);
                player.playerCurrency.CurrencyQuantity -= service.precio;
                if(city.luck < service.luck)
                {
                    city.luck = service.luck;
                    city.luckTime = service.time;
                }
            }
        }
        else if (found != null || (found != null && !found.hasService))
        {
            SerialService playerService = player.inventoryService.findItem(service);
            {
                if(playerService != null)
                {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡Ya tienes este servicio!";
                showModal();

                }
            }
        }

    }
}
