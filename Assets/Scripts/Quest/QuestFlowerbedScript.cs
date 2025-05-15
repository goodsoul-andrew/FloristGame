using UnityEngine;
using UnityEngine.LightTransport;

public class QuestFlowerbed : MonoBehaviour
{
    [SerializeField] GameObject questPlant;
    public System.Action OnComplete;
    private bool isActive;
    private Health HP;

    void Start()
    {
        isActive = true;
        HP = GetComponent<Health>();
        HP.IsImmortal = true;
    }

    public bool IsRightFlower(QuestFlower flower)
    {
        return questPlant.CompareTag(flower.tag);
    }

    public bool TryActivate(QuestFlower flower)
    {

        if (!isActive || !IsRightFlower(flower)) return false;
        OnComplete?.Invoke();
        HP.Kill();
        return true;
    }
}
