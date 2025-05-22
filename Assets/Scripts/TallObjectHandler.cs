using UnityEngine;

class TallObjectHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer selfSprite;
    private int originalOrder;

    void Start()
    {
        selfSprite = (selfSprite == null) ? GetComponent<SpriteRenderer>() : selfSprite;
        originalOrder = selfSprite.sortingOrder;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SpriteRenderer otherSprite = other.GetComponent<SpriteRenderer>();
        int otherSortingOrder = -1;

        if (otherSprite != null)
        {
            otherSortingOrder = otherSprite.sortingOrder;
        }
        else
        {
            otherSortingOrder = FindSortingOrderInChildren(other.transform);
        }

        if (otherSortingOrder != -1)
        {
            selfSprite.sortingOrder = otherSortingOrder + 1;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        selfSprite.sortingOrder = originalOrder;
    }

    private int FindSortingOrderInChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer childSprite = child.GetComponent<SpriteRenderer>();
            if (childSprite != null)
            {
                return childSprite.sortingOrder;
            }
            int sortingOrderInGrandchildren = FindSortingOrderInChildren(child);
            if (sortingOrderInGrandchildren != -1)
            {
                return sortingOrderInGrandchildren;
            }
        }

        return -1;
    }
}