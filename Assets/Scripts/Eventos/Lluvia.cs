using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lluvia : Evento
{
    //sistema particulas
    ParticleSystem lluvia;

    public override void efecto(){
        if(active == true){
            createZone();
            bajarVidaBarco();
            PanelBatalla.Write("Empieza a llover...");
        }
        

        //1 crear zona -> elegir zona aleatoria
        //le baja al barco un % de vida si atraviesa por ahi

    }
    // public void createZone(){
    //     float posX = Random.Range(-960.0f, 556.0f);
    //     float posZ = Random.Range(-804.0f, 300.0f);
    //     transform.position = new Vector3(posX, 50.0f, posZ);
    // }
    public void bajarVidaBarco(){
        // foreach(Fighter b in barco){
        //     b.stats.vida -= 50;
        // }
        // if(barco!=null)
        //     barco.stats.vida -= 50;//MIRAR SI ESTO ESTA BIEN
    }

    public override void desactivar(){
        //parar loop
        PanelBatalla.Write("La tormenta cada vez es más débil...");
        active = false;
        barco.Clear();
        
       // lluvia.main.loop = false;
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Barco"){
            barco.Add(  other.gameObject.GetComponent<Fighter>());
            efecto();
        }
            // Debug.Log("entra algo");
    }
}
    

