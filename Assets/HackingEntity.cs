using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using Miniscript;
using UnityEditor;

public class HackingEntity : MonoBehaviour
{
    public string idName;
    public HackMethod[] hackMethods;
    public TextAsset initialSourceCode;
    public UIHacking hackInterface;
    public Interpreter interpreter;

    private string _entitySourceCode;
    private void Awake()
    {
        interpreter = new Interpreter();
        interpreter.standardOutput = (string s) => hackInterface.statusCode.text = s;
        interpreter.implicitOutput = (string s) => hackInterface.statusCode.text = $"<color=\"green\">{s}</color>";
        interpreter.errorOutput = (string s) =>
        {
            hackInterface.statusCode.text = $"<color=\"red\">{s}</color>";
            interpreter.Stop();
        };

        _entitySourceCode = initialSourceCode.text;

    }


    public void Compile()
    {
        interpreter.Reset(hackInterface.hackCode.source);
        interpreter.Compile();
        
        
        if (GetComponent<Renderer>().material.color == Color.blue)
        {
            interpreter.SetGlobalValue("color", new ValString("blue"));
        }
        else  if (GetComponent<Renderer>().material.color == Color.red)
        {
            interpreter.SetGlobalValue("color", new ValString("red"));
        }
        else  if (GetComponent<Renderer>().material.color == Color.green)
        {
            interpreter.SetGlobalValue("color", new ValString("green"));
        }
        else  if (GetComponent<Renderer>().material.color == Color.yellow)
        {
            interpreter.SetGlobalValue("color", new ValString("yellow"));
        }
        else
        {
            interpreter.SetGlobalValue("color", new ValString(GetComponent<Renderer>().material.color.ToString()));
        }
        
        
        var setColorFnc = Intrinsic.Create("setColor");
        setColorFnc.AddParam("color", "red");
        setColorFnc.code = (context, partialResult) =>
        {
            if (context.GetVar("color").ToString() == "blue")
            {
                GetComponent<Renderer>().material.color = Color.blue;
                interpreter.SetGlobalValue("color", new ValString("blue"));
                return new Intrinsic.Result(1);
            }
            else if (context.GetVar("color").ToString() == "red")
            {
                GetComponent<Renderer>().material.color = Color.red;
                interpreter.SetGlobalValue("color", new ValString("red"));
                return new Intrinsic.Result(1);
            }
            else if (context.GetVar("color").ToString() == "green")
            {
                GetComponent<Renderer>().material.color = Color.green;
                interpreter.SetGlobalValue("color", new ValString("green"));
                return new Intrinsic.Result(1);
            }
            else if (context.GetVar("color").ToString() == "yellow")
            {
                GetComponent<Renderer>().material.color = Color.yellow;
                interpreter.SetGlobalValue("color", new ValString("yellow"));
                return new Intrinsic.Result(1);
            }
            
            return new Intrinsic.Result(0);
        };
        
        try
        {
            interpreter.RunUntilDone(0.01);
        }
        catch (MiniscriptException e)
        {
            Debug.Log("Script error: " + e.Description());
        }
    }
    
    public void ResetCode()
    {
        
    }
    
    public void StopTerminal()
    {
        _entitySourceCode = hackInterface.hackCode.source;
        hackInterface.compileBtn.onClick.RemoveListener(Compile);
        hackInterface.resetBtn.onClick.RemoveListener(Compile);
    }
    
    public void StartTerminal()
    {
        hackInterface.hackCode.source = _entitySourceCode;
        hackInterface.compileBtn.onClick.AddListener(Compile);
        hackInterface.resetBtn.onClick.AddListener(Compile);
    }
    
}
