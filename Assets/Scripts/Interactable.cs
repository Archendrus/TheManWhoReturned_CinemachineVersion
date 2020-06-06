using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private SpriteRenderer iconSprite;
    [SerializeField] private Sprite gamepadButtonSprite;
    [SerializeField] private Sprite keyboardButtonSprite;

    private bool triggered = false;
    private bool inRange = false;

    public Dialogue Dialogue;

    public void OnEnable()
    {
        // Set sprites to use for advance icon based on current input device
        // Add listener to get device change events
        SetIconsForCurrentControlScheme(playerInput.currentControlScheme);
        InputUser.onChange += onInputDeviceChange;
    }

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

    private void SetIconsForCurrentControlScheme(string scheme)
    {
        switch (scheme)
        {
            case "Keyboard":
                iconSprite.sprite = keyboardButtonSprite;
                break;
            case "Gamepad":
                iconSprite.sprite = gamepadButtonSprite;
                break;
        }
    }

    public void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        // user changed devices, set new sprite for advance icon
        if (change == InputUserChange.ControlSchemeChanged)
        {
            Debug.Log(playerInput.currentControlScheme);
            SetIconsForCurrentControlScheme(playerInput.currentControlScheme);
        }
    }
}
