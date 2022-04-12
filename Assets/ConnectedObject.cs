using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FMODUnity;
using TMPro;
using UnityEngine;

public class ConnectedObject : HackEntity
{

    [EventRef] public string compileSound;
    [EventRef] public string errorSound;
    public HackMethod[] hackMethods;
    public Canvas canvasToShow;
    public Canvas canvasToMark;
    private bool _inputActive;
    private bool _inside;
    public string idName;
    public GameObject idInfo;
    [TextArea(15, 50)] public string code;
  
    [HideInInspector]
    public bool hasSolved;

    public List<ConnectedObject> entitiesToActivate = new List<ConnectedObject>();
    public void RegisterEntity(ConnectedObject entity)
    {
        entitiesToActivate.Add(entity);
    }

    private void Start()
    {
     
        foreach(var obj in entitiesToActivate)
        {
            obj.RegisterEntity(this);
        }

        foreach (var meth in hackMethods)
        {
            foreach (var entity in entitiesToActivate)
            {
                meth.onMethodEnd += delegate {
                    if (entity != null)
                    {
                        entity.CheckEntity();
                        //StartCoroutine(CineOff());
                    } 
                };
            }
        }
    }


    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if(!idInfo.activeSelf)
                idInfo.SetActive(true);
        }
        else
        {
            if(idInfo.activeSelf)
                idInfo.SetActive(false);
        }
        
        if (!_inside) return;
        
        if (Input.GetKeyUp(KeyCode.Return) && !_inputActive)
        {
            canvasToMark.gameObject.SetActive(false);
            canvasToShow.gameObject.SetActive(true);
            canvasToShow.GetComponent<RectTransform>().position = transform.position;
            canvasToShow.GetComponent<CodeBase>().inputField.Select();
            canvasToShow.GetComponent<CodeBase>().inputField.ActivateInputField();
            canvasToShow.GetComponent<CodeBase>().inputField.text = String.Format(code,idName);
            PlayerController.StopMove = true;
            _inputActive = true;
        }
    }

    private void OnCompileClicked()
    {
        if (_inputActive)
        {
            var result = ParseCode(OnCodeChange(canvasToShow.GetComponent<CodeBase>().inputField.textComponent));
            canvasToShow.gameObject.SetActive(false);
            canvasToMark.gameObject.SetActive(true);
            canvasToMark.GetComponent<RectTransform>().position = transform.position;
            PlayerController.StopMove = false;
            _inputActive = false;
            RuntimeManager.PlayOneShot(result == -1 ? compileSound : errorSound, transform.position);
        }
    }
    
    private void OnResetClicked()
    {
        if (_inputActive)
        {
            canvasToShow.GetComponent<CodeBase>().inputField.text = String.Format(code,idName);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        canvasToShow.GetComponent<CodeBase>().compileBtn.onClick.AddListener(OnCompileClicked);
        canvasToShow.GetComponent<CodeBase>().resetBtn.onClick.AddListener(OnResetClicked);
        canvasToMark.gameObject.SetActive(true);
        canvasToMark.GetComponent<RectTransform>().position = transform.position;
        _inside = true;
    }
    

    private void OnTriggerExit(Collider other)
    {
        canvasToShow.GetComponent<CodeBase>().compileBtn.onClick.RemoveListener(OnCompileClicked);
        canvasToShow.GetComponent<CodeBase>().resetBtn.onClick.RemoveListener(OnResetClicked);
        canvasToShow.gameObject.SetActive(false);
        canvasToMark.gameObject.SetActive(false);
        _inside = false;
    }
    
    private IEnumerable<string> OnCodeChange(TMP_Text code)
    {
        return EnumerateLines(code);
    }
    
    public int ParseCode(IEnumerable<string> lines)
    {
        int lineOfCode = -1;
        foreach (var code in lines)
        {
            foreach (var method in hackMethods)
            {
                lineOfCode++;
                if (method.CheckMethod(idName, code, gameObject))
                    continue;
                return lineOfCode;
            }
        }
        foreach (var code in lines)
        {
            foreach (var method in hackMethods)
            {
                method.Execute(idName, code, gameObject);
            }
        }
        return -1;
    }
    
    IEnumerable<string> EnumerateLines(TMP_Text text)
    {
        // We use GetTextInfo because .textInfo is not always valid
        TMP_TextInfo textInfo = text.GetTextInfo(text.text);
     
        for (int i = 0; i < textInfo.lineCount; i++)
        {
            TMP_LineInfo line = textInfo.lineInfo[i];
            yield return text.text.Substring(line.firstCharacterIndex, line.characterCount);
        }
    }


    public override void CheckEntity()
    {
        
    }
}
