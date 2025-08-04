using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickModels : MonoBehaviour
{
    //public CustomButton button;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if (EventSystem.current.IsPointerOverGameObject())
            //{
            //    Debug.Log("clicked on UI");
            //}
             if(LookForGameObject(out RaycastHit hit)) {
                hit.transform.gameObject.GetComponent<CustomButton>().OnClick();
            }
        }
    }

    private bool LookForGameObject(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Debug.Log(Physics.Raycast(ray, out hit));
        // Debug.Log(hit.transform.gameObject.name);

        return Physics.Raycast(ray, out hit);
    }
}
