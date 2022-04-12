using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Settings")]
public class Settings : ScriptableObject
{
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private Color color3;
    [SerializeField] private Color color4;


    public string Color1 => "#" + ColorUtility.ToHtmlStringRGB(color1);
    public string Color2 => "#" + ColorUtility.ToHtmlStringRGB(color2);
    public string Color3 => "#" + ColorUtility.ToHtmlStringRGB(color3);
    public string Color4 => "#" + ColorUtility.ToHtmlStringRGB(color4);

}
