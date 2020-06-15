using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePoint;

    void Update()
    {
        // Attach DialogueBox to a sprite's DialoguePoint
        Vector3 newPos = Camera.main.WorldToScreenPoint(DialoguePoint.transform.position);
        GetComponent<RectTransform>().position = new Vector3(newPos.x, newPos.y, 1);
    }

    public void SetDialoguePoint(GameObject newPoint)
    {
        DialoguePoint = newPoint;
    }
}
