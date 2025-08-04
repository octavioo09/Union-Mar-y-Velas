
using System.IO;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class CSVcityStats : MonoBehaviour
{
    //[Sirenix.OdinInspector.FilePath]
    //public string filePath = "/Scriptable Objects/Items/Products";

    private static string csvPath = "/CosasCarlos/CSVs/puertos-mercancias.csv";
    private static string csvPathProducts = "/CosasCarlos/CSVs/productos.csv";


    [MenuItem("Utilities/Update City Stats")]
    public static void FillCityStats()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string[] sceneDataName = sceneName.Split('_');
        string cityName = sceneDataName[0]+"_Stats.asset";
        CityStatsSO cityStats = AssetDatabase.LoadAssetAtPath<CityStatsSO>("Assets/CosasCarlos/Scriptable Objects/CityStats/" + cityName);
        cityStats.cityName = sceneDataName[0];
        string[] lines = File.ReadAllLines(Application.dataPath + csvPath);
        string[] productsName = File.ReadAllLines(Application.dataPath +csvPathProducts);
        string[] productsData = productsName[0].Split(';');
        ProductsSO[] products = new ProductsSO[productsData.Length];
        int k = 0;
        foreach(string name in productsData)
        {
            products[k] = AssetDatabase.LoadAssetAtPath<ProductsSO>("Assets/CosasCarlos/Scriptable Objects/Items/Products/" + name + ".asset");
            k++;
            if(k == 60)
            {
                break;
            }
        }
        foreach (string line in lines) {
            string[] data = line.Split(';');
            if (data[0] == cityStats.cityName)
            {
                for(int i = 1; i < 61; i++)
                {
                    
                    foreach (ProductsSO product in products)
                    {
                        if(product.ID == i)
                        {
                            if(cityStats.GetPairByID(i).producto != null)
                            {
                            }
                            else
                            {
                                cityStats.GetPairByID(i, product);
                            }
                            //Debug.Log(cityStats.GetPairByID(i).producto.precio);
                            //cityStats.GetPairByID(i).producto = product;
                            //Debug.Log(product.name);
                            //Debug.Log(product.ID);
                            //Debug.Log(cityStats.GetPairByID(i).producto);

                            break;
                        }
                    }
                    cityStats.GetPairByID(i).existencias = (ItemStats)int.Parse(data[(i * 3) - 2]);
                    cityStats.GetPairByID(i).demanda = (ItemStats)int.Parse(data[(i * 3) - 1]);
                    cityStats.GetPairByID(i).produccion = (ItemStats)int.Parse(data[(i * 3)]);
                }
                AssetDatabase.Refresh();
                break;
            }
        }
    }

}
