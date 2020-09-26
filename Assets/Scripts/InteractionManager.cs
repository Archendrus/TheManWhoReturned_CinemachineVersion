using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    private int currentLine;

    [SerializeField] private TextMeshProUGUI dialogueText  = null;
    [SerializeField] private DialogueBox dialogueBox = null;
    [SerializeField] private float typeSpeed = 0;
    [SerializeField] private PlayerController player = null;
    [SerializeField] private PlayerInput playerInput = null;
    [SerializeField] private Image advanceIcon = null;
    [SerializeField] private Sprite gamepadButtonSprite = null;
    [SerializeField] private Sprite keyboardButtonSprite = null;
    [SerializeField] private AudioSource typeSound = null;
    private AudioClip clip = null;

    void OnEnable()
    {
        // Set sprites to use for dialogue advance icon based on current input device
        // Add listener to get device change events
        SetIconsForCurrentControlScheme(playerInput.currentControlScheme);
        InputUser.onChange += onInputDeviceChange;

        clip = typeSound.clip;
    }

    void OnDisable()
    {
        InputUser.onChange -= onInputDeviceChange;
    }

    void OnInteract(InputAction.CallbackContext ctx)
    {
        // Interact button press and player has something to interact with
        if (ctx.performed && player.Interactable != null)
        {
            Interactable interactable = player.Interactable.GetComponent<Interactable>();
            if (interactable.sceneChange != null)
            {
                // do scene change
            }
            else
            {
                List<Dialogue.Line> dialogueLines = interactable.Dialogue.Lines;

                // Canvas not on
                // reset line position, hide interactable icon, show canvas, 
                // attach dialogue box to point defined 
                if (!transform.gameObject.activeInHierarchy)
                {
                    currentLine = 0;
                    player.Interactable.GetComponent<Interactable>().SetTriggered(true);
                    transform.gameObject.SetActive(true);

                    dialogueBox.SetDialoguePoint(dialogueLines[currentLine].CharacterDialoguePoint);

                    StartCoroutine(TypeOneLine(dialogueLines[currentLine].Text));
                }
                // Canvas on, increment line position if more lines
                else if (currentLine < dialogueLines.Count - 1)
                {
                    if (dialogueText.text == dialogueLines[currentLine].Text)
                    {
                        currentLine++;
                        dialogueBox.SetDialoguePoint(dialogueLines[currentLine].CharacterDialoguePoint);
                        StartCoroutine(TypeOneLine(dialogueLines[currentLine].Text));
                    }

                }
                else // Canvas on and no lines left, hide canvas, show interactable icon
                {
                    if (dialogueText.text == dialogueLines[currentLine].Text)
                    {
                        dialogueText.text = "";
                        player.Interactable.GetComponent<Interactable>().SetTriggered(false);
                        transform.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    IEnumerator TypeOneLine(string line)
    {
        // Hide advance icon, clear previous text
        advanceIcon.enabled = false;
        dialogueText.text = "";

        // Type each character in line
        foreach (char ch in line)
        {
            dialogueText.text += ch;
            if (ch != ' ')
            {
                typeSound.PlayOneShot(clip);
            }
            
            yield return new WaitForSeconds(typeSpeed);
        }

        // Line fully type, show advance icon
        advanceIcon.enabled = true;
    }

    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        // user changed devices, set new sprite for advance icon
        if (change == InputUserChange.ControlSchemeChanged)
        {
            SetIconsForCurrentControlScheme(playerInput.currentControlScheme);
        }
    }

    void SetIconsForCurrentControlScheme(string scheme)
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
