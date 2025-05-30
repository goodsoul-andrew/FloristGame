using UnityEngine;

public class QuestFlowerbed : MonoBehaviour
{
    [SerializeField] GameObject questPlant;
    public System.Action OnComplete;
    private bool isActive;
    private Health HP;
    private PlaySoundsScript soundPlayer;

    void Start()
    {
        isActive = true;
        HP = GetComponent<Health>();
        HP.IsImmortal = true;
        soundPlayer = GetComponent<PlaySoundsScript>();
    }

    public bool IsRightFlower(QuestFlower flower)
    {
        return questPlant.CompareTag(flower.tag);
    }

    public bool TryActivate(QuestFlower flower)
    {

        if (!isActive || !IsRightFlower(flower)) return false;
        OnComplete?.Invoke();
        soundPlayer.PlaySound(0);
        HP.Kill();
        return true;
    }
}
