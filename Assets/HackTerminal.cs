using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class HackTerminal : MonoBehaviour
{
    public FPSController fpsController;
    public CinemachineVirtualCamera virtualTerminalCamera;
    public CinemachineVirtualCamera virtualHackingCamera;
    public UIHacking terminal;
        
    private bool _isAtTerminal;
    private bool _isUsingTerminal;

    private void Update()
    {
        if (_isAtTerminal)
        {
            if (Input.GetKeyUp(KeyCode.E) && !_isUsingTerminal)
            {
                //fpsController.enabled = false;
                //virtualTerminalCamera.Priority = 20;
                _isUsingTerminal = true;
                StartCoroutine(AtTerminal());
            }
            else if (Input.GetKeyUp(KeyCode.Escape) && _isUsingTerminal)
            {
                //virtualTerminalCamera.Priority = 20;
                //virtualHackingCamera.Priority = 10;
                terminal.gameObject.SetActive(false);
                StartCoroutine(ExitTerminal());
            }
        }

    }

    IEnumerator ExitTerminal()
    {
        GetComponent<HackingEntity>().StopTerminal();
        terminal.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        //virtualTerminalCamera.Priority = 10;
        //virtualHackingCamera.Priority = 10;
        yield return new WaitForSeconds(0.01f);
        //fpsController.enabled = true;
        _isUsingTerminal = false;
        
    }

    IEnumerator AtTerminal()
    {
        yield return new WaitForSeconds(0.01f);
        //virtualTerminalCamera.Priority = 10;
        //virtualHackingCamera.Priority = 20;
        terminal.gameObject.SetActive(true);
        GetComponent<HackingEntity>().StartTerminal();
    }

    private void OnTriggerStay(Collider other)
    {
        _isAtTerminal = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        _isAtTerminal = false;
    }
}
