using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMoveIA : Skill
{
    private float nuevaPosX, nuevaPosZ, tolerancia = 10;
    private  Vector3 randomPoint;
    protected override void OnRun(){
        
        // nuevaPosX = Random.Range(this.emitter.transform.position.x - 70, this.emitter.transform.position.x + 70);
        // nuevaPosZ = Random.Range(this.emitter.transform.position.z - 70, this.emitter.transform.position.z + 70);

         randomPoint = Vector3.Lerp(emitter.transform.position, receiver.transform.position, Random.value);
        // Debug.Log("El punto aleatorio entre los objetos es: " + randomPoint);
        emitter.transform.LookAt(randomPoint);
        animationDuration = Vector3.Distance(emitter.transform.position, randomPoint)/ emitter.stats.velocidad1;
        this.enabled = true;
        
        emitter.enabled = true;
    }

    private void Start(){
        this.enabled = false;
    }
    private void Update(){
        // Debug.Log("IA llega a punto");
        emitter.transform.Translate(new Vector3(0, 0,(emitter.stats.velocidad1*Time.deltaTime)));
        if (Vector3.Distance(emitter.transform.position, randomPoint) < tolerancia){
            // Debug.Log("IA llega a punto");
            this.enabled = false;
            emitter.enabled = false;
        }

    }
}
