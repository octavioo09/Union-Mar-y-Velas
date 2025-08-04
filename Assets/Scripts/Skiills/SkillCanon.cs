using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCanon : Skill
{
    private int enfriamiento = 1;
    public GameObject prefab;
    private int amount = 40;
    protected void Start(){
      prefab.SetActive(false);
    }
   protected override void OnRun()
  {
    lastSkill = "Canyon";
      if(enfriamiento>0){
        
        AnimateCanon("canon1");
        PanelBatalla.Write("Preparando para disparar");
        enfriamiento--;
       // getPerpendicular();
      }
      else{
     
        Animate("canon2");
        PanelBatalla.Write("Boom!");
        
        enfriamiento = 01;
        getPerpendicular();
      }
      
      
    
    // float amount = this.GetModification();
    // this.receiver.calcularNuevaVida(amount);
    
  }
  protected void AnimateCanon(string nombreAnimacion){
        if(prefab!= null){
            prefab.SetActive(true);
            var anim =prefab.GetComponent<Animator>();
            anim.Play(nombreAnimacion, -1, 0f);
             var go = Instantiate(effectPrfb, receiver.transform.position, Quaternion.identity);
             Destroy(go, animationDuration);
        }
  }
  public void getPerpendicular(){
        Vector3 direction = emitter.transform.forward;
        direction.Normalize();
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.up);
        Debug.DrawRay(emitter.transform.position, perpendicular, Color.green, 1500f);
        // Debug.Log(perpendicular);
        RaycastHit hit;
        if (Physics.SphereCast(emitter.transform.position, 50, perpendicular, out hit, 500f)){
            Vector3 objectPosition = hit.point; 
            float distance = Vector3.Distance(emitter.transform.position, objectPosition); 
            // Debug.Log("Ha chocao con algo");
            Disparar();
            if (distance < 100f)
            {
                // Se encontró una colisión dentro del rango especificado
                // Debug.Log("Se encontró una colisión dentro del rango con: " + objectPosition);
            }

        }
    

  }
  public void Disparar(){
     receiver.calcularNuevaVida(amount);

  }
}
