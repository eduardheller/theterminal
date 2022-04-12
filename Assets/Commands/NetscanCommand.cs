using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Command/Netscan")]
public class NetscanCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        
        var result = "\n";

        if (!string.IsNullOrEmpty(argument))
        {
            GameController.Instance.terminal.DisplayResults($"{terminal}\nArguments not allowed here");
            return;
        }

        result += $"Checking connected computers in this network~0.2 ...~\n";
        
        foreach (var host in GameController.Instance.currentHost.networkHosts)
        {
            result += $"~0.2 ...~ Found {host.dns} with IP: {host.ipAddress}\n";
        }
        GameController.Instance.terminal.DisplayResults($"{terminal}\n{result}");

    }
}