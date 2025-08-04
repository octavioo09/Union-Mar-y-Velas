using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMoverse : Skill
{
     private bool waitImput = false, selectPosition = false;
     public static event System.Action InputReceived;
    private bool inputReceived = false;
    private Vector3 worldPosition;
    private Camera cam;

    private void Start(){
       this.enabled = false; 
    }
   private void Update()
    {
        
      if (Input.GetMouseButton(0) && inputReceived && waitImput== false )
        {
            waitImput= true;
           // StopCoroutine(moveShip());
        //   Debug.Log("segfundo click");
            selectPosition = true;
            
            InputReceived?.Invoke();
             Time.timeScale = 1f;
             getCoordinates();
             
             inputReceived =false;
            // StopAllCoroutines();
             //moveShip();
          //  StartCoroutine(moveShip());
             
            //hacer el run del movimento y que no vuelva hasta terminar todo el movimiento.
        }
         else if (Input.GetMouseButton(0) && !inputReceived)
        {
            inputReceived = true;
            //InputReceived?.Invoke();
        }
        if(selectPosition == true){
            
            if(IsAtDestination()){
                // Debug.Log("estamos en destino");
                emitter.transform.Translate(new Vector3(0, 0,0));
                selectPosition= false;
            }
            else{
                emitter.transform.Translate(new Vector3(0, 0,(emitter.stats.velocidad1*Time.deltaTime)));
                Debug.Log("select input = true");
            }
        }
        
    }
   
   
   
   

    protected override void OnRun(){
        this.enabled = true;
        // Debug.Log("Decidiendo posicion...");
        StopAllCoroutines();
        Time.timeScale = 0f;
        
        selectPosition = false;
        inputReceived = false;
       
        InputReceived = null;
        waitImput = false;
        //StopCoroutine(moveShip());
        //StartCoroutine(WaitForInput());
        // while(!waitImput){
        //     if(Input.GetMouseButton(0)){//boton izq
        //         waitImput = true;
        //         Vector3 screenPos = Input.mousePosition;
        //         Ray ray = Camera.main.ScreenPointToRay(screenPos);
        //         Plane plane = new Plane(Vector3.up, Vector3.zero);
        //         Vector3 worldPosition =Vector3.zero;

        //         float distance;
        //         if(plane.Raycast(ray, out distance))
        //             worldPosition = ray.GetPoint(distance);
                
        //         Debug.Log("posicion  en el mundo: " +worldPosition);
        //     }
        // }
    } 
    
    // IEnumerator WaitForInput(){
    //     yield return new WaitUntil(() => waitImput);
    //     Debug.Log("Input recibido!");
    // }
    public void getCoordinates(){
        // Debug.Log("skill moverse11111111111 "+this.enabled);
        Vector3 screenPos = Input.mousePosition;
        cam = GameObject.Find("Cenital").GetComponent<Camera>();
        Ray ray = cam.ScreenPointToRay(screenPos);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        worldPosition =Vector3.zero;
        // Debug.Log("skill moverse222222222222 "+this.enabled);
        float distance;
        if(plane.Raycast(ray, out distance)){
            worldPosition = ray.GetPoint(distance);
            //   Debug.Log("posicion  en el mundo: " +worldPosition);
        }
            
        
      
        emitter.transform.LookAt(new Vector3(worldPosition.x, 0.0f, worldPosition.z));


        animationDuration = Vector3.Distance(emitter.transform.position, worldPosition)/ emitter.stats.velocidad1;
        // Debug.Log("Tiempo que ttarda de ir de a a b: "+animationDuration);
        

        // Debug.Log("Hace un look at a  "+worldPosition);
        inputReceived = false;
    }

    public void moveShip(){
        // Debug.Log(  "Nombre emisor: " +emitter.idName);
        //emitter.enabled = true;
        // while(!IsAtDestination()){
        //     emitter.transform.Translate(new Vector3(0, 0,(emitter.stats.velocidad1*Time.deltaTime)));

        //     //yield return null;
        // }
        
    }

    bool IsAtDestination(){
        int tolerancia = 15;
        //this.enabled = false;
        
        //Debug.Log(Vector3.Distance(emitter.transform.position, worldPosition));
        if (Vector3.Distance(emitter.transform.position, worldPosition) < tolerancia){
             Debug.Log("llego al desttino");
           // StopCoroutine(moveShip());
            return true;
        }
        return false;
    }
   
}
