using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text : CustomUIComponent
{
    public TextSO textSO;
    public EnumStyle style;

    private TextMeshProUGUI textMeshProUGUI;


    public override void Setup()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Configure()
    {
        textMeshProUGUI.color    = textSO.theme.GetTextColor(style);
        textMeshProUGUI.font     = textSO.font;
        textMeshProUGUI.fontSize = textSO.size;
        textMeshProUGUI.alignment = TextAlignmentOptions.Center;
    }

}
