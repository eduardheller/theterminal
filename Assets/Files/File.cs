using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/File/File")]
public class File : ScriptableObject
{
    public bool notDeleteAble;
    [EventRef] public string[] snd;
        
    [TextArea(15,1000)]
    public string content;

    public virtual void HasBeenDownloaded()
    {
        return;
    }
}
