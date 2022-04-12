using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Cd")]
public class CdCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        var files = GameController.Instance.currentDirectory.files;
        var directories = GameController.Instance.currentDirectory.directories;
        
        var result = "\n\n";
        bool hasFound = false;
        
        if (argument.Equals(".."))
        {
            if(GameController.Instance.currentDirectory.previousDirectory != null)
                GameController.Instance.currentDirectory = GameController.Instance.currentDirectory.previousDirectory;
            hasFound = true;
        }
        else
        {
            foreach (var dirs in directories)
            {
                if (!argument.Equals(dirs.path)) continue;
                GameController.Instance.currentDirectory = dirs;
                hasFound = true;
                break;
            }
        }

        GameController.Instance.terminal.DisplayResults(!hasFound
            ? $"{terminal}\nThe directory {argument} does not exist"
            : $"{terminal}\n\n");
    }
}