using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using UnityEngine.SceneManagment;

public class UI_manager : MonoBehaviour
{
    // Start is called before the first frame update
    public float velTime;
    public GameObject infoBarcos;
    public TMP_Text[] textosBarcos; 
    public bool showInfo = true;
    public GameObject sol;
    [SerializeField] private GameObject botonPausa;
     [SerializeField] private GameObject menuPausa;

    
    public void Start(){
        menuPausa.SetActive(false);
    }
     public void setVelTime( float newVel){
        velTime = newVel;
        sol.GetComponent<DiaNoche>().velTime = velTime;
    }
    public void setInfo(){
        showInfo = false;
    }
    
    public void InformacionBarco(Barcos barco){
        showInfo = true;
     //   infoBarcos.RawImage.Tripulantes.numero.text = barco.tripulantes;
     //jeje nos e loq ue necesita;
   //  Debug.Log(textosBarcos[0].GetComponent<Text>().text);
     textosBarcos[0].text ="textoooo"; ///barco.tripulantes.ToString();
     textosBarcos[1].text = barco.soldados.ToString();
    }
    
    void Update(){
        // if(showInfo){
        //    // Debug.Log("cambio");
        //     infoBarcos.SetActive(true);
        // }
        // else{
        //     infoBarcos.SetActive(false);
        // }
    }

    public void Pausa(){
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Reanudar(){
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void Cerrar(){
        Application.Quit();
    }
}
