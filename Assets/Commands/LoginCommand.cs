using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Commands/Login")]
public class LoginCommand : Command
{
    private string user;
    private string password;
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        var result = "\n\n";

        if (parameterIndex == 0)
        {
            GameController.Instance.terminal.SetCursorToEndOfMessage("\n\nUsername: ");
            return;
        }

        if (parameterIndex == 1)
        {
            user = parameter;
            GameController.Instance.terminal.SetCursorToEndOfMessage("\nPassword: ");
            return;
        }

        password = parameter;

        if (user != "" && password != "")
        {
            if (user == GameController.Instance.currentHost.root.user)
            {
                if (password == GameController.Instance.currentHost.root.password)
                {
                    GameController.Instance.terminal.hasRootAccess = true;
                    GameController.Instance.terminal.hasUserAccess = true;
                    GameController.Instance.currentName = user;
                    GameController.Instance.currentDirectory = GameController.Instance.currentHost.root.rootDirectory;
                    GameController.Instance.terminal.DisplayResults($"{terminal}\n\nLogin successful as {user} with root rights\n");
                    FMODUnity.RuntimeManager.PlayOneShot(clip[0]);
                    user = "";
                    password = "";
                    return;
                }
            }
            else 
            {
                foreach (var userInServer in GameController.Instance.currentHost.users)
                {
                    if (user == userInServer.user)
                    {
                        if (password == userInServer.password)
                        {
                            GameController.Instance.terminal.hasUserAccess = true;
                            GameController.Instance.currentName = userInServer.user;
                            GameController.Instance.currentDirectory = userInServer.rootDirectory;
                            GameController.Instance.terminal.DisplayResults($"{terminal}\n\nLogin successful as {user}\n");
                            FMODUnity.RuntimeManager.PlayOneShot(clip[0]);

                            user = "";
                            password = "";
                            return;
                        }
                    }
    
                }
            }
            GameController.Instance.terminal.DisplayResults($"{terminal}\nLogin failed!");
        }
        
        

    }
}