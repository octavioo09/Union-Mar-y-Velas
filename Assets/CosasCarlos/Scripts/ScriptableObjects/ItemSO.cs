using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using UnityEditor;
using UnityEngine;

[Serializable]
public abstract class Item : ScriptableObject 
{
    public string ItemName; /*= System.IO.Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(Selection.activeObject));*/

    //public ItemTypeEnum tipo1;
    //public ItemTypeEnum tipo2;

    //public int existencias;
    //public float peso;

    //public ItemContainerEnum soporte;
    public float precio;

    public string ItemDescription;

    //public List<Item> ItemsProducibles;

    //public List<Item> itemsNecesarios;


}
