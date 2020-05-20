using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScaleFactor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 1080p - 6
        // 720p - 4
        // 1600x900 - 5 <- PROBLEM
        float scale = Screen.currentResolution.width / 320;
        if (scale % 2 != 0)
        {
            scale++;
        }
        gameObject.GetComponent<CanvasScaler>().scaleFactor = scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
