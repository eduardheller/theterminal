using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisableRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().raycastTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
