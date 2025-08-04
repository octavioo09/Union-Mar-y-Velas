using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
public class CambioCamara : MonoBehaviour
{
   public Camera[] camaras;
   public RawImage panelInformativo;
   public CombatManager combatManager;
   public UI_manager UIManager;
   private bool activo = false, barcoSeleccionado = false;
    public  LayerMask detectionLayer;
    private GameObject barcoDetector;
    void Start()
    {
        // camaras[0].gameObject.SetActive(true);
         camaras[1].gameObject.SetActive(false);
         panelInformativo.enabled = false;
          panelInformativo.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("pulso espacio");
        if(Input.GetKeyDown(KeyCode.Space)){
            activo = !activo;
            //camaras[0].gameObject.SetActive(false);
            if(activo == true){
                camaras[0].gameObject.SetActive(false);
                camaras[1].gameObject.SetActive(true);
                
            }
            else{
                barcoSeleccionado= false;
                activo = false;
                camaras[0].gameObject.SetActive(true);
                camaras[1].gameObject.SetActive(false);
            }

        }
        if(Input.GetMouseButtonDown(0)){
            if(barcoSeleccionado == false){
                 DetectObject();
            }
            else{
                Debug.Log("nueva pos");
                barcoSeleccionado= false;
                SetNuevaPos();
                panelInformativo.enabled = false;
                panelInformativo.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                //barcoDetector.GetComponent<BarcoFighter>().target;
            }
           
        }

        
        if(combatManager.getIsCombatActive()){
            camaras[0].gameObject.SetActive(true);
            camaras[1].gameObject.SetActive(false);
        }

        
        

    }
   private void SetNuevaPos(){
         Ray ray = camaras[1].ScreenPointToRay(Input.mousePosition);
            
            // Crear un plano en el mundo (puedes cambiar esto a cualquier superficie que desees)
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        // Intersectar el rayo con el plano
        if (plane.Raycast(ray, out distance))
        {
            Vector3 mousePositionWorld = ray.GetPoint(distance);

            
            GameObject obj = GameObject.Find("Target"+barcoDetector.name);
            Transform newTransform;// = obj.transform;
             if(obj != null){
                newTransform = obj.transform;
             }
             else{
                obj = new GameObject("Target"+barcoDetector.name);
                 newTransform = obj.transform;
                 obj.AddComponent<BoxCollider>(); // as BoxCollider;
                 obj.GetComponent<BoxCollider>().isTrigger = true;
                 obj.tag = "WayPoint";
                 }
                        barcoDetector.GetComponent<BarcoFighter>().target = newTransform;
            newTransform.position = new Vector3(mousePositionWorld.x, barcoDetector.transform.position.y, mousePositionWorld.z);
            //barcoDetector.GetComponent<BarcoFighter>().target.position = new Vector3(mousePositionWorld.x, barcoDetector.transform.position.y, mousePositionWorld.z);
            Vector3 sourcePostion = barcoDetector.transform.position;//The position you want to place your agent
            // NavMeshHit closestHit;
            // if( NavMesh.SamplePosition(  sourcePostion, out closestHit, 500, 1 ) ){
            // barcoDetector.transform.position = closestHit.position;
            // barcoDetector.AddComponent<NavMeshAgent>();
           
            // barcoDetector.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = (newTransform.position);
            // //TODO
            // }
            barcoDetector.transform.LookAt(newTransform.position);
            
            barcoDetector.GetComponent<BarcoFighter>().stats.velocidad1 = barcoDetector.GetComponent<BarcoFighter>().stats.velocidadMax;
        }
    }

    private void DetectObject(){
        if(activo == true){
           
            Ray ray  = camaras[1].ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, detectionLayer))
            {
                // El rayo ha golpeado un objeto en la capa especificada
                barcoDetector = hit.collider.gameObject;
                Debug.Log("Se ha detectado el objeto: " + barcoDetector.name);
                barcoSeleccionado = true;
                panelInformativo.enabled = true;
                panelInformativo.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                string frase ="Has seleccionado el barco: "+barcoDetector.name;
                panelInformativo.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = frase;
                //mandar el barco al siguiennte punto queq se quiera
                
            }
        }
    }
}
