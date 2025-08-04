using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InputRow : MonoBehaviour
{
    private CustomButton button;
    public TMP_InputField inputField;
    [HideInInspector]
    public string text; 
    private void Awake()
    {
        //inputField = transform.Find("InputField").GetComponent<TMP_InputField>();
        //Debug.Log(inputField);
    }

    private void Start()
    {
        button = GetComponentInChildren<CustomButton>();
        button.onClick.AddListener(getText);
    }

    //public void getText(string inputString)
    //{
    //    inputField.text = inputString;
    //    text = inputString;
    //}

    public void getText()
    {
       text = inputField.text;
    }
}
