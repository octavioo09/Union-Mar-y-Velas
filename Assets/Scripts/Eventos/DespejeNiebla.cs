using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespejeNiebla : Evento

{
    public ParticleSystem niebla;
    public override void efecto(){
       
    }
    public override void desactivar(){
        niebla.loop = false;
    }
    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Barco"){
            desactivar();

        }
    }
}
