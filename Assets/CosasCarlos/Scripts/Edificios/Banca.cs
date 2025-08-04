using TMPro;



public class Banca : Building
{

    public enum BancaUI
    {
        None = 0,
        Active = 1,
        Input = 2
    }

    public ComercioView bancaView;
    public ComercioView inputView;
    public InputRow inputRow;
    public View modalView;
    public CustomButton closeButton;
    private BancaUI layer;

    double money;
    double prestamo;
    double interes;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        closeButton.onClick.AddListener(delegate { exitView(); });

        CustomButton[] bancaButtons = bancaView.GetComponentsInChildren<CustomButton>();
        bancaButtons[0].onClick.AddListener(delegate { showPrestamo(); });
        bancaButtons[1].onClick.AddListener(delegate { showIngresar(); });
        bancaButtons[2].onClick.AddListener(delegate { showRetirar(); });
        modalView.GetComponentInChildren<CustomButton>().onClick.AddListener(delegate { closeModal(); });  

        bancaView.gameObject.SetActive(false);
        inputView.gameObject.SetActive(false);
        modalView.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);

    }

    public void showUI()
    {
        if (getEntrance() == true)
        {
            if (!active)
            {
                bancaView.gameObject.SetActive(true);
                inputView.gameObject.SetActive(false);
                modalView.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(true);
                layer = BancaUI.Active;
                active = true;
            }
        }
        else
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Debes pasar primero por la aduana";
            showModal();
        }
    }

    public void showPrestamo()
    {
        bancaView.gameObject.SetActive(false);
        inputView.gameObject.SetActive(true);
        CustomButton inputButton = inputRow.GetComponentInChildren<CustomButton>();
        Text[] texts = inputView.GetComponentsInChildren<Text>();
        texts[1].GetComponentInChildren<TextMeshProUGUI>().text = "Cantidad a pedir prestada";
        inputButton.onClick.AddListener(delegate { hacerPrestamo(); });
        inputButton.GetComponentInChildren<TextMeshProUGUI>().text = "Pedir prestado";
        layer = BancaUI.Input;
    }

    public void showIngresar()
    {
        bancaView.gameObject.SetActive(false);
        inputView.gameObject.SetActive(true);
        Text[] texts = inputView.GetComponentsInChildren<Text>();
        texts[1].GetComponentInChildren<TextMeshProUGUI>().text = "Cantidad a ingresar";

        CustomButton inputButton = inputRow.GetComponentInChildren<CustomButton>();
        inputButton.onClick.AddListener(delegate { ingresarDinero(); });
        inputButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ingresar";

        layer = BancaUI.Input;
    }

    public void showRetirar()
    {
        bancaView.gameObject.SetActive(false);
        inputView.gameObject.SetActive(true);
        //inputView.transform.Find("textp").GetComponent<TextMeshProUGUI>().text = "Cantidad a retirar";
        Text[] texts = inputView.GetComponentsInChildren<Text>();
        texts[1].GetComponentInChildren<TextMeshProUGUI>().text = "Cantidad a retirar";

        CustomButton inputButton = inputRow.GetComponentInChildren<CustomButton>();
        inputButton.onClick.AddListener(delegate { retirarDinero(); });
        inputButton.GetComponentInChildren<TextMeshProUGUI>().text = "Retirar";

        layer = BancaUI.Input;
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


        if (layer == BancaUI.Active)
        {
            bancaView.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            layer = BancaUI.None;
            active = false;
        }

        else if (layer == BancaUI.Input)
        {
            inputView.gameObject.SetActive(false);
            bancaView.gameObject.SetActive(true);
            layer = BancaUI.Active;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void hacerPrestamo()
    {

        inputRow.getText();
        if (prestamo > 0)
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡Ya has pedido un préstamo!";
            showModal();
        }
        else if (inputRow.text != "")
        {
            if (double.TryParse(inputRow.text, out var quantity))
            {
                prestamo = quantity;
                city.time_prestamo = 1;
                player.playerCurrency.CurrencyQuantity += (float)quantity;
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Has recibido prestado " + quantity + " reales.\n Deberás devolver "+quantity*1.2+" reales.";
                showModal();
            }
            else
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No es válido";
                showModal();
            }
        }
    }

    public void ingresarDinero()
    {

        inputRow.getText();
        if (player.playerCurrency.CurrencyQuantity <= 0)
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡No tienes dinero que ingresar!";
            showModal();
        }
        else if (inputRow.text != "")
        {
            if (double.TryParse(inputRow.text, out var quantity))
            {
                money += quantity;
                player.playerCurrency.CurrencyQuantity -= (float)quantity;
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Has ingresado " + quantity + " reales.";
                showModal();
            }
            else
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No es válido";
                showModal();
            }
        }
    }

    public void retirarDinero()
    {

        inputRow.getText();
        if (money <= 0)
        {
            modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡No hay dinero disponible para retirar!";
            showModal();
        }
        else if (inputRow.text != "")
        {
            if (double.TryParse(inputRow.text, out var quantity) && money > quantity)
            {
                money -= quantity;
                player.playerCurrency.CurrencyQuantity += ((float)quantity - ((float)quantity*0.02f));
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Has retirado " + ((float)quantity - ((float)quantity * 0.02f)) + " reales, has pagado "+ ((float)quantity * 0.02f)+" reales.";
                showModal();
            }
            else
            {
                modalView.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "No es válido";
                showModal();
            }
        }
    }
}
