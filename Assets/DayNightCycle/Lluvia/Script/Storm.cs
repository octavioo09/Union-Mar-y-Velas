using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    //Variables Privadas
    private DiaNoche DNScript;
    private float min, inter, espera;

    [Header("Dias")]
    public int dia;
    public int random;
    [Header("Objetos")]
    public ParticleSystem particleSys;
    public TerrainLayer []layer;
    public GameObject storm, nubes;
    public Light luna;
    [Header("Materiales")]
    public Material nube;
    public Material tormenta;
    [Header("Efectos")]
    public Light refucilo;
    public WindZone viento;
    public AudioSource soundAmbient, soundStorm;
    public GameObject vidrioVFX;

    void Start()
    {
        particleSys.Stop();
        refucilo.enabled = true;
        random = Random.Range(0, 3);
        DNScript = this.GetComponent<DiaNoche>();
    }

    void Update()
    {
        //Establecemos el tiempo Ciclo de 1 dia en 24 min reales
        min += DNScript.velTime * Time.deltaTime;//1 seg = a 1 min
        if (min >= 1440)// 1 dia
        {
            min = 0;
            dia++;
            random = Random.Range(0, 3);
        }
        if (dia > 3)
        {
            dia = 0;
        }

        if (dia == random)
        {
            particleSys.Play();
            for (int i = 0; i < layer.Length; i++)
            {
                layer[i].smoothness = 1;
            }
            nubes.GetComponent<Renderer>().material = tormenta;
            // nubes.GetComponent<AnimMaterial>().desplasarX = -0.5f;
            // nubes.GetComponent<AnimMaterial>().desplasarY = -0.5f;
            this.GetComponent<Light>().enabled = false;
            luna.enabled = false;
            storm.SetActive(true);
            refucilo.enabled = true;
            RenderSettings.fog = true;
            viento.windMain = -1;
            soundStorm.volume = 1;
            soundAmbient.volume = 0;
            vidrioVFX.SetActive(true);
        }
        else if(dia != random)
        {
            particleSys.Stop();
            for (int i = 0; i < layer.Length; i++)
            {
                layer[i].smoothness = 0;
            }
            nubes.GetComponent<Renderer>().material = nube;
            // nubes.GetComponent<AnimMaterial>().desplasarX = 0.1f;
            // nubes.GetComponent<AnimMaterial>().desplasarY = 0.1f;
            storm.SetActive(false);
            refucilo.enabled = false;
            RenderSettings.fog = false;
            viento.windMain = 0.25f;
            soundStorm.volume = 0;
            soundAmbient.volume = 1;
            vidrioVFX.SetActive(false);
        }
        if (refucilo.enabled)
        {
            // cada 10 un rayo
            espera += 1 * Time.deltaTime;
            if (espera > 10 && espera < 11)
            {
                inter += 10 * Time.deltaTime;
                if (inter >= 1)
                {
                    inter = 0;
                }
                refucilo.intensity = inter;
                storm.GetComponent<Renderer>().material.color = new Vector4(1, 1, 1, inter);
            }
            else if (espera > 11)
            {
                espera = 0;
            }
            else
            {
                storm.GetComponent<Renderer>().material.color = new Vector4(0, 0, 0, 0);

            }
           
        }
        
    }
}
