using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barco : MonoBehaviour
{
    // Start is called before the first frame update
    public Barcos barco;
    [SerializeField] DiaNoche sol;
    [SerializeField] Transform path_manager;
    public string destino;
    public bool ida = false, vuelta = false, enCombate = false, ganador = false, muerto = false;
    public string origen;

    public Transform target;
     public int posicion;
     public float fuerza, timer = 3.0f;

    // void Start(){
    //     EncontrarRuta();
    //     transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

    // }
    // void setTarget(){

    // }
    // void EncontrarRuta()
    // {
    //     for(int i = 0; i < path_manager.childCount ; i++){
    //         if(path_manager.GetChild(i).name == origen){
    //             ida = true;
    //             posicion = i;
    //             break;
    //             //ira por el nextWayPoint
    //         } else if ( path_manager.GetChild(i).name == destino){
    //             //ira por el lastWayPoint
    //             vuelta = true;
    //             posicion = i;
    //             break;
    //         }
    //     } 
    //     if(ida){
    //         for(int i = 0; i< path_manager.GetChild(posicion).childCount; i++){
    //             if(path_manager.GetChild(posicion).GetChild(i).name == destino){
    //                 Debug.Log("encontramos destino");
    //                 target = path_manager.GetChild(posicion).GetChild(i).GetChild(0);
    //                 //ponerle  el next point pa que lo siga, solo colocarlo
                    
    //             }
    //         }
    //     }
    // }
 
    // void IniciarRuta(){

    // }
    // void Update(){
    //     if(enCombate==false)//sigue la ruta si no esta en combate
    //         transform.Translate(new Vector3(0, 0,(Time.deltaTime* sol.GetComponent<DiaNoche>().velTime)*(barco.velocidad1*Time.deltaTime)));
    //     else{//si esta en combate
    //         transform.Translate(new Vector3(0, 0, 0));
    //         timer -= Time.deltaTime;
    //         if(timer < 0)
    //         {
    //             timer = 3f;
    //         //    onCombat();
    //         }
    //         if(ganador == true){
    //             enCombate = false;
    //         }

    //     }
      
    // }

    // void OnTriggerEnter(Collider other){
    //     if(other.gameObject.tag=="WayPoint"){
    //         target = other.gameObject.GetComponent<WayPoints>().nextPoint;
    //         transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    //       //  Debug.Log("holiwissss trigger");
    //     }
    //     if(other.gameObject.tag =="Enemigo"){
    //        // Debug.Log("holiwissss trigger2");
    //        // enCombate = true;
    //         ganador = false;
    //         target = other.gameObject.transform;
    //         Debug.Log( " trigger desde barco "+target);
    //         transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    //     }
    // }
    
   
}
