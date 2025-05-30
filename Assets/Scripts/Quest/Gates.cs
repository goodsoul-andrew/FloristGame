
using UnityEngine;

public class Gates : MonoBehaviour
{
    private int sealsCount;
    public bool isOpen{ get; private set; }
    [SerializeField] GameObject[] seals;
    [SerializeField] GameObject left;
    private BoxCollider2D leftCollider;
    private Animator leftAnimator;
    private Vector2 originalLeftSize;
    private Vector2 originalLeftOffset;

    [SerializeField] GameObject right;
    private Vector2 originalRightSize;
    private Vector2 originaRightOffset;
    private BoxCollider2D rightCollider;
    private Animator rightAnimator;
    private PlaySoundsScript soundPlayer;
    public System.Action OnOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sealsCount = seals.Length;
        soundPlayer = GetComponent<PlaySoundsScript>();
        foreach (var seal in seals)
        {
            if (seal.TryGetComponent<Health>(out var health))
            {
                health.OnDeath += DisableSeal;
            }
        }
        rightCollider = right.GetComponent<BoxCollider2D>();
        originalRightSize = rightCollider.size;
        originaRightOffset = rightCollider.offset;
        rightAnimator = right.GetComponent<Animator>();

        leftCollider = left.GetComponent<BoxCollider2D>();
        originalLeftSize = leftCollider.size;
        originalLeftOffset = leftCollider.offset;
        leftAnimator = left.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenGates()
    {
        if (isOpen) return;
        //Debug.Log("open gates");
        soundPlayer.PlaySound("Open");
        isOpen = true;
        var width = rightCollider.size.x;
        rightCollider.size = new Vector2(rightCollider.size.x * 0.53f, rightCollider.size.y);
        rightCollider.offset = new Vector2(rightCollider.offset.x + width * 0.53f * 0.5f, rightCollider.offset.y);
        rightAnimator.SetTrigger("Open");

        width = leftCollider.size.x;
        leftCollider.size = new Vector2(leftCollider.size.x * 0.53f, leftCollider.size.y);
        leftCollider.offset = new Vector2(leftCollider.offset.x - width * 0.53f * 0.5f, leftCollider.offset.y);

        leftAnimator.SetTrigger("Open");
        OnOpen?.Invoke();
    }

    public void CloseGates()
    {
        if (!isOpen) return;
        isOpen = false;
        soundPlayer.PlaySound("Close");

        rightCollider.size = originalRightSize;
        rightCollider.offset = originaRightOffset;
        rightAnimator.SetTrigger("Close");

        leftCollider.size = originalLeftSize;
        leftCollider.offset = originalLeftOffset;
        leftAnimator.SetTrigger("Close");
    }

    void DisableSeal()
    {
        sealsCount -= 1;
        if (sealsCount == 0)
        {
            OpenGates();
        }
    }
}
