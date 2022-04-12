using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Cat")]
public class CatCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        var files = GameController.Instance.currentDirectory.files;
        var directories = GameController.Instance.currentDirectory.directories;
        
        var result = "\n";
        bool hasFound = false;

        if (string.IsNullOrEmpty(argument))
        {
            GameController.Instance.terminal.DisplayResults($"{terminal}\nNO FILE SPECIFIED");
            return;
        }
   
        foreach (var file in files)
        {
            if (!argument.Equals(file.name)) continue;
            foreach (var snd in file.snd)
            {
                if (snd!="")
                {
                    FMODUnity.RuntimeManager.PlayOneShot(snd);
                }
            }
       
            result += $"<font=\"LiberationSans SDF\"><size=80%>{file.content}<size=100%></font>";
            hasFound = true;
            break;
        }


        GameController.Instance.terminal.DisplayResults(!hasFound
            ? $"{terminal}\nFile {argument} does not exist."
            : $"{terminal}\n{result}\n");
    }
}