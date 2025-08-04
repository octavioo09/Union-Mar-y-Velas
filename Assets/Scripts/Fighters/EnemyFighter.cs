using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : Fighter
{
    // Start is called before the first frame update
    // protected void Start(){
     
    //  idName ="piratas";
    // }
    public override void InitTurn(){
      StartCoroutine(IA());
    }
    IEnumerator IA(){
      yield return new WaitForSeconds(1f);
      Skill skill = skills[Random.Range(0, skills.Length-1)];
      skill.SetEmitterAndReceiver(this, this.combatManager.GetOposingFighter());
      combatManager.OnFighterSkill(skill);
    }

     void OnTriggerEnter(Collider other){
      if(isAlive() == true)
        if(other.gameObject.tag == "Barco"&& !combatManager.getIsCombatActive()){
          target = other.gameObject.transform;
          ganador = false;
          transform.LookAt(new Vector3(target.position.x, target.position.y, target.position.z));
          
          combatManager.addFighter(this);
          combatManager.addFighter(other.GetComponent<BarcoFighter>()) ; 
          this.enabled = false;
          combatManager.prepareCombat();
        }
        else if(other.gameObject.tag == "WayPoint_Enemigo"&& !combatManager.getIsCombatActive()){
           target = other.gameObject.GetComponent<WayPoints>().nextPoint;
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }

        }

    
    void Update(){
      if(isAlive()){
        //ueq se muevaaa
        transform.Translate(new Vector3(0, 0,(Time.deltaTime* sol.GetComponent<DiaNoche>().velTime)*(stats.velocidad1*Time.deltaTime)));
      }
      else if (combatManager.getIsCombatActive  ()){
        //esperar 5 minutos y respawun
      }
    }

    

}
