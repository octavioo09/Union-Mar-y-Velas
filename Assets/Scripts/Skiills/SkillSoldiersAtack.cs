using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSoldiersAtack : Skill
{
  public float amount;
  protected override void OnRun()
  {
    lastSkill = "Soldados";
    if(emitter.abordaje){
      Animate("ataqueSoldados");
      float amount = this.GetModification();
      this.receiver.calcularNuevaVida(amount);
    }else{
      Debug.Log("Debes abordar primero");
      //PanelBatalla.Write("Debes abordar primero");
    }
    // float amount = this.GetModification();
    // this.receiver.calcularNuevaVida(amount);
    
  }

  public float GetModification(){
    //faltan
    if(emitter.GetCurrentStats()!= null){
      //Debug.Log("noo es null");
    }
    Barcos emitterStats = this.emitter.GetCurrentStats();
    //Barcos receiverStats = this.emitter.GetCurrentStats();
    
    float rawDamage = emitterStats.tripulantes + (emitterStats.soldados*3);

    return rawDamage;
    
    
    }
}
