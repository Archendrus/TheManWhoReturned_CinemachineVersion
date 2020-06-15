using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public struct Line
    {
        public GameObject CharacterDialoguePoint;

        [TextArea(2, 5)]
        public string Text;
    }

    public List<Line> Lines;
}
