using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Productos")]
public class ProductsSO : Item
{

    public int ID;
    public ItemTypeEnum tipo1;
    public ItemTypeEnum tipo2;

    public int existencias;
    public float peso;

    public Dictionary<string, int> nvlExistencias = new Dictionary<string, int>{ { "Muy bajo",  33 },
                                                                                 { "Bajo",      16 },
                                                                                 { "Medio",      0 },
                                                                                 { "Alto",      -16},
                                                                                 { "Muy alto",  -33} };

    public ItemContainerEnum soporte;


    public int cantidad;

    public List<Item> ItemsProducibles;

    public List<Item> itemsNecesarios;
}
