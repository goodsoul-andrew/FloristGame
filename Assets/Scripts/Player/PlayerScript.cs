using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IMoving, IDamageable
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    [SerializeField]private Animator animator;
    private FlowersManager flowersManager;
    private DialogueManager dialogueManager;

    private Vector2 moveInput;
    public bool isPaused;
    public bool isDead;
    private bool canPlace;
    [SerializeField] private float placeRadius = 5;
    [SerializeField] private float placeDelay = 2f;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject InteractionObject;

    [SerializeField]private Animator cutsceneAnimator;


    private List<Interaction> interactors = new();

    private CircleCollider2D selfColllider;
    public Vector2 TruePosition => (Vector2)selfColllider.transform.position + selfColllider.offset;

    public float Speed { get; set; }
    public Health Hp {get; set;}
    public float MaxSpeed {get; set;}

    private void Start()
    {
        Speed = 6f;
        MaxSpeed = Speed;
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();
        flowersManager = FindFirstObjectByType<FlowersManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        selfColllider = GetComponent<CircleCollider2D>();
        Hp = GetComponent<Health>();

        playerInput.actions["Move"].performed += HandleMove;
        playerInput.actions["Move"].canceled += HandleMoveCanceled;

        playerInput.actions["Place"].started += HandleClick;

        playerInput.actions["Pause"].started += HandlePause;

        playerInput.actions["ChangeFlower"].started += HandleChangeFlower;

        playerInput.actions["Interact"].started += HandleInteraction;

        playerInput.actions["SkipDialogue"].started += HandleDialogueSkip;

        StartCoroutine(StartPlaceDelay(0));
        Hp.OnDeath += HandleDeath;
    }

    private void FixedUpdate()
    {
        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);

        if(moveInput.x*moveInput.x+moveInput.y*moveInput.y!=0) FindFirstObjectByType<TutorialManager>().FinishTutorial("walk");

        rb.MovePosition(rb.position + moveInput * (Speed * Time.deltaTime));
    }

    public void InteractionEnter(Interaction interactor) 
    {
        if(!interactors.Contains(interactor))
        {
            interactors.Add(interactor);
            InteractionObject.SetActive(true);
        }
    }

    public void InteractionExit(Interaction interactor) 
    {
        interactor.EndInteraction();
        interactors.Remove(interactor);
        InteractionObject.SetActive(false);
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        if (isPaused) return;
        moveInput = context.ReadValue<Vector2>();
        // //Debug.Log($"Movement Input: {moveInput}");
    }

    private void HandleMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        // //Debug.Log($"Movement Input canceled");
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

    private void HandleInteraction(InputAction.CallbackContext context)
    {
        if (!isPaused && interactors.Count!=0)
        {   
            var interactor = interactors[0];
            interactor.StartInteraction();
            interactors.Remove(interactor);
            if(interactors.Count == 0)
                InteractionObject.SetActive(false);
        }
    }

    private void HandleDialogueSkip(InputAction.CallbackContext context)
    {
        if (isPaused) return;
        dialogueManager.DisplayNextSentence();
        FindFirstObjectByType<TutorialManager>().FinishTutorial("dialogue");
    }

    private void HandleDeath()
    {
        isPaused = true;
        isDead = true;
        Hp.IsImmortal = true;
        animator.SetBool("isDead", true);
        cutsceneAnimator.SetTrigger("Die");
    }
    public IEnumerator StartPlaceDelay(float delay)
    {
        canPlace = false;
        yield return new WaitForSeconds(delay);
        canPlace = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0.1f;
        isPaused = true;
        pauseMenu.SetActive(true);
        //AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
        //AudioListener.pause = false;
    }
}
