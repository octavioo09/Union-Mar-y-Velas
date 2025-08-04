using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Evento : MonoBehaviour
{
    // Start is called before the first frame update
  protected List<Fighter> barco;
  public bool active = false;
  void Start(){
    barco = new List<Fighter>();
  }

  public abstract void efecto();
  public abstract void desactivar();

  public void setBarco(Fighter fighter){
       // barco = fighter;
    }

    public void setNewVel( float newVel){
       // barco.stats.velocidad1 = newVel;
    }

     public void createZone(){
        float posX = Random.Range(-960.0f, 556.0f);
        float posZ = Random.Range(-804.0f, 300.0f);
        transform.position = new Vector3(posX, 50.0f, posZ);
    }
}
