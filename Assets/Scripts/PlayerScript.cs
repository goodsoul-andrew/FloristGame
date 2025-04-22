using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IMoving
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Animator animator;
    private FlowersManagerScript flowersManager;

    private Vector2 moveInput;
    private bool isPaused;
    private bool canPlace;
    [SerializeField] private float placeRadius = 5;
    [SerializeField] private float placeDelay = 2f;
    [SerializeField] private UnityEngine.GameObject pauseMenu;

    private CircleCollider2D selfColllider;
    public Vector2 TruePosition => (Vector2)selfColllider.transform.position + selfColllider.offset;

    public float Speed { get; set; }

    private void Start()
    {
        Speed = 10f;
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flowersManager = GetComponent<FlowersManagerScript>();
        selfColllider = GetComponent<CircleCollider2D>();

        playerInput.actions["Move"].performed += HandleMove;
        playerInput.actions["Move"].canceled += HandleMoveCanceled;

        playerInput.actions["Place"].started += HandleClick;

        playerInput.actions["Pause"].started += HandlePause;

        playerInput.actions["ChangeFlower"].started += HandleChangeFlower;

        StartCoroutine(StartPlaceDelay(0));
        ResumeGame();
    }

    private void FixedUpdate()
    {
        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);

        if(moveInput.x*moveInput.x+moveInput.y*moveInput.y!=0) TutorialScript.FinishTutorial("walk");

        rb.MovePosition(rb.position + moveInput * (Speed * Time.deltaTime));
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
        if (isPaused || !canPlace) return;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var dist = ((Vector2)rb.transform.position - worldPosition).magnitude;
        //Debug.Log($"Клик по позиции: {worldPosition}, рассдояние до игрока = {dist}");
        if (dist <= placeRadius)
        {
            flowersManager.PlaceFlower(worldPosition);
            StartCoroutine(StartPlaceDelay(placeDelay));
        }
    }

    private void HandlePause(InputAction.CallbackContext context)
    {
        if (!isPaused) PauseGame();
    }

    private void HandleChangeFlower(InputAction.CallbackContext context)
    {
        if (!isPaused) flowersManager.SetIndex(int.Parse(context.control.name));
    }

    public IEnumerator StartPlaceDelay(float delay)
    {
        canPlace = false;
        yield return new WaitForSeconds(delay);
        canPlace = true;
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
