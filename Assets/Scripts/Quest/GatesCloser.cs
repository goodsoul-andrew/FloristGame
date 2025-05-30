using UnityEngine;

class GatesCloser : MonoBehaviour
{
    [SerializeField] private Gates gates;
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        gates.CloseGates();
    }
}