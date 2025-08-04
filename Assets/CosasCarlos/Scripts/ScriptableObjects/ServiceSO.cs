using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Servicio")]
public class ServiceSO : Item
{
    public double time;
    public double benefits;
    public ItemTypeEnum type;
    [NonSerialized]
    public string city;
}
