using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CombatStatus{
  WAITING_FOR_FIGHTER,
  FIGHTER_ACTION,
  CHECK_FOR_VICTORY,
  NEXT_TURN
}
public class CombatManager : MonoBehaviour
{
   public List<Fighter> fighters = new List<Fighter>() ;
   private int fighterIndex;
   public DiaNoche sol;
   public Camera cam;
   private bool isCombatActive;
   private CombatStatus combatStatus;
   private Skill currentFighterSkill;
   private int turn;
  //  private bool waitImput = false, selectPosition = false;
  //  public static event System.Action InputReceived;
  //   private bool inputReceived = false;

   
   public GameObject panel;
   public GameObject animacionInicio;

   void Start(){
      turn = 1;
     setActivePanel(false);
     animacionInicio.SetActive(false);
     this.fighterIndex = 0;
     this.isCombatActive = false;
     
     
   }
  public bool getIsCombatActive(){
    return isCombatActive;
  }

 public void setActivePanel( bool option){

      panel.SetActive(option);
    }

   public void prepareCombat(){
      isCombatActive = true;
     // Time.timeScale = 0f;
      setActivePanel(true);
      PanelBatalla.Write("Inciar batalla");
      animacionInicio.SetActive(true);
      var anim = animacionInicio.GetComponent<Animator>();
      anim.Play("combateInicio",-1, 0f);

     // Debug.Log("prepare combat...");
      foreach( var fgtr in this.fighters){
        fgtr.combatManager = this;
      }
      Debug.Log(fighters[0]);
      Debug.Log(fighters[1]);
      cam.transform.position = (fighters[0].transform.position+fighters[1].transform.position)/2;
      Vector3 newPos =new Vector3(cam.transform.position.x,200,cam.transform.position.z);
      cam.transform.position = newPos;
      //cam.transform.Translate(new Vector3(0, 300, 0));
      // cam.transform.position.y = 200;
      combatStatus = CombatStatus.NEXT_TURN;
      fighterIndex = -1;
      sol.velTime = 0;
      // animacionInicio.SetActive(false);
      StartCoroutine(this.CombatLoop());
      
    }
   IEnumerator CombatLoop(){
yield return new WaitForSeconds(5f);
        while(isCombatActive){
        //  Debug.Log("estoy aqui");
         //
         setActivePanel(true);
         switch (combatStatus){
          case CombatStatus.WAITING_FOR_FIGHTER:
            yield return null;
            break;
          case CombatStatus.FIGHTER_ACTION:
            PanelBatalla.Write(($"{fighters[fighterIndex].idName} usa {currentFighterSkill.skillName}."));
            yield return null;
            currentFighterSkill.Run();
            // if(currentFighterSkill.skillName=="Moverse"){
            //   Debug.Log("estamos en moverse");
            //   // Time.timeScale = 0f;
            //   Debug.Log("estamos en moverse3333333333");
            //   }
            combatStatus= CombatStatus.CHECK_FOR_VICTORY;
            
            
               yield return new WaitForSeconds(currentFighterSkill.animationDuration  +5f);
              // Debug.Log("Tiempo que ttarda de ir de a a b en combat: "+currentFighterSkill.animationDuration);
             // currentFighterSkill.disableFighters(); // = false;
             currentFighterSkill = null;
           
            
            break;
          case CombatStatus.CHECK_FOR_VICTORY:
            foreach(var fgtr in fighters){
              if(fgtr.isAlive() == false){
                isCombatActive = false;
                //fighters.Clear();
                sol.velTime = 60;
                PanelBatalla.Write("Combate terminado");
              }
              else{
                combatStatus = CombatStatus.NEXT_TURN;
              }

            }
            if(isCombatActive == false){
              fighters.Clear();
            }
            yield  return null;
            break;
          case CombatStatus.NEXT_TURN:
            yield return new WaitForSeconds(1f);
            fighterIndex = (fighterIndex +1 )% fighters.Count;
            var currentTurn = this.fighters[this.fighterIndex];
            PanelBatalla.Write($"{currentTurn.idName} tiene el turno");
            currentTurn.InitTurn();
            //fighterIndex = (fighterIndex +1 )% fighters.Count;
            turn ++;
            combatStatus = CombatStatus.WAITING_FOR_FIGHTER;
            break;
         }
            //yield return new WaitForSeconds(2f);

            // var currentTurn = this.fighters[this.fighterIndex];
            // PanelBatalla.Write($"{currentTurn.idName} tiene el turno");
            // currentTurn.InitTurn();
            // fighterIndex = (fighterIndex +1 )% fighters.Count;
            // turn ++;
        }
   
   }

  public void addFighter(Fighter luchador){
    if(fighters.Contains(luchador)){
      // Debug.Log("ya esta en al lista");
    }
    else fighters.Add(luchador);
  }
   
  public bool getCombatActive(){
    return isCombatActive;
  }

  public Fighter GetOposingFighter(){
    if( fighterIndex == 0) return fighters[1];
    else return fighters[0];
  }

  public void OnFighterSkill(Skill skill){
    currentFighterSkill = skill;
    combatStatus = CombatStatus.FIGHTER_ACTION;
  }

  //  private void Update()
  //   {
  //     if (Input.GetMouseButton(0) && inputReceived)
  //       {
  //         Debug.Log("segfundo click");
  //           selectPosition = true;
  //           InputReceived?.Invoke();
  //           //hacer el run del movimento y que no vuelva hasta terminar todo el movimiento.
  //       }
  //        else if (Input.GetMouseButton(0) && !inputReceived)
  //       {
  //           inputReceived = true;
  //           //InputReceived?.Invoke();
  //       }
        
  //   }

   }
