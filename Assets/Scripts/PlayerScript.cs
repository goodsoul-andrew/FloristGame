using System;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    public float moveSpeed = 10f;
    public UnityEngine.Object creationObject;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
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

        var creation = Instantiate(creationObject, worldPosition, Quaternion.Euler(0, 0, 0));
        Debug.Log("Клик по позиции: " + worldPosition);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * (moveSpeed * Time.deltaTime));
    }
}
