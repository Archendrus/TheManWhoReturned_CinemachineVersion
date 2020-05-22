using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScaleFactor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float scale = Screen.currentResolution.width / 320;
        if (scale % 2 != 0)
        {
            Mathf.Floor(scale);
            scale++;
        }
        gameObject.GetComponent<CanvasScaler>().scaleFactor = scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
