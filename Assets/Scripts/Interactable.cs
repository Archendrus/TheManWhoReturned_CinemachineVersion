using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject button = null;
    [SerializeField] private PlayerInput playerInput = null;
    [SerializeField] private SpriteRenderer iconSprite = null;
    [SerializeField] private Sprite gamepadButtonSprite = null;
    [SerializeField] private Sprite keyboardButtonSprite = null;

    private bool triggered = false;
    private bool inRange = false;

    public Dialogue Dialogue;

    void OnEnable()
    {
        // Set sprites to use for interact icon based on current input device
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Show button sprite
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Hide button sprite
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    void SetIconsForCurrentControlScheme(string scheme)
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

    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        // user changed devices, set new sprite for advance icon
        if (change == InputUserChange.ControlSchemeChanged)
        {
            Debug.Log(playerInput.currentControlScheme);
            SetIconsForCurrentControlScheme(playerInput.currentControlScheme);
        }
    }
}
