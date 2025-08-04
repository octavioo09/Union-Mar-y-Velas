using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VientoFavorable : Evento
{
    // Start is called before the first frame update
     public override void efecto(){
        if(active == true){
             PanelBatalla.Write("Parece que se levanta un viento favorable...");

        }
        // setNewVel(barco.stats.velocidad1 +3.0f);
     }
     public override void desactivar(){
        active = false;
        barco.Clear();
        PanelBatalla.Write("El aire se ha calmado...");
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Barco"){
            barco.Add(other.gameObject.GetComponent<Fighter>());
            efecto();
        }
            
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.tag=="Barco"){
            //se ponbe la velocidad normal
        }
    }
}
