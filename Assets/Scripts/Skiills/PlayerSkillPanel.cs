using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSkillPanel : MonoBehaviour
{
    private  BarcoFighter emitter;
    //private int index;
    public GameObject[] skillButtons;
    public TMP_Text[] skillButtonLabels;

    void Awake(){
        this.Hide();
        foreach (var btn in skillButtons){
            btn.SetActive(false);
        }
    }

    public void ConfigureButtons(int index, string skillName, BarcoFighter fighter){
        
        skillButtons[index].SetActive(true);
        skillButtonLabels[index].text = skillName;
        
        emitter = fighter;
        Button boton = skillButtons[index].GetComponent<Button>();
        boton.onClick.RemoveAllListeners();//si no se iran acumulando
        if(boton != null){
            switch (index){
                case 0: 
                    boton.onClick.AddListener(OnClikButton0);
                break;
                case 1:
                    boton.onClick.AddListener(OnClikButton1);
                break;
                case 2:
                    boton.onClick.AddListener(OnClikButton2);
                break;
                default:
                    boton.onClick.AddListener(OnClikButton3);
                break;        }
        }
        else Debug.Log("Error: no se encuentra el boton");
        
    }

    public void OnClikButton0(){
        emitter.ExecuteSkill(0);
    }
     public void OnClikButton1(){
        emitter.ExecuteSkill(1);
    }
     public void OnClikButton2(){
        emitter.ExecuteSkill(2);
    }
     public void OnClikButton3(){
        // Debug.Log("anyado moverse");
        emitter.ExecuteSkill(3);
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }
}
