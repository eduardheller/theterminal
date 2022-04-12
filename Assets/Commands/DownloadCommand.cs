using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Command/Download")]
public class DownloadCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {

        if (string.IsNullOrEmpty(argument))
        {
            GameController.Instance.terminal.DisplayResults($"{terminal}\nYou have to specify an argument");
            return;
        }

        foreach (var file in GameController.Instance.currentDirectory.files)
        {
            if (file.name.Equals(argument))
            {
                var folder = GameController.Instance.hosts[0].root.rootDirectory.directories[0];
                GameController.Instance.terminal.DisplayResults($"{terminal}\n\nDownloading file to {folder.path} folder ~0.5 .....~ Complete!\n");
                file.HasBeenDownloaded();
                return;
            }
        }

        
    
    }
}