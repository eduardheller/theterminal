using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{

    [SerializeField] private float fadeTimer = 3f;

    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowText());


    }


    IEnumerator ShowText()
    {
        byte alpha = 0;
        _text.faceColor = new Color32(_text.faceColor.r, _text.faceColor.g, _text.faceColor.b,alpha);

        while (alpha < 255)
        {
            alpha++;
            _text.faceColor = new Color32(_text.faceColor.r, _text.faceColor.g, _text.faceColor.b,alpha);
            yield return new WaitForSeconds(0.03f);
        }

        while (alpha > 0)
        {
            alpha--;
            _text.faceColor = new Color32(_text.faceColor.r, _text.faceColor.g, _text.faceColor.b,alpha);
            yield return new WaitForSeconds(0.03f);
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
