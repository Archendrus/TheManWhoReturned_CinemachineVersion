using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject DialogueTextObject;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public Animator animator;
    private bool canInteract = false;
    private GameObject interactable;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canvas.gameObject.activeInHierarchy)
        {
            // If moving, set animator state, maybe flip sprite
            if (moveDirection.x != 0)
            {
                animator.SetBool("isRunning", true);
                if (moveDirection.x < 0)
                {
                    gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                }
                else
                {
                    gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
                }
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        if (!canvas.gameObject.activeInHierarchy)
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            rb.MovePosition(position + (moveDirection * speed * Time.fixedDeltaTime));
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveDirection = ctx.ReadValue<Vector2>();
    }

    public void OnQuit(InputAction.CallbackContext ctx)
    {
        Application.Quit();
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && interactable != null)
        {
            string text = interactable.GetComponent<Interactable>().Text;
            TextMeshProUGUI DialogueText = DialogueTextObject.GetComponent<TextMeshProUGUI>();
            if (!canvas.gameObject.activeInHierarchy)
            {
                DialogueText.text = text;
                interactable.GetComponent<Interactable>().SetTriggered(true);
                canvas.gameObject.SetActive(true);
            }
            else
            {
                DialogueText.text = "";
                interactable.GetComponent<Interactable>().SetTriggered(false);
                canvas.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactable = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactable = null;
        }
    }
}
