using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "View", menuName = "ScriptableObjects/UI/View")]
public class ViewSO : ScriptableObject
{
    public ThemeSO theme;
    public RectOffset padding;
    public float spacing;
}
