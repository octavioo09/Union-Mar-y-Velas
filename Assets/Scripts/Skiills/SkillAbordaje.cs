using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAbordaje : Skill
{
    private int tolerancia = 60;
     protected override void OnRun()
    {
       
        lastSkill = "Abordaje";
        if(Vector3.Distance(emitter.transform.position, receiver.transform.position)< tolerancia){
             Animate("plancha");
            PanelBatalla.Write("Plancha colocada");
            emitter.abordaje = true;
            
        }
        else{
            PanelBatalla.Write("La plancha debe estar mas cerca");
        }
        
    }
}
