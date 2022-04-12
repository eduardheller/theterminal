using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class DoorOpenMechanic : ConnectedObject
{
    
    private Level _level;
    [EventRef]
    public string explosionSound;
    public GameObject explosionEffect;
    
    [HideInInspector]
    public bool destroyed;
    // Start is called before the first frame update


    public void RegisterLevel(Level lvl)
    {
        _level = lvl;
    }


    public override void CheckEntity()
    {
        foreach(var obj in entitiesToActivate)
        {
            Debug.Log("WIEEEEE");
            if (!obj.hasSolved)
                return;
        }

        destroyed = true;
        FMODUnity.RuntimeManager.PlayOneShot(explosionSound, transform.position);
        Destroy(this.gameObject);
        explosionEffect.SetActive(true);
        _level.CheckDoors();
    }
}
