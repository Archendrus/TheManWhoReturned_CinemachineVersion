using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePoint = null;

    void Update()
    {
        if (dialoguePoint != null)
        {
            // Attach DialogueBox to a sprite's DialoguePoint
            Vector3 newPos = Camera.main.WorldToScreenPoint(dialoguePoint.transform.position);
            GetComponent<RectTransform>().position = new Vector3(newPos.x, newPos.y, 1);
        }

    }

    public void SetDialoguePoint(GameObject newPoint)
    {
        dialoguePoint = newPoint;
    }
}
