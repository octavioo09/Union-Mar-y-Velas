using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Barco", menuName = "ScriptableObjects/Barco")]
public class Barcos : Item
{
    // Start is called before the first frame update
    public int artilleria;
    public int soldados;
    public int  tripulantes;
    public float velocidad1, velocidad2, velocidadMax;
    public GameObject carcasa;
    public float vida;
    public float vidaTotal;
    public float fuerza;

    public float perdidasTripulantes;
    public float perdidasSoldados;
    //artilleria???
    [TextArea(2, 2)]
    public string descripcion;
    public int capacity;
    public bool activo;//para el puerto
    public double time = -1;
    public InventoryProductSO inventario;

    
}

