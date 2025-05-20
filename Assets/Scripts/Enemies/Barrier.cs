using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private List<GameObject> protectors;
    public int Count {get; private set;}

    void Start()
    {
        protectors = protectors.Where(el => el != null).ToList();
        foreach (var guard in protectors)
        {
            if (guard.TryGetComponent<Health>(out var health))
            {
                Count += 1;
                health.OnDeath += DecreaseCount;
            }
        }
    }

    private void DecreaseCount()
    {
        Count -= 1;
        if (Count <= 0)
        {
            FindFirstObjectByType<TutorialManager>().FinishTutorial("block");
            Destroy(gameObject);
        }
    }
}
