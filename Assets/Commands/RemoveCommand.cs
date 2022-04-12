using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Commands/Remove")]
public class RemoveCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        var files = GameController.Instance.currentDirectory.files;
        var result = "\n\n";

        if (argument.Equals("*"))
        {
            foreach (var file in files)
            {
                result += $"Deleting file {file.name} ~0.2 ...~ File deleted\n";
            }
            files.Clear();
            GameController.Instance.terminal.DisplayResults(terminal + result);
            return;
        }
        
        File fileToDelete = null;
        
        foreach (var file in files)
        {
            if (!argument.Equals(file.name)) continue;
            fileToDelete = file;
            break;
        }

        if (fileToDelete != null)
        {
            files.Remove(fileToDelete);
            GameController.Instance.terminal.DisplayResults(terminal + $"\n\nDeleting file {fileToDelete.name} ~0.2 ...~ File deleted\n");
        }
        else
        {
            GameController.Instance.terminal.DisplayResults(terminal + $"\n\n File {argument} does not exist\n");
        }

    }
}