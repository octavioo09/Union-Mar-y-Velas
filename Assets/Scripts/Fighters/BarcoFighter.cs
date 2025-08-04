using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcoFighter : Fighter
{
     public PlayerSkillPanel skillPanel;
    void Awake(){
       //EncontrarRuta();
        transform.LookAt(new Vector3(0, transform.position.y, 0));
        // Debug.Log("Hace un look at a  "+new Vector3(target.position.x, transform.position.y, target.position.z));

         
    }
    //   protected void Start(){
    //      EncontrarRuta();
    //     transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    //    // idName= "zabra";
        
    // }
    public override void InitTurn(){
            skillPanel.Show();
             for( int i = 0; i < skills.Length; i++){
                skillPanel.ConfigureButtons(i, skills[i].skillName, this);
             }
    }
      public void ExecuteSkill(int index){
        skillPanel.Hide();

        Skill skill = skills[index];
        // Debug.Log("index: " +index);
        skill.SetEmitterAndReceiver(this, combatManager.GetOposingFighter());
        combatManager.OnFighterSkill(skill);

        // Debug.Log($"  Running {skill.skillName} skill");
    }


        void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="WayPoint" && !combatManager.getCombatActive() /*&&other.gameObject.name == idName+"Target"*/){
            Debug.Log("llego al wp");
            stats.velocidad1 = 0;
           // target = other.gameObject.GetComponent<WayPoints>().nextPoint;
            //transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        }
        if(other.gameObject.tag =="Enemigo"){
      
            ganador = false;
        
        }
        if(other.gameObject.tag == "Mapa"){
            stats.velocidad1 = 0;
        }

        
    }
    void OnTriggerStay(Collider collision){
        
        if(collision.gameObject.tag =="Mapa"){
            stats.velocidad1 = 0;
        }
    }     

    // public void ExecuteSkill(int index){
    //     skillPanel.Hide();

    //     Skill skill = skills[index];

    //     Debug.Log($"  Running {skill.skillName} skill");
    // }

    void Update(){
        if(target!=null)
        if(combatManager.getIsCombatActive()== false && isAlive()&& target.position != transform.position){    
            transform.Translate(new Vector3(0, 0,(Time.deltaTime* sol.GetComponent<DiaNoche>().velTime)*(stats.velocidad1*Time.deltaTime)));
        }
        if(target.position == transform.position){
            Debug.Log("no se mueve");
        }
    }

    //   void EncontrarRuta()
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
    }

