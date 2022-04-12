using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SyntaxHighlighter : MonoBehaviour
{
    private TextMeshProUGUI _textUi;
    // Start is called before the first frame update

    private string[] _basicTokens = {"IF", "THEN", "ELSE"};
    private string[] _returnTokens = {"RETURN", "GOTO"};
   
    
    
    void Start()
    {
        _textUi = GetComponent<TextMeshProUGUI>();
        _textUi.GetTextInfo(_textUi.text);


        var offset = 0;
        for (int i = 0; i<_textUi.textInfo.wordCount; i++)
        {
            var word = _textUi.textInfo.wordInfo[i].GetWord();
            if(_basicTokens.Contains(word))
            {
                var rplString = $"<color={GameController.Instance.settings.Color1}>{word}</color>";
                _textUi.text = _textUi.text.ReplaceAt(_textUi.textInfo.wordInfo[i].firstCharacterIndex+offset, word.Length,
                    rplString);

                offset += rplString.Length - word.Length;
            }
            else if (_returnTokens.Contains(word))
            {
                var rplString = $"<color={GameController.Instance.settings.Color2}>{word}</color>";
                _textUi.text = _textUi.text.ReplaceAt(_textUi.textInfo.wordInfo[i].firstCharacterIndex+offset, word.Length,
                    rplString);

                offset += rplString.Length - word.Length;
            }
        }
    }
    

}
