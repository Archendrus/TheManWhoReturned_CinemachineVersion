using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject button;
    private bool triggered = false;
    private bool inRange = false;

    public string Text;

    // Update is called once per frame
    void Update()
    {
        // Hide button if player not in range
        // or the interactable has been triggered
        if (inRange && !triggered)
        {
            button.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
        }
    }

    public void SetTriggered(bool value)
    {
        triggered = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Show button sprite
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Hide button sprite
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
