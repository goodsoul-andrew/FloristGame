using System;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float placeRadius = 2;
    [SerializeField] private UnityEngine.Object creationObject;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerInput.actions["Move"].performed += HandleMove;
        playerInput.actions["Move"].canceled += HandleMoveCanceled;

        playerInput.actions["Place"].started += HandleClick;
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Movement Input: {moveInput}");
    }

    private void HandleMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        Debug.Log($"Movement Input canceled");
    }

    private void HandleClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log("Клик по позиции: " + worldPosition);
        var dist = ((Vector2)rb.transform.position - worldPosition).magnitude;
        Debug.Log("Расстояние от клика до игрока = " + dist);
        if (dist <= placeRadius)
        {
            Instantiate(creationObject, worldPosition, Quaternion.Euler(0, 0, 0));
        }
    }

    private void FixedUpdate()
    {
        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);

        Camera.main.transform.position = new Vector3(rb.position.x, rb.position.y, Camera.main.transform.position.z);
        rb.MovePosition(rb.position + moveInput * (moveSpeed * Time.deltaTime));
    }
}
