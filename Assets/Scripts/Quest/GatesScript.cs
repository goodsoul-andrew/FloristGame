using UnityEditor.UI;
using UnityEngine;

public class Gates : MonoBehaviour
{
    private int sealsCount;
    [SerializeField] GameObject[] seals;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sealsCount = seals.Length;
        foreach (var seal in seals)
        {
            if (seal.TryGetComponent<Health>(out var health))
            {
                health.OnDeath += DisableSeal;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OpenGates()
    {
        //Debug.Log("open gates");
        var rightCollider = right.GetComponent<BoxCollider2D>();
        var width = rightCollider.size.x;
        rightCollider.size = new Vector2(rightCollider.size.x / 2, rightCollider.size.y);
        rightCollider.offset = new Vector2(rightCollider.offset.x + width / 4, rightCollider.offset.y);

        var rightAnimator = right.GetComponent<Animator>();
        rightAnimator.SetBool("isOpen", true);

        var leftCollider = left.GetComponent<BoxCollider2D>();
        width = leftCollider.size.x;
        leftCollider.size = new Vector2(leftCollider.size.x / 2, leftCollider.size.y);
        leftCollider.offset = new Vector2(leftCollider.offset.x - width / 4, leftCollider.offset.y);

        var leftAnimator = left.GetComponent<Animator>();
        leftAnimator.SetBool("isOpen", true);
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
