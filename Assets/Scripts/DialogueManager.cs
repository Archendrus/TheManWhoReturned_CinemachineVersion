using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private int currentLine;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typeSpeed;
    [SerializeField] private PlayerController player;

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && player.Interactable != null)
        {
            List<string> dialogueLines = player.Interactable.GetComponent<Interactable>().Dialogue.Lines;

            // Canvas not on, show canvas, reset line position
            // hide interactable icon
            if (!transform.gameObject.activeInHierarchy)
            {
                currentLine = 0;
                player.Interactable.GetComponent<Interactable>().SetTriggered(true);
                transform.gameObject.SetActive(true);
                StartCoroutine(TypeOneLine(dialogueLines[currentLine]));
            }
            // Canvas on, increment line position if more lines
            else if (currentLine < dialogueLines.Count - 1)
            {
                if (dialogueText.text == dialogueLines[currentLine])
                {
                    currentLine++;
                    StartCoroutine(TypeOneLine(dialogueLines[currentLine]));
                }

            }
            else // Canvas on and no lines left, hide canvas, show interactable icon
            {
                if (dialogueText.text == dialogueLines[currentLine])
                {
                    dialogueText.text = "";
                    player.Interactable.GetComponent<Interactable>().SetTriggered(false);
                    transform.gameObject.SetActive(false);
                }
            }
        }
    }

    private IEnumerator TypeOneLine(string line)
    {
        // TODO: Fix this. Garbage in first line when re-trigger same interactable
        dialogueText.text = "";
        foreach (char ch in line)
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
