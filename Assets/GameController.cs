using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public string currentName = "Dave";
    [HideInInspector]
    public Host currentHost;
    [HideInInspector]
    public Directory currentDirectory;

    public Camera mainCamera;
    public GameObject gameUi;
    public GameObject menuUi;
    [SerializeField] private List<Host> soHosts;
   
    [HideInInspector] public List<Host> hosts;
    public Terminal terminal;
    public Image logo;
    public Settings settings;
    public Texture2D cursorTexture;
    
    [HideInInspector]
    public List<UpgradeFile> upgradeFiles = new List<UpgradeFile>();

    public int balance;
    
    public PlayableDirector director;

    public PlayableDirector hackEffect;
    [EventRef] public string hackingSound;
    [EventRef] public string hackingVoice;

    [HideInInspector]
    public bool hasMouse;

    void OnEnable()
    {
        hackEffect.Stop();
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            mainCamera.gameObject.SetActive(true);
            gameUi.gameObject.SetActive(true);
            menuUi.gameObject.SetActive(false);
        }

    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }

    
    public static GameController _instance;
    public static GameController Instance
    {
        get {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameController>();

            }
            return _instance;
        }
    }
    
    
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var host in soHosts)
        {
            hosts.Add(Instantiate(host));
        }


        Cursor.SetCursor(cursorTexture, Vector3.zero, CursorMode.Auto);
  

        // the home host
        currentHost = hosts[0];
        currentName = "dave";
        currentDirectory = currentHost.root.rootDirectory;
    }

    public void CreateDistortEffect(float time)
    {
        StartCoroutine(Distort(time));
    }

    IEnumerator Distort(float time)
    {
        //retroCameraEffect.randomGlitches = true;
        yield return new WaitForSeconds(time);
        //retroCameraEffect.randomGlitches = false;
    }

    public void HackLevel()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StartCoroutine(StartHack());
    }
    
    IEnumerator StartHack()
    {
        hackEffect.Play();
        FMODUnity.RuntimeManager.PlayOneShot(hackingSound);
        FMODUnity.RuntimeManager.PlayOneShot(hackingVoice);
        yield return new WaitForSeconds(3f);

        hackEffect.Stop();
        hackEffect.Evaluate();
        hackEffect.time = hackEffect.playableAsset.duration - 0.01;
     
        gameUi.SetActive(false);
        mainCamera.gameObject.SetActive(false);
        Instantiate(currentHost.hackLevel);
    }

    public void HackedSuccessfully()
    {
        terminal.DisplayResults($"\nReceived root information: \nuser=<b><color={settings.Color1}>{currentHost.root.user}</color></b>\npassword=<b><color={settings.Color1}>{currentHost.root.password}</color></b>\n",true);
    }
    public void NoiseEffectBackward()
    {
        if (!hasMouse)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }

        StartCoroutine(NoiseEffectBw());
    }

    IEnumerator NoiseEffectBw()
    {
        hackEffect.time = hackEffect.playableAsset.duration - 0.01;
        while (true)
        {
            double t = hackEffect.time - Time.deltaTime;
            if (t < 0)
                t = 0;
     

            hackEffect.time = t;
            hackEffect.Evaluate();

            if (t == 0)
            {
                hackEffect.Stop();
                enabled = false;
                break;
            }

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
