using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niebla : Evento
{
    // Start is called before the first frame update
    public override void efecto(){
        if(active == true){
            createZone();
            PanelBatalla.Write("Una niebla impide que veas más allá...");

        }
    }
    public override void desactivar(){
        PanelBatalla.Write("Cada vez se despeja más...");
        active = false;
        barco.Clear();
        
        
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Barco"){
            barco.Add( other.gameObject.GetComponent<Fighter>());
            efecto();
        }
            // Debug.Log("entra algo");
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.tag=="Barco"){
            //se ponbe la velocidad normal
        }
    }
}
