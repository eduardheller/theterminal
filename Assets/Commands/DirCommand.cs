using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Dir")]
public class DirCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        
        var files = GameController.Instance.currentDirectory.files;
        var directories = GameController.Instance.currentDirectory.directories;
        
        var result = "\n\n";

        if (string.IsNullOrEmpty(argument))
        {
            foreach (var file in files)
            {
                if(file!=null)
                    result += $"<FILE>  {file.name}\n";
            }
        
            foreach (var dirs in directories)
            {
                if(dirs!=null)
                    result += $"<DIR>   {dirs.path}\n";
            }
        
            if(GameController.Instance.currentDirectory.previousDirectory!=null) 
                result += $"<DIR>   ..\n";
            
            GameController.Instance.terminal.DisplayResults(terminal + result);
            return;
        }
        GameController.Instance.terminal.DisplayResults($"{terminal}\nArguments not allowed here");
    }
}


