using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Commands/Connect")]
public class ConnectCommand : Command
{
    
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        var result = "\n\n";
        var found = false;

        foreach (var host in GameController.Instance.hosts)
        {
            if (!string.IsNullOrEmpty(host.ipAddress) && host.ipAddress.Equals(argument) || !string.IsNullOrEmpty(host.dns) && host.dns.Equals(argument))
            {
                GameController.Instance.currentHost = host;
                FMODUnity.RuntimeManager.PlayOneShot(clip[0]);
                
                var logo = GameController.Instance.currentHost.logo;
                
                if (logo != null)
                {
                    GameController.Instance.logo.gameObject.SetActive(true);
                    GameController.Instance.logo.sprite = logo;
                    var pos = GameController.Instance.terminal.GetLastCharacterPosition();
                    GameController.Instance.logo.rectTransform.position = new Vector3(pos.x - 400, pos.y);
                    result += $"\n\n\n\n\n\n";
                }
                
                result += $"Connecting to {argument} ~1.0 .....~\n";
                result += $"Connected to {host.hostName}. Welcome!\n";


                if (host.dontNeedLogin)
                {
                    GameController.Instance.terminal.hasUserAccess = true;
                    GameController.Instance.currentName = host.users[0].user;
                    GameController.Instance.currentDirectory = host.users[0].rootDirectory;
                }
                else
                {
                    result += $"Type <color=\"yellow\">login</color> to login.\n\n";
                }
                
                result += $"{host.welcomeMessage}\n";
                
                GameController.Instance.terminal.hasRootAccess = false;
                found = true;
                break;
            }
        }

        if (!found)
        {
            FMODUnity.RuntimeManager.PlayOneShot(clip[0]);
            result += $"Connecting to {argument} ~1.0 .......~\n";
            result += $"Can't resolve host!\n";
        }

        
        GameController.Instance.terminal.DisplayResults(terminal + result);

    }
}