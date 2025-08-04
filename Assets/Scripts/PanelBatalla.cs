using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PanelBatalla : MonoBehaviour
{
    protected static PanelBatalla current;
    public TMP_Text logLabel;

    void Awake(){
        current = this;
    }

    public static void Write(string message){

        current.logLabel.text = message;
    }


}
