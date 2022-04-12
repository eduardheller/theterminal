using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Hacking/Directory")]
public class Directory : ScriptableObject
{
    public User user;
    public string path;  
    [SerializeField] private List<File> baseFiles;
    [SerializeField] private List<Directory> baseDirectories;
    public Directory previousDirectory;

    private List<Directory> _directories;
    
    public List<File> files { get; set; }
    public List<Directory> directories { get; set; }
    public string GetFullPath()
    {
        if (previousDirectory != null)
            return previousDirectory.GetFullPath() + path;
        return path;
    }

    public void OnEnable()
    {
        files = new List<File>(baseFiles);
        directories = new List<Directory>(baseDirectories);
    }
}
