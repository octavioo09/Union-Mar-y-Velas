using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  abstract class Fighter : MonoBehaviour
{
    public string idName;
    public Image barraVida;
    public CombatManager combatManager;
    public Barcos stats;
    public Transform target, transformFighter;
    public GameObject semi, destrozado, entero;
    public bool ganador;
    public bool abordaje = false;

     public DiaNoche sol;
    public Transform path_manager;
    public string destino;
    public bool  muerto = false;
    // public string origen;
    public int posicion;
    //public PlayerSkillPanel skillPanel;

    protected Skill[] skills;
    public abstract void InitTurn();

    public bool isAlive(){
        if(stats.vida > 0.0f){
           
            return true;
        }
        else{
            stats.vida = 0.0f;
            return false;
        }
    }

  
    protected virtual void Start(){
        skills = this.GetComponentsInChildren<Skill>();
        cambiarAmodelo1();
    }

    void cambiarAmodelo1(){
        entero.SetActive(true);
        semi.SetActive(false);
        destrozado.SetActive(false);
    }
    void cambiarAmodelo2(){
        entero.SetActive(false);
        semi.SetActive(true);
        destrozado.SetActive(false);
    }
    void cambiarAmodelo3(){
        entero.SetActive(false);
        semi.SetActive(false);
        destrozado.SetActive(true);
    }

    void bajarVidaImagen(){
        barraVida.fillAmount = stats.vida/stats.vidaTotal;
        if((stats.vida/stats.vidaTotal)<=0.5){
           
            barraVida.color = new Color(1f, 0.5f, 0f, 1f);// naranja
            cambiarAmodelo2();
        }
        if((stats.vida/stats.vidaTotal)<=0.1){
           
            barraVida.color = new Color(1f, 0.5f, 0.5f, 1f);// rojo
            cambiarAmodelo3();
        }
    }

     public void calcularNuevaVida( float amount){
       this.stats.vida = Mathf.Clamp(this.stats.vida - amount, 0f, this.stats.vidaTotal);
       bajarVidaImagen();
      //  Debug.Log("calcular nueva vida enemigo");
    }

    public Barcos GetCurrentStats(){
        return stats;
    }
}
