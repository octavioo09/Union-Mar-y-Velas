using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class City : MonoBehaviour
{
    InventoryServiceSO services;
    double luck;
    PlayerSO player;
    // Start is called before the first frame update
    void Start()
    {
        CSVcityStats.FillCityStats();
        player = AssetDatabase.LoadAssetAtPath<PlayerSO>("Assets/CosasCarlos/Scriptable Objects/Player/myPlayerSO.asset");
        if (player.barcoActivo && player.barcoActivo.inventario)
        {
            player.playerInventory = player.barcoActivo.inventario;
        }
        else
        {
            player.playerInventory = ScriptableObject.CreateInstance<InventoryProductSO>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
