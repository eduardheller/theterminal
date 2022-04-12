using System;
using System.Collections;
using System.Collections.Generic;
using Miniscript;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHacking : MonoBehaviour
{
    public CodeEditor taskCode;
    public CodeEditor hackCode;
    public Button compileBtn;
    public Button resetBtn;
    public TextMeshProUGUI statusCode;
    private bool _hasInitiated = false;
    public Camera mainCamera;
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        _hasInitiated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hasInitiated)
        {
            taskCode.enabled = false;
            _hasInitiated = true;
        }
        
        //transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);

    }
}