 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemigo1 : MonoBehaviour
{
    public Image barraVida;
    public ParticleSystem smoke;
    public float fuerza = 10;
    public bool enCombate = false, ganador = false, muerto = false, enPatrulla = false;
    public int soloUnaVez = 0;
  //  public Transform target;
    //
   // bool estarAlerta;
    // public LayerMask capaJugador;
    public float velocidadCombate;
    public float timer = 3.0f;
    //  public int modeloActivo = 1;

    // Start is called before the first frame update
    // void Start()
    // {
    //    var emission = smoke.emission;
    //         emission.enabled = false;
    //    // cambiarAmodelo1();
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     if(muerto == false){
    //         if(enCombate == false){ // esta patrullando
    //             //a patrullar
                
    //         }
    //         else{//entra en combate
    //             velocidadCombate--;
    //             if(velocidadCombate<=0){
    //                 velocidadCombate=0;
    //              //   camaraCombate(true);
    //                 timer -= Time.deltaTime;
    //                 if(timer < 0)
    //                 {
    //                     timer = 3f;
    //                     onCombat();
    //                 }
    //             }
    //             transform.Translate(new Vector3(0, 0,(Time.deltaTime* velocidadCombate)));
    //             if(ganador == true){
    //                 enCombate = false; 
    //                // camaraCombate(false);
    //             }

    //         }
    //     }
    //     else{
    //         //esperar 5 minutos a repawn
    //     }
    // }

    // private void OnDrawGizmos(){
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, rangoAlerta);
    // }
    // void cambiarAmodelo1(){
    //     entero.SetActive(true);
    //     semi.SetActive(false);
    //     destrozado.SetActive(false);
    // }
    // void cambiarAmodelo2(){
    //     entero.SetActive(true);
    //     semi.SetActive(true);
    //     destrozado.SetActive(false);
    // }
    // void cambiarAmodelo3(){
    //     entero.SetActive(false);
    //     semi.SetActive(false);
    //     destrozado.SetActive(true);
    // }

    // void OnTriggerEnter(Collider other){
    //     if(other.gameObject.tag == "Barco"){
           
    //         target = other.gameObject.transform;
    //       //  Debug.Log("trigger desde enemigo" + target);
    //       //  enCombate=true;
    //         ganador = false;
            
    //       //  Debug.Log(new Vector3(target.position.x, target.position.y, target.position.z));
    //         transform.LookAt(new Vector3(target.position.x, target.position.y, target.position.z));
    //         //  transform.Translate(new Vector3(0, 0,(Time.deltaTime* barco.velocidad1)));
    //     }

    // }

    // void calcularFuerza(){
    //   //  Debug.Log("calculo fuerza enemigo");
    //     fuerza = barco.soldados*3 + barco.tripulantes*1;
    // }
    // void calcularPerdidas(){
    //     barco.soldados = barco.soldados - (int)(barco.soldados*barco.perdidasSoldados);
    //     barco.tripulantes = barco.tripulantes - (int)(barco.tripulantes* barco.perdidasTripulantes);
    //     if(barco.soldados <00){
    //         barco.soldados=0;
    //         if(barco.tripulantes<= 0) barco.tripulantes = 1;
    //     }
    //     if(barco.tripulantes<0){
    //         barco.tripulantes = 0;
    //     }
    // }
    // void calcularNuevaVida(){
    //     barco.vida -= target.GetComponent<Barco>().fuerza;
    //     Debug.Log("calcular nueva vida enemigo");
    // }
    // void comprobarVida(){
    //     Debug.Log(barco.vida);
    //     if((barco.vida/barco.vidaTotal)<=0){
    //         cambiarAmodelo3();
    //         barco.vida = 0;
    //         muerto = true;
    //         Debug.Log("barco totalmente destruido");
    //         ganador = false;
    //         smoke.Play();
    //         camaraCombate(false);

    //         target.GetComponent<Barco>().ganador = true;
            
    //     }
    //      else if((barco.vida/barco.vidaTotal)<=0.5){
    //         //humo + modelo semidestruido
    //         var emission = smoke.emission;
    //         emission.enabled = true;
    //         if(soloUnaVez == 0){
    //             smoke.Play();
    //             cambiarAmodelo2();
    //             soloUnaVez = 1;
    //         }
            
    //         Debug.Log("barco a la mitad o menos "+ barco.vida);

    //     }
        
    // }

    void onCombat(){
        // calcularFuerza();
        // calcularNuevaVida();
        // calcularPerdidas();
        // //InvokeRepeating("comprobarVida",4.0f, 4.0f );
        // bajarVidaImagen();
        // comprobarVida();
        
    }
    // void bajarVidaImagen(){
    //     barraVida.fillAmount = barco.vida/barco.vidaTotal;
    //     if((barco.vida/barco.vidaTotal)<=0.5){
    //        // CambiarColor(new Color(1f, 0.5f, 0f, 1f));
    //         barraVida.color = new Color(1f, 0.5f, 0f, 1f);// naranja
    //     }
    //     if((barco.vida/barco.vidaTotal)<=0.1){
    //        // CambiarColor(new Color(1f, 0.5f, 0f, 1f));
    //         barraVida.color = new Color(1f, 0.5f, 0.5f, 1f);// rojo
    //     }
    // }
    void respawn(){
        //posicion resetteo de valores
    }
    // void camaraCombate(bool cam){
    //     Transform camaraCombate = transform.Find("CameraCombate");
    //     camaraCombate.GetComponent<Camera>().enabled = cam;
    // }
}
