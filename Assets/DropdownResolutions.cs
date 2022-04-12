using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownResolutions : MonoBehaviour
{
    List<Resolution> resolutions;
    public TMP_Dropdown dropdownMenu;
    void Start()
    {
        resolutions =  GetResolutions();
        dropdownMenu.onValueChanged.AddListener(delegate
        {
            Screen.SetResolution(resolutions[resolutions.Count-1 - dropdownMenu.value].width, resolutions[resolutions.Count-1 - dropdownMenu.value].height,  Screen.fullScreenMode);
        });
        int j = 0;
        for (int i = resolutions.Count-1; i >= 0; i--)
        {
            dropdownMenu.options[j].text = ResToString(resolutions[i]);
            dropdownMenu.value = i;
            dropdownMenu.options.Add(new TMP_Dropdown.OptionData(dropdownMenu.options[j].text));
            j++;
        }

        dropdownMenu.value = 0;
    }
    

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height + " " + res.refreshRate+ "hz";
    }
    
    public static List<Resolution> GetResolutions() {
        //Filters out all resolutions with low refresh rate:
        Resolution[] resolutions = Screen.resolutions;
        HashSet<Tuple<int, int>> uniqResolutions = new HashSet<Tuple<int, int>>();
        Dictionary<Tuple<int, int>, int> maxRefreshRates = new Dictionary<Tuple<int, int>, int>();
        for (int i = 0; i < resolutions.GetLength(0); i++) {
            //Add resolutions (if they are not already contained)
            Tuple<int, int> resolution = new Tuple<int, int>(resolutions[i].width, resolutions[i].height);
            uniqResolutions.Add(resolution);
            //Get highest framerate:
            if (!maxRefreshRates.ContainsKey(resolution)) {
                maxRefreshRates.Add(resolution, resolutions[i].refreshRate);
            } else {
                maxRefreshRates[resolution] = resolutions[i].refreshRate;
            }
        }
        //Build resolution list:
        List<Resolution> uniqResolutionsList = new List<Resolution>(uniqResolutions.Count);
        foreach (Tuple<int, int> resolution in uniqResolutions) {
            Resolution newResolution = new Resolution();
            newResolution.width = resolution.Item1;
            newResolution.height = resolution.Item2;
            if(maxRefreshRates.TryGetValue(resolution, out int refreshRate)) {
                newResolution.refreshRate = refreshRate;
            }
            uniqResolutionsList.Add(newResolution);
        }
        return uniqResolutionsList;
    }
}

