using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class Skill : MonoBehaviour
{
    public  string skillName;
    protected string lastSkill;
    public float animationDuration;
    public GameObject effectPrfb;
    

    protected Fighter emitter;
    protected Fighter receiver;
    // protected int enfriamiento;

    protected void Awake(){
        effectPrfb.SetActive(false);
    }
    protected void Animate(string nombreAnimacion){
        if(effectPrfb!= null){
            effectPrfb.SetActive(true);
            var anim =effectPrfb.GetComponent<Animator>();
            anim.Play(nombreAnimacion,-1, 0f);
             var go = Instantiate(effectPrfb, receiver.transform.position, Quaternion.identity);
             Destroy(go, animationDuration);
        }
        
        

    }

    public void Run(){
        //this.Animate();
        this.OnRun();
    }

    public void SetEmitterAndReceiver(Fighter em, Fighter rec){
        // Debug.Log("set em y rec");
        emitter = em;
        receiver = rec;
    }

    public void disableFighters(){
        // Debug.Log("disable emitter");
        emitter.enabled = false;
        receiver.enabled = false;
    }
    protected abstract void OnRun();
}
