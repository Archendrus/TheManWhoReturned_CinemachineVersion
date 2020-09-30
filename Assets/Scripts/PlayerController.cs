using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Canvas canvas = null;

    public Animator animator;
    public GameObject Interactable;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

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

    void FixedUpdate()
    {
        // Allow move if no dialog open
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        // set GameObject interactable on collision
        if (collision.CompareTag("Interactable"))
        {
            Interactable = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Clear GameObject interactable on exit collision
        if (collision.CompareTag("Interactable"))
        {
            Interactable = null;
        }
    }
}
