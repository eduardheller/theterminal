using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Command/Shop")]
public class ShopCommand : Command
{
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        var result = "\n\n";


        var shop = GameController.Instance.currentHost.shop;
        if (string.IsNullOrEmpty(argument))
        {
  
            int i = 0;
            foreach (var area in shop.items)
            {
                result += $"<b><color={settings.Color1}>{i++}</color></b>: {area.name}\n";
            }
            result += $"\nType <b><color={settings.Color1}>shop <index></color></b> to go to the specific buy page.\n";
            GameController.Instance.terminal.DisplayResults(terminal + result);
            return;
        }

        int resultArgument;

        if (int.TryParse(argument, out resultArgument))
        {
            if (resultArgument < shop.items.Length && resultArgument>=0)
            {
                var area = shop.items[resultArgument];
                int i = 0;
                foreach (var item in area.items)
                {
                    result += $"<b><color={settings.Color1}>{item.buyId}</color></b>: <b><color={settings.Color2}>{item.name} ({item.cost}c)</color></b>: {item.buyDescription}\n";
                }
                
                GameController.Instance.terminal.DisplayResults(terminal + result);
                return;
            }
        }
        GameController.Instance.terminal.DisplayResults($"{terminal}\nArguments not allowed here");
    }
}