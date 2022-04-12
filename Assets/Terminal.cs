using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Terminal : MonoBehaviour
{
    [SerializeField] [Range(0.001f, 1f)] private float typingDelay = 1f;
    public List<Command> commands;
    [SerializeField] private TextMeshProUGUI terminalContentText;
    [SerializeField] private Image cursor;
    
    
    [SerializeField] [EventRef] private string typingSound;
    [SerializeField] [EventRef] private string returnSound;
    public ScrollRect scrollRect;
    public Scrollbar verticalScrollbar;
    private string _helpCommand = "help";
    private string _command;
    private string _argument;
    private List<string> _commandHistory;
    private int _historyCount = 0;
    private string _typedCommand = "";
    private AudioSource _audioSource;
    private Autocomplete _tree;
    private string _currentCommand = "";
    private int _parameterIndex = 0;
    [HideInInspector]
    public bool hasRootAccess = false;
    public bool hasUserAccess = false;
    private bool _isTyping = false;
    private FMOD.Studio.EventInstance typingSoundInstance;
    private FMOD.Studio.EventInstance returnSoundInstance;

    private string _startText;
    
    private struct ContentText
    {
        public string content;
        public bool hasTyping;
        public float delaySpeed;
    }
    
    
    private void Start()
    {
        _commandHistory = new List<string>();
        _audioSource = GetComponent<AudioSource>();
        _startText = terminalContentText.text;
        terminalContentText.text = "";
        DisplayResults(_startText);
        //terminalContentText.text += GetPreCommandString;
        hasRootAccess = true;
        hasUserAccess = true;
        
        typingSoundInstance = FMODUnity.RuntimeManager.CreateInstance(typingSound);
        returnSoundInstance = FMODUnity.RuntimeManager.CreateInstance(returnSound);
    }

    public string GetPreCommandString => $"<color={GameController.Instance.settings.Color4}>{GameController.Instance.currentName}</color>@<color={GameController.Instance.settings.Color4}>{GameController.Instance.currentHost.hostName}</color>:{GameController.Instance.currentDirectory.GetFullPath()}>";

    public string GetCommandFromHistory(bool forward)
    {
        string result = "";
        if (forward)
            ++_historyCount;
        else
            --_historyCount;
        _historyCount = Mathf.Clamp(_historyCount, 0, _commandHistory.Count-1);
        result = _commandHistory[_commandHistory.Count - 1 - _historyCount];
        return result;
    }
    
    public void Execute(string cmd, string parameter, string terminal)
    {
        _historyCount = -1;
        _commandHistory.Add(cmd);
  
        var words = cmd.Split(' ');
        
        if (words[0] == _helpCommand && words.Length==1)
        {
            string helpDescription = "";
            foreach (var command in commands)
            {
                helpDescription += $"\n<b><color=#9ad0d2>{command.name}</color></b>: {command.helpDescription}";
            }
            DisplayResults($"{terminal}\n{helpDescription}\n");
            return;
        }


        var currentCmd = "";
        
        
        if (_parameterIndex==0)
            currentCmd = words[0];
        else
            currentCmd = _currentCommand.Split(' ')[0];

        foreach (var command in commands)
        {

            if((currentCmd == command.name))
            {
                if (words.Length == 1)
                {
                    command.Use(terminal, "",_parameterIndex++,parameter);
                    return;
                }
                command.Use(terminal, words[1],_parameterIndex++,parameter);
                return;
            }
        }
        
        foreach (var command in GameController.Instance.currentHost.commands)
        {
            if (hasUserAccess)
            {
                if((currentCmd == command.name))
                {
                    if (words.Length == 1)
                    {
                        command.Use(terminal, "",_parameterIndex++,parameter);
                        return;
                    }
                    command.Use(terminal, words[1],_parameterIndex++,parameter);
                    return;
                }
            }
    
        }
        
        
        DisplayResults($"{terminal}\nCommand not found"); 
    }
    
    private void Update()
    {

        if (_isTyping) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            HandleHistoryCommands(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            HandleHistoryCommands(false);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            HandleAutoComplete();
        }

        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (_typedCommand.Length != 0)
                {
                    terminalContentText.text = terminalContentText.text.Remove(terminalContentText.text.Length - 1, 1);
                    _typedCommand = _typedCommand.Remove(_typedCommand.Length - 1, 1);
                }
                
       
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
  
                if (_currentCommand == "")
                {
                    _currentCommand = _typedCommand;
   
                    Execute(_typedCommand,"", terminalContentText.text);
                    _typedCommand = "";
                    returnSoundInstance.start();
                }
                else
                {
                    Execute(_currentCommand,_typedCommand, terminalContentText.text);
                    _typedCommand = "";
                    returnSoundInstance.start();
                }
            }
            else
            {
                terminalContentText.text += c;
                _typedCommand += c;
 
                Canvas.ForceUpdateCanvases();
                scrollRect.verticalNormalizedPosition = 0f;
                typingSoundInstance.start();
            }
        }


        UpdateCursor();
        

    }

    public void SetCursorToEndOfMessage(string message)
    {
        terminalContentText.text += message;
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        UpdateCursor();
    }

    public void DisplayResults(string result, bool withTerminal = false)
    {
        if (withTerminal)
        {
            result = $"{terminalContentText.text}\n" + result;
        }
        
        Regex regex = new Regex(@"~(\d(\.\d\d?)?)\s((.|\n)*?)~");
        var matches = regex.Matches(result);

        var results = new ContentText[matches.Count*2+1];
        var startIndexToSplit = 0;

        var i = 0;
      
        foreach (Match match in matches)
        {
            if (match.Success)
            {
                // Part 4: get the Group value and display it.
                var typingDelayCurrent = float.Parse(match.Groups[1].Value);
                var content = match.Groups[3].Value;
                var index = match.Index;

                var lengthToCut = index - startIndexToSplit;
                results[i].content = result.Substring(startIndexToSplit, lengthToCut);
                results[i].hasTyping = false;
                results[i].delaySpeed = 0f;
                i++;
                
                results[i].content = content;
                results[i].hasTyping = true;
                results[i].delaySpeed = typingDelayCurrent;
                i++;
                
                startIndexToSplit = index + match.Value.Length;
            }
        }
        
        var lengthToCutLast = result.Length - startIndexToSplit;
        results[i].content = result.Substring(startIndexToSplit, lengthToCutLast);
        results[i].hasTyping = false;
        results[i].delaySpeed = 0f;

        terminalContentText.text = "";
        if(gameObject.activeSelf)
            StartCoroutine(TypingTextAnimation(results));
    }

    private void UpdateCursor()
    {
        if (terminalContentText.text.Length > 0)
        {
            var pos = GetLastCharacterPosition();
            cursor.rectTransform.position = new Vector3(pos.x + 17, pos.y);
        }

    }

    public Vector3 GetLastCharacterPosition()
    {
        terminalContentText.ForceMeshUpdate(true);
        Vector3 ok = terminalContentText.textInfo.characterInfo[terminalContentText.textInfo.characterCount-1].bottomLeft;
        var worldCharacterLocation = terminalContentText.transform.TransformPoint(ok);
        return new Vector3(worldCharacterLocation.x , worldCharacterLocation.y);
    }

    private void HandleHistoryCommands(bool forward)
    {
        var result = GetCommandFromHistory(forward);
            
        terminalContentText.text = terminalContentText.text.Remove(terminalContentText.text.Length - _typedCommand.Length, _typedCommand.Length);
        _typedCommand = _typedCommand.Remove(_typedCommand.Length - _typedCommand.Length, _typedCommand.Length);

        terminalContentText.text += result;
        _typedCommand += result;
        
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        typingSoundInstance.start();
    }

    private void HandleAutoComplete()
    {
        int index = 0;
        string[] autoCompletionWords = new string[GameController.Instance.currentDirectory.files.Count+GameController.Instance.currentDirectory.directories.Count];
        
        foreach (var file in GameController.Instance.currentDirectory.files)
        {
            autoCompletionWords[index++] = file.name;
        }
        foreach (var dir in GameController.Instance.currentDirectory.directories)
        {
            autoCompletionWords[index++] = dir.path;
        }

        _tree = new Autocomplete(autoCompletionWords);

        if (_typedCommand.Length > 0)
        {
            string[] words = _typedCommand.Split(' ');
            var toAutoComplete = "";
            if (words.Length > 1)
            {
                toAutoComplete = words[1];

            }
            else
            {
                toAutoComplete = words[0];
            }
            
            var completionWords = _tree.GetWordsForPrefix(toAutoComplete);

            if (completionWords.Count > 0)
            {
                if (words.Length > 1)
                {
                    completionWords[0] = words[0] + " " + completionWords[0];
                }

      
                terminalContentText.text = terminalContentText.text.Remove(terminalContentText.text.Length - _typedCommand.Length, _typedCommand.Length);
                _typedCommand = _typedCommand.Remove(_typedCommand.Length - _typedCommand.Length, _typedCommand.Length);

            
                terminalContentText.text += completionWords[0];
                _typedCommand += completionWords[0];
                Canvas.ForceUpdateCanvases();
                scrollRect.verticalNormalizedPosition = 0f;
                typingSoundInstance.start();
            }
        }
          
    }
    
    IEnumerator TypingTextAnimation(ContentText[] texts)
    {
        _isTyping = true;
        foreach (var text in texts)
        {
 
            if (text.hasTyping)
            {
                foreach (var c in text.content)
                {
                    terminalContentText.text += c;
                    UpdateCursor();
                    Canvas.ForceUpdateCanvases();
                    scrollRect.verticalNormalizedPosition = 0f;
                    yield return new WaitForSeconds(text.delaySpeed * 0.01f);
                }
            }
            else
            {
                terminalContentText.text += text.content;
                UpdateCursor();
                Canvas.ForceUpdateCanvases();
                scrollRect.verticalNormalizedPosition = 0f;
                _currentCommand = "";
                _parameterIndex = 0;
            }
        }

        terminalContentText.text += $"\n{GetPreCommandString}";
        UpdateCursor();
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        _isTyping = false;
    }

}
