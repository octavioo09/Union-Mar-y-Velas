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
    public string ItemName; 

    
    public float precio;

    public string ItemDescription;

   

}
