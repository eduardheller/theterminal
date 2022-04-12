using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/File/Upgrade/MouseUpgrade")]
public class MouseUpgrade : UpgradeFile
{
    public override void HasBeenDownloaded()
    {
        base.HasBeenDownloaded();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        GameController.Instance.hasMouse = true;
    }
}
