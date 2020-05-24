using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScaleFactor : MonoBehaviour
{
    void Start()
    {
        // Set CanvasScaler scale factor
        // Calculate scale factor using native resolution
        // Ensure always an even integer
        float scale = Screen.currentResolution.width / 320;
        if (scale % 2 != 0)
        {
            Mathf.Floor(scale);
            scale++;
        }
        gameObject.GetComponent<CanvasScaler>().scaleFactor = scale;
    }
}
