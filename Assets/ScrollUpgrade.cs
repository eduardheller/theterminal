using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/File/Upgrade/ScrollUpgrade")]
public class ScrollUpgrade : UpgradeFile
{
    public override void HasBeenDownloaded()
    {
        base.HasBeenDownloaded();
        GameController.Instance.terminal.scrollRect.enabled = true;
        GameController.Instance.terminal.scrollRect.verticalScrollbar =
            GameController.Instance.terminal.verticalScrollbar;
        GameController.Instance.terminal.enabled = true;
    }
}

