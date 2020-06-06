using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private int currentLine;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typeSpeed;
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Image advanceIcon;
    [SerializeField] private Sprite gamepadButtonSprite;
    [SerializeField] private Sprite keyboardButtonSprite;

    public void OnEnable()
    {
        // Set sprites to use for advance icon based on current input device
        // Add listener to get device change events
        SetIconsForCurrentControlScheme(playerInput.currentControlScheme);
        InputUser.onChange += onInputDeviceChange;
    }

    public void OnDisable()
    {
        InputUser.onChange -= onInputDeviceChange;
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        // Interact button press and player has something to interact with
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
        // Hide advance icon, clear previous text
        advanceIcon.enabled = false;
        dialogueText.text = "";

        // Type each character in line
        foreach (char ch in line)
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typeSpeed);
        }

        // Line fully type, show advance icon
        advanceIcon.enabled = true;
    }

    public void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        // user changed devices, set new sprite for advance icon
        if (change == InputUserChange.ControlSchemeChanged)
        {
            SetIconsForCurrentControlScheme(playerInput.currentControlScheme);
        }
    }

    private void SetIconsForCurrentControlScheme(string scheme)
    {
        switch (scheme)
        {
            case "Keyboard":
                advanceIcon.sprite = keyboardButtonSprite;
                break;
            case "Gamepad":
                advanceIcon.sprite = gamepadButtonSprite;
                break;
        }
    }
}
