using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viento : Evento
{
    public override void efecto(){
        if(active == true){
            
            createZone();
            PanelBatalla.Write("Parece que se levanta un viento turbulento...");
            calcularDireccion();
        }
        // setNewVel(barco.stats.velocidad1 -3.0f);
    }
    public void  createRotation(){
       float rotY = Random.Range(0.0f, 360.0f);
       transform.rotation = Quaternion.Euler(0f, rotY, 0f);
    }
     public override void desactivar(){
        PanelBatalla.Write("El aire se ha calmado...");
        active = false;
        barco.Clear();
        
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

    void calcularDireccion(){
        foreach(Fighter b in barco){
            Vector3 direccionToViento = (transform.position - b.gameObject.transform.position ).normalized;
            Vector3 forwardInB = b.gameObject.transform.forward;
            float dotProduct = Vector3.Dot(direccionToViento, forwardInB);

            if(dotProduct < 0.5f){
                // Debug.Log("Misma direccion");
            }else{
                // Debug.Log("Direccionn contraria");
            }
        }
        
    }
}
