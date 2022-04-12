using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkCursor : MonoBehaviour
{

    [SerializeField] private float blinkDelay = 0.5f;
    private Image _cursor;
    // Start is called before the first frame update
    void Start()
    {
        _cursor = GetComponent<Image>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            _cursor.enabled = !_cursor.enabled;
            yield return new WaitForSeconds(blinkDelay);
        }
    }
    
}
