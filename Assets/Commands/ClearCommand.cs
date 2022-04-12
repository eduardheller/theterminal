using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Clear")]
public class ClearCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        if (string.IsNullOrEmpty(argument))
        {
            GameController.Instance.terminal.DisplayResults( "");
            return;
        }
        GameController.Instance.terminal.DisplayResults($"{terminal}\nArguments not allowed here");
    }
}
