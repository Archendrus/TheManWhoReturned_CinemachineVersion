using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 5f;
    private Vector2 moveDirection;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

    private void FixedUpdate()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        rb.MovePosition(position + (moveDirection * speed * Time.fixedDeltaTime));
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveDirection = ctx.ReadValue<Vector2>();
    }

    public void OnQuit(InputAction.CallbackContext ctx)
    {
        Application.Quit();
    }
}
