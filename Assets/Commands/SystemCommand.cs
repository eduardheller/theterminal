using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Commands/System")]
public class SystemCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        var result = $"\nCPU: 1mhz interstar 8080 16bit\nram: 64kb \nlocation: germany";
        GameController.Instance.terminal.DisplayResults($"{terminal}\n{result}\n");
    }
}