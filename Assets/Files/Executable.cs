using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
[CreateAssetMenu(menuName = "Hacking/File/Executable")]
public class Executable : File
{
    public Command cmdToAdd;

    public override void HasBeenDownloaded()
    {
        var folder = GameController.Instance.hosts[0].root.rootDirectory.directories[0];
        folder.files.Add(this);
        if(cmdToAdd!=null)
            GameController.Instance.terminal.commands.Add(cmdToAdd);
    }
}
