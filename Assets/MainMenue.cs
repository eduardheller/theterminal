using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenue : MonoBehaviour
{

    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject canvasOptions;
    [SerializeField] private FMODUnity.StudioEventEmitter menueMusic;
    [SerializeField] private GameObject introObject;
    [SerializeField] private Camera mainMenuCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGame()
    {
        canvasMenu.SetActive(false);
        canvasOptions.SetActive(false);
        menueMusic.enabled = false;
        mainMenuCamera.gameObject.SetActive(false);
        introObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GameController.Instance.hasMouse = false;
        Cursor.visible = false;
    }
    

    public void Exit()
    {
        Application.Quit();
    }

    public void Options()
    {
        canvasMenu.SetActive(false);
        canvasOptions.SetActive(true);
    }

    public void OptionsBack()
    {
        canvasMenu.SetActive(true);
        canvasOptions.SetActive(false);
    }

}
