using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    private GameObject sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Camera.main.WorldToScreenPoint(sprite.transform.position);
        float x = Mathf.Ceil(newPos.x);
        float y = Mathf.Ceil(newPos.y);
        GetComponent<RectTransform>().position = new Vector3(x, y, 1);
        //GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(sprite.transform.position);
    }
}
