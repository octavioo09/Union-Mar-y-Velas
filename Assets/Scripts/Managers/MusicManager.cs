using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource navegacion;
     public AudioSource combate;
     public CombatManager combatManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!combate.isPlaying && combatManager.getCombatActive()){
            navegacion.Pause();
            combate.Play();
        }
        else if(!navegacion.isPlaying && !combatManager.getCombatActive()){
            navegacion.Play();
        }
    }
}
