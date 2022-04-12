using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLabel : MonoBehaviour
{
    public GameObject canvas;
    private void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
}
