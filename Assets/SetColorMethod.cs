using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(menuName = "Hacking/Method/SetColor")]
public class SetColorMethod : HackMethod
{
    public Color colorToSolve;
    private Color _targetColor;
    
    public override bool CheckMethod(string idName, string code, GameObject obj)
    {
        Regex regex = new Regex(idName + @".setColor\(\""((red|blue|green|yellow))\""\)");
        var match = regex.Match(code);
        return match.Success;
    }

    public override void Execute(string idName, string code, GameObject obj)
    {
        Regex regex = new Regex(idName + @".setColor\(\""((red|blue|green|yellow|black|gray|white|magenta))\""\)");
        var match = regex.Match(code);
        OnMethodStart();
        var res = match.Groups[1].Value;
        
        if (res.Equals("red"))
        {
            _targetColor = Color.red;
        }
        else if (res.Equals("blue"))
        {
            _targetColor = Color.blue;
        }
        else if (res.Equals("green"))
        {
            _targetColor = Color.green;
        }
        else if (res.Equals("yellow"))
        {
            _targetColor = Color.yellow;
        }
        else if (res.Equals("black"))
        {
            _targetColor = Color.black;
        }
        else if (res.Equals("gray"))
        {
            _targetColor = Color.gray;
        }
        else if (res.Equals("white"))
        {
            _targetColor = Color.white;
        }
        else if (res.Equals("magenta"))
        {
            _targetColor = Color.magenta;
        }
        GameController.Instance.StartCoroutine(OnUpdate(obj));
    }

    public override IEnumerator OnUpdate(GameObject obj)
    {
        var renderObj = obj.GetComponent<Renderer>();
        while (true)
        {
            if (Vector4.Distance(_targetColor, renderObj.material.color) > 0.1f)
            {
                renderObj.material.color = Vector4.Lerp(renderObj.material.color, _targetColor, Time.deltaTime);
            }
            else
            {
                renderObj.material.color = _targetColor;
                if(_targetColor==colorToSolve) {
                    obj.GetComponent<ConnectedObject>().hasSolved = true;
                }
                OnMethodEnd();
                break;
            }

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

}
