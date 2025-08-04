using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Text", menuName ="ScriptableObjects/UI/Text")]
public class TextSO : ScriptableObject
{
    public ThemeSO theme;

    public TMP_FontAsset font;
    public float size;

}
