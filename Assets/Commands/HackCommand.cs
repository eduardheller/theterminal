using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Hack")]
public class HackCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        var host = GameController.Instance.currentHost;

        if (host != GameController.Instance.hosts[0])
        {
            if (host.hackLevel != null)
            {
                GameController.Instance.HackLevel();
            }


            GameController.Instance.terminal.DisplayResults(
                    $"{terminal}\nStarting hack...");
        }
        else
        {
            GameController.Instance.terminal.DisplayResults(
                $"{terminal}\nCan't hack local terminal...");
        }

   
    }
}

