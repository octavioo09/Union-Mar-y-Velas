using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "City", menuName = "ScriptableObjects/City")]
public class CitySO : ScriptableObject
{
    public InventoryServiceSO services;
    public double luck;
    public double luckTime;
    public int AlmacenCount;
    public string cityName;
    public bool entrancePaid;
    public bool exitPaid;

    //Ayuntamiento
    public double impuestos_arrendados;
    public double time_impuestos;

    //Banca
    public double dinero_prestado;
    public double dinero_ingresado;
    public double time_prestamo;


}


