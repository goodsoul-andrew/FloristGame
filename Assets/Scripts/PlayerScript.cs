using System;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Animator animator;

    private Vector2 moveInput;
    private bool isPaused;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float placeRadius = 5;
    [SerializeField] private UnityEngine.Object creationObject;
    [SerializeField] private UnityEngine.GameObject pauseMenu;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playerInput.actions["Move"].performed += HandleMove;
        playerInput.actions["Move"].canceled += HandleMoveCanceled;

        playerInput.actions["Place"].started += HandleClick;

        playerInput.actions["Pause"].started += HandlePause;

        ResumeGame();
    }

    private void FixedUpdate()
    {
        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);

        rb.MovePosition(rb.position + moveInput * (moveSpeed * Time.deltaTime));
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        // Debug.Log($"Movement Input: {moveInput}");
    }

    private void HandleMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        // Debug.Log($"Movement Input canceled");
    }

    private void HandleClick(InputAction.CallbackContext context)
    {
        if (isPaused) return;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // Debug.Log("Клик по позиции: " + worldPosition);
        var dist = ((Vector2)rb.transform.position - worldPosition).magnitude;
        // Debug.Log("Расстояние от клика до игрока = " + dist);
        if (dist <= placeRadius)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPosition, 0.5f); // Радиус может быть настройкой

            bool canPlace = true;
            foreach (var collider in colliders)
            {
                if (collider.gameObject.CompareTag("PlayerMinion"))
                {
                    canPlace = false; // Найден объект с тем же тегом
                    break;
                }
            }

            if (canPlace)
            {
                GameObject minion = (GameObject)Instantiate(creationObject, worldPosition, Quaternion.Euler(0, 0, 0));
                minion.tag = "PlayerMinion";
            }
            else
            {
                Debug.Log("Невозможно разместить миниона: область занята!");
            }
        }
    }

    private void HandlePause(InputAction.CallbackContext context)
    {
        if (!isPaused) PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
        AudioListener.pause = false;
    }
}
