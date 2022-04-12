using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Playables;

public class Exit : MonoBehaviour
{
    public GameObject level;

    public PlayableDirector director;
    [EventRef] public string hackingSound;
    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
        
        
    }
    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector && level.GetComponent<Level>().hasSolved)
        {
            Destroy(level.gameObject);
            GameController.Instance.gameUi.SetActive(true);
            GameController.Instance.HackedSuccessfully();
            GameController.Instance.NoiseEffectBackward();
            GameController.Instance.mainCamera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        director.time = 0f;
        director.Play();
        FMODUnity.RuntimeManager.PlayOneShot(hackingSound);
    }
}
