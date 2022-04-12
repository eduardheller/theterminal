using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Command/Buy")]
public class BuyCommand : Command
{
    
    private string user;
    private string id;
    private string localBank;
    
    protected override void Execute(string terminal, string argument, int parameterIndex, string parameter)
    {
        var result = "\n\n";
        
        int resultArgument;
        bool hasFound = false;
        UpgradeFile buyItem = null;
        if (int.TryParse(argument, out resultArgument))
        {

            var shop = GameController.Instance.currentHost.shop;
            foreach (var area in shop.items)
            {
                foreach (var item in area.items)
                {
                    if (item.buyId.Equals(resultArgument))
                    {
                        buyItem = item;
                        hasFound = true;
                    }
                }
            }

            if (!hasFound)
            {
                result += $"BuyId does not exist in this shop\n";
                GameController.Instance.terminal.DisplayResults(terminal + result);
                return;
            }


            if (parameterIndex == 0)
            {
                GameController.Instance.terminal.SetCursorToEndOfMessage(
                    "\n\nShould we use your local bank data? yes(y) or no(n): ");
                return;
            }

            localBank = parameter;
            if (localBank.Equals("y"))
            {
                result += $"Checking bank account data ~0.3 ....~ \n";
                if (GameController.Instance.balance >= buyItem.cost)
                {
                    result += $"Buying item {buyItem.buyId}: {buyItem.name} ~0.3 ....~ Done!\n";
                    result += $"Installing system file: {buyItem.name} ~0.3 ....~ Done!\n";
                    result += $"Feature enabled.\n";
                    GameController.Instance.terminal.DisplayResults(terminal + result);

                    FMODUnity.RuntimeManager.PlayOneShot(clip[0]);

                    buyItem.HasBeenDownloaded();
                    hasFound = true;
                    return;
                }
                result += $"Not enough Balance!\n";
                GameController.Instance.terminal.DisplayResults(terminal + result);
                return;
            }
            
            if (localBank.Equals("n"))
            {
                if (parameterIndex == 1)
                {
                    GameController.Instance.terminal.SetCursorToEndOfMessage("\n\nBank user: ");
                    return;
                }

                if (parameterIndex == 2)
                {
                    user = parameter;
                    GameController.Instance.terminal.SetCursorToEndOfMessage("\n\nBank ID: ");
                    return;
                }

                id = parameter;


                result += $"Checking bank account data ~0.3 ....~ \n";

                if (user != "" && id != "")
                {
                    if (user.Equals("Dave_Graham") && id.Equals("22-333-144-13"))
                    {
                        if (GameController.Instance.balance >= buyItem.cost)
                        {
                            result += $"Buying item {buyItem.buyId}: {buyItem.name} ~0.3 ....~ Done!\n";
                            result += $"Installing system file: {buyItem.name} ~0.3 ....~ Done!\n";
                            result += $"Feature enabled.\n";
                            GameController.Instance.terminal.DisplayResults(terminal + result);

                            FMODUnity.RuntimeManager.PlayOneShot(clip[0]);

                            buyItem.HasBeenDownloaded();
                            hasFound = true;
                            return;
                        }
                        result += $"Not enough Balance!\n";
                        GameController.Instance.terminal.DisplayResults(terminal + result);
                        return;

                    }
                    result += $"Wrong bank account\n";
                }
            }
        }
        else
        {
            result += $"Wrong Buy ID\n";
        }
        GameController.Instance.terminal.DisplayResults(terminal + result);
  
    }
}