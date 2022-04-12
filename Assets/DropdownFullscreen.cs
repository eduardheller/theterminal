using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownFullscreen : MonoBehaviour
{
    
    public TMP_Dropdown dropdownMenu;
    public static FullScreenMode CurrentScreenMode;
    
    public bool hasFullscreen;
    // Start is called before the first frame update
    void Start()
    {
        dropdownMenu.onValueChanged.AddListener(delegate
        {
            if (dropdownMenu.value == 0)
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            else if (dropdownMenu.value == 1)
                Screen.fullScreenMode = FullScreenMode.Windowed;
            else if (dropdownMenu.value == 2)
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        });
    }

    
}
