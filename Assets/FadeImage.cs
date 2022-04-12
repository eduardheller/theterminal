using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{

    [SerializeField] private float fadeTimer = 3f;

    [SerializeField] private GameObject toActivate;
    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        StartCoroutine(ShowText());


    }


    IEnumerator ShowText()
    {
        float alpha = 0f;
        var color = _image.color;
        _image.color = color;

     
        while (alpha < 1)
        {
            alpha += 1f/255f;
            yield return new WaitForSeconds(0.03f);
        }
        
        while (alpha > 0)
        {
            alpha -= 1f/255f;;
            color = new Color(color.r, color.g, color.b,alpha);
            _image.color = color;
            yield return new WaitForSeconds(0.03f);
        }
        toActivate.SetActive(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}