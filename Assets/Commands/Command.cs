using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Command : ScriptableObject
{
    public string name;
    public bool needsRoot;
    public bool needsUser;
    [TextArea]
    public string helpDescription;

    [EventRef]
    public string[] clip;
    
    public List<string> parameters = new List<string>();

    [HideInInspector] public Settings settings;
    
    public void Use(string terminal, string argument, int parameterIndex, string parameter)
    {
        var hasRoot = GameController.Instance.terminal.hasRootAccess;
        var userAccess = GameController.Instance.terminal.hasUserAccess;
        settings = GameController.Instance.settings;
        if (!userAccess && needsUser)
        {
            GameController.Instance.terminal.DisplayResults($"{terminal}\nThis command is restricted because you have not the rights");
            return;
        }
        if (!hasRoot && needsRoot)
        {
            GameController.Instance.terminal.DisplayResults($"{terminal}\nThis command is restricted because you have not the rights");
            return;
        }

        Execute(terminal, argument, parameterIndex, parameter);
        

    }
    protected abstract void Execute(string terminal, string argument, int parameterIndex, string parameter);
}
