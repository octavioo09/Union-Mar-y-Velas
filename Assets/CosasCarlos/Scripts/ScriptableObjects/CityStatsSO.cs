using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "City Stats", menuName = "ScriptableObjects/City Stats")]
public class CityStatsSO : ScriptableObject
{
    [Serializable]
    public class Pair
    {
        
        public ProductsSO producto;
        public ItemStats existencias;
        public ItemStats demanda;
        public ItemStats produccion;
    }

    [HideInInspector]
    public string cityName;

    [SerializeField]
    private List<Pair> cityStats;

    public void Start()
    {
        cityStats = new List<Pair>();
        for(int i = 0; i< 61; i++)
        {
            cityStats.Add(new Pair { });
        }
    }
    public Pair GetPair(ProductsSO producto)
    {
        foreach (Pair pair in cityStats)
        {
            if (pair.producto == producto)
            {
                return pair;
            }
        }
        Pair pairVacio = new();
        return pairVacio;
    }

    public Pair GetPairByID(int ID)
    {
        foreach (Pair pair in cityStats)
        {
            if (pair != null && pair.producto.ID == ID)
            {
                return pair;
            }
        }
        Pair pairVacio = new();
        return pairVacio;
    }

    public Pair GetPairByID(int ID, ProductsSO producto)
    {
        foreach (Pair pair in cityStats)
        {
            if (pair.producto.ID == ID)
            {
                return pair;
            }
        }
        Pair pairVacio = new();
        pairVacio.producto = producto;
        //Debug.Log(pairVacio.producto.precio);
        return pairVacio;
    }
    public void setProduct(Pair pair, ProductsSO product)
    {
        if(pair.producto == null)
        {
            pair.producto = new();
        }
        pair.producto = product;
    }

    //public ProductsSO getProductByID(int ID)
    //{

    //}

    //public void setStats(int ID, int newExistencias, int newDemandas, int newProduccion)
    //{
    //    foreach (Pair pair in cityStats)
    //    {
    //        if (pair.producto.ID == ID)
    //        {
    //            pair.existencias = newExistencias;

    //        }
    //    }

    //}
}