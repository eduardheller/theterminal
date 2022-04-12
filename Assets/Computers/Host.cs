using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Hacking/Host")]
public class Host : ScriptableObject
{
    public string hostName;
    public string ipAddress;
    public string dns;
    [TextArea] public string welcomeMessage;
    public User root;
    public User[] users;
    public Sprite logo;
    public Host[] networkHosts;
    public bool dontNeedLogin;
    public Command[] commands;
    public Shop shop;
    public GameObject hackLevel;
}
