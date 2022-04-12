using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Hacking/Shop/Area")]
public class ShopArea : ScriptableObject
{
    public string name;
    public UpgradeFile[] items;
}
