using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Command/Exit")]
public class ExitCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        if (string.IsNullOrEmpty(argument))
        {
            Application.Quit();
            return;
        }
        GameController.Instance.terminal.DisplayResults($"{terminal}\nArguments not allowed here");
    }
}