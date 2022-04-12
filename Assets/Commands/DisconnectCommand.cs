using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Commands/Disconnect")]
public class DisconnectCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        var result = "\n\n";

        if (GameController.Instance.currentHost == GameController.Instance.hosts[0])
        {
            result += $"You are currently not connected to a remote computer\n";
            GameController.Instance.terminal.DisplayResults(terminal + result);
            return;
        }
        
        result += $"Disconnecting from {GameController.Instance.currentHost.hostName}\n";
        GameController.Instance.currentHost = GameController.Instance.hosts[0];
        GameController.Instance.currentDirectory = GameController.Instance.currentHost.root.rootDirectory;
        GameController.Instance.currentName = "Dave";
        GameController.Instance.terminal.hasRootAccess = true;
        GameController.Instance.terminal.hasUserAccess = true;
        GameController.Instance.terminal.DisplayResults(terminal + result);

    }
}