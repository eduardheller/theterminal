using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Command/Balance")]
public class BalanceCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        
        var result = "\n";

        if (!string.IsNullOrEmpty(argument))
        {
            GameController.Instance.terminal.DisplayResults($"{terminal}\nArguments not allowed here");
            return;
        }

        result += $"Getting balance from your bank@22.11.24.22 ~0.3 ...~\n";
        result += $"<b><color={settings.Color1}>Bank user</color></b>: Dave_Graham\n";
        result += $"<b><color={settings.Color1}>Bank ID</color></b>: 22-333-144-13\n";
        result += $"<b><color={settings.Color1}>Balance</color></b>: <b><color={settings.Color2}>{GameController.Instance.balance}c </color></b>\n";
 
        GameController.Instance.terminal.DisplayResults($"{terminal}\n{result}");

    }
}