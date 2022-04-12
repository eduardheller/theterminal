using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public abstract class HackMethod : ScriptableObject
{
    public string regEx;


    public event Action onMethodStart;
    public event Action onMethodEnd;
    
    public abstract bool CheckMethod(string idName,string code, GameObject obj);
    public abstract void Execute(string idName,string code, GameObject obj);
    
    public abstract IEnumerator OnUpdate(GameObject obj);

    protected virtual void OnMethodStart()
    {
        onMethodStart?.Invoke();
    }
    
    protected virtual void OnMethodEnd()
    {
        onMethodEnd?.Invoke();
    }
}
