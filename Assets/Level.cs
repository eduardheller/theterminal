using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Level : MonoBehaviour
{
    public DoorOpenMechanic[] doors;

    [EventRef]
    public string overFlowSound;
    [HideInInspector] public bool hasSolved;
    public Canvas canvasOverflow;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var obj in doors)
        {
            obj.RegisterLevel(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CheckDoors()
    {
        foreach(var obj in doors)
        {
            if (!obj.destroyed)
                return;
        }

        hasSolved = true;
        canvasOverflow.gameObject.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot(overFlowSound);
       
    }
}
