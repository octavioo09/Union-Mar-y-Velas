//https://www.youtube.com/watch?v=6LaJoj6xi_c&list=PLQvxRv0FutJP04UqiGKqS1-fivd43YjJy&index=6

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaNoche : MonoBehaviour
{
    public float minutos, grados; 
    public float velTime = 10;
    const int segsEnUnDia = 1440;

    public Light luna;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        minutos += velTime * Time.deltaTime;
        if(minutos >= segsEnUnDia){ minutos = 0; }

        //1 grado = 0.25 minutos
        grados = minutos/4;  //1/4 = 0.25

        transform.localEulerAngles = new Vector3 ( grados, -90f, 0.0f);

        if(grados >= 180){
            this.GetComponent<Light>().enabled = false;
            luna.enabled = true;
        }
        else{
            this.GetComponent<Light>().enabled = true;
            luna.enabled = false;
        }


    }
}
