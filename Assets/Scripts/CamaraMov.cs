using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMov : MonoBehaviour
{
    public Camera camara;
    public GameObject controlador_UI;
    private float zoom = 10f;
    // public float altura, posX, posY; //posiciones de la camara en  x y z
    public float speedH, speedV;
    float yaw, pitch;
    const int botonIzq = 0;
    const int botonDcho = 1;
    const int botonMedio = 2;
    //public LayerMask capaTransitable;
    // private Ray miRayo;
    // private RaycastHit informacionRayo;
    // void Start(){
    //   capaTransitable = LayerMask.GetMask("Raycast Detect");
    // }
    // Update is called once per frame
     private void Update()
    {
       
        if(Input.GetMouseButton(botonDcho)){
              yaw += speedH * Input.GetAxis("Mouse X");
              pitch -= speedV * Input.GetAxis("Mouse Y");
           // Debug.Log("hola boton izq");
             transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
         
        if(Input.GetMouseButton(botonMedio)){
             
          float newX = transform.position.x + Input.GetAxis("Mouse X");
          float newZ = transform.position.z + Input.GetAxis("Mouse Y");
          Vector3 newVec = new Vector3(newX, transform.position.y, newZ);
          transform.position = newVec;
            
        }
         
         camara.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoom;
    }
}
