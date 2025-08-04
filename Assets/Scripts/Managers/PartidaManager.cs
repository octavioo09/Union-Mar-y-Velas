using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PartidaManager : MonoBehaviour
{
   public GameObject[] barcos;
   public DespejeNiebla[] nieblas;
   //public DatosJuego datos = new DatosJuego ();
   public string archivoDeGuardado;


[System.Serializable]
    public class DatosBarco
    {
        public float vidaBarco;
        public Vector3 posBarco;
        public string nombreBarco;
        public Vector3 positionWp;
        public bool isActiveBarco;
    }

    [System.Serializable]
    public class DatosBarcosWrapper
    {
        public DatosBarco[] datosBarco;
    }

    public void GuardarDatos()
    {
        DatosBarco[] array = new DatosBarco[barcos.Length];

        for (int i = 0; i < barcos.Length; i++)
        {
            array[i] = new DatosBarco()
            {
                vidaBarco = barcos[i].GetComponent<Fighter>().stats.vida,
                posBarco = barcos[i].transform.position,
                nombreBarco = barcos[i].name,
                positionWp = barcos[i].GetComponent<Fighter>().target.position,
                isActiveBarco = barcos[i].activeSelf
            };
        }

        DatosBarcosWrapper wrapper = new DatosBarcosWrapper();
        wrapper.datosBarco = array;

        string cadenaJSON = JsonUtility.ToJson(wrapper);

        File.WriteAllText( archivoDeGuardado, cadenaJSON);
    }
  public void Awake(){
    archivoDeGuardado = Application.dataPath + "/datosJuego.json";
    // barcos = GameObject.FindObjectsOfType<Fighter>();
    // nieblas = GameObject.FindObjectsOfType<DespejeNiebla>();
   }

//     public void CargarDatos(){
//         if(File.Exists(archivoDeGuardado)){
//             string contenido = File.ReadAllText(archivoDeGuardado);
//             datos = JsonUtility.FromJson<DatosJuego>(contenido);
//         }
//     }

public void Update(){
    if(Input.GetKeyDown(KeyCode.C)){
        cargarDatos ();
    }
    if(Input.GetKeyDown(KeyCode.G)){
        GuardarDatos();
    }
}
public void cargarDatos(){
    if (File.Exists(archivoDeGuardado)){
        string contenido = File.ReadAllText(archivoDeGuardado);
        DatosBarcosWrapper wrapper = JsonUtility.FromJson<DatosBarcosWrapper>(contenido);
        DatosBarco[] datosBarcoArray = wrapper.datosBarco;
        for(int i = 0;i<barcos.Length;i++){
            barcos[i].GetComponent<Fighter>().stats.vida =datosBarcoArray[i].vidaBarco;
            barcos[i].transform.position = datosBarcoArray[i].posBarco;
            barcos[i].name = datosBarcoArray[i].nombreBarco;
            barcos[i].GetComponent<Fighter>().target.position = datosBarcoArray[i].positionWp;
            barcos[i].SetActive( datosBarcoArray[i].isActiveBarco); // = datosBarcoArray[i].isActiveBarco;
        }
    }   
}

// public void GuardarDatos(){
//     DatosBarco[] array = new DatosBarco[]{
//         new DatosBarco(){
//             vidaBarco = barcos[0].GetComponent<Fighter>().stats.vida, 
//             posBarco = barcos[0].transform.position, 
//             nombreBarco = barcos[0].name,
//             positionWp = barcos[0].GetComponent<Fighter>().target.position,
//             isActiveBarco= barcos[0].activeSelf
//         },
//         new DatosBarco(){
//             vidaBarco = barcos[1].GetComponent<Fighter>().stats.vida, 
//             posBarco = barcos[1].transform.position, 
//             nombreBarco = barcos[1].name,
//             positionWp = barcos[1].GetComponent<Fighter>().target.position,
//             isActiveBarco= barcos[1].activeSelf
//         }

//     }; 
    

//     foreach (var i in array){
//         Debug.Log("vida: "+i.vidaBarco);
//         Debug.Log("pos: "+i.posBarco);
//     }

//     // for(int i = 0; i < barcos.Length; i++){
//     //     DatosBarco datosB = new DatosBarco() {
//     //         vidaBarco = barcos[i].GetComponent<Fighter>().stats.vida, 
//     //         posBarco = barcos[i].transform.position, 
//     //         nombreBarco = barcos[i].name,
//     //         positionWp = barcos[i].GetComponent<Fighter>().target.position,
//     //         isActiveBarco= barcos[i].activeSelf
//     //     }; // Añadí el punto y coma aquí
//     //     array[i] = datosB;
//     // }
//     // DatosJuego nuevosDatos = new DatosJuego (){
//     //     datosBarco = array
//     // };
//     // string cadenaJSON = "["; // = JsonUtility.ToJson(nuevosDatos.datosBarco[0]+"\n"); 
//     // for(int i = 0;i < barcos.Length; i++){
//     //     cadenaJSON+= JsonUtility.ToJson(nuevosDatos.datosBarco[i]) +",s";
//     // } 
//     // cadenaJSON+="]";
//     string cadenaJSON = JsonUtility.ToJson (array);
//     Debug.Log(cadenaJSON    );
//     File.WriteAllText(Application.persistentDataPath + "/" + "datosJuego.json", cadenaJSON);
// }

//    public void CrearBarco(string tipoBarco){
//         //GameObjetct newbarco = GameObject.Find(tipoBarco+"-objeto");
//    }
}




