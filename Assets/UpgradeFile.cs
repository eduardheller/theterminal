using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Hacking/File/Upgrade")]
public class UpgradeFile : File
{
    public int buyId;
    public int cost;
    [TextArea]
    public string buyDescription;
    public int neededRam;
    public int neededHdd;
    public UpgradeFile[] requiredUpgrades;
    
    public override void HasBeenDownloaded()
    {
        GameController.Instance.CreateDistortEffect(4.0f);
        var folder = GameController.Instance.hosts[0].root.rootDirectory.directories[0];
        folder.files.Add(this);
    }
    
}
