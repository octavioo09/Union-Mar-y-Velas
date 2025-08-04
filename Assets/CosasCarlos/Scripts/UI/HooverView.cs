using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HooverView : CustomUIComponent
{

    public ViewSO viewData;
    public GameObject containerCenter;
    private Image imageCenter;


    public override void Setup()
    {
        imageCenter = containerCenter.GetComponent<Image>();
    }

    public override void Configure()
    {
        imageCenter.color = viewData.theme.secondary_bg;
    }

}
