using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class FlowersManager : MonoBehaviour
{
    [SerializeField] private List<Flower> Flowers;
    private Plant[] Plants;
    [SerializeField] private Sprite scrollSprite;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private int numberOfFlowers;
    [SerializeField] private GameObject Bar;

    private int Index = 0;
    private GameObject[] FlowersNumbers;
    private GameObject[] FlowersImages;
    private GameObject[] FlowersBackgrounds;
    private RectTransform scrollTransform;

    private void Start()
    {
        Plants = new Plant[Flowers.Count];
        for (var i = 0; i < Plants.Length; i++)
        {
            if (Flowers[i].Object.TryGetComponent<Plant>(out var plant))
            {
                Plants[i] = plant;
            }
            else
            {
                throw new InvalidOperationException("Попытка добавить в массив растений не растение");
            }
        }
        RecreateFlowersUI();

        var scrollObj = CreateBasicObject(new Vector2(70, 70), new Vector2((float)(Index - (numberOfFlowers - 1) / 2.0) * 100, 0));
        var spriteImage = scrollObj.AddComponent<Image>();
        spriteImage.sprite = scrollSprite;

        scrollTransform = scrollObj.GetComponent<RectTransform>();
    }
    public void PlaceFlower(Vector2 position)
    {
        if (Index >= numberOfFlowers) return;
        if (Flowers[Index].Count == 0) return;
        if (Plants[Index].TryPlace(position))
        {
            FindFirstObjectByType<TutorialManager>().FinishTutorial("place");
            Flowers[Index].Count--;
            if (Flowers[Index].Count >= 0)
            {
                var textComponent = FlowersNumbers[Index].GetComponent<TextMeshProUGUI>();
                textComponent.text = Flowers[Index].Count.ToString();
            }
        }
    }
    public void SetIndex(int index)
    {
        index--;
        //Debug.Log($"индекс нажат:{index}");
        if (index < numberOfFlowers)
        {
            FindFirstObjectByType<TutorialManager>().FinishTutorial("change");
            //Debug.Log($"Новый индекс:{index}");
            Index = index;
            scrollTransform.anchoredPosition = new Vector2((float)(Index - (numberOfFlowers - 1) / 2.0) * 100, 0);
        }
    }

    public void SetNumberOfFlowers(int number)
    {
        numberOfFlowers = Math.Min(number, Flowers.Count);
        RecreateFlowersUI();
        scrollTransform.anchoredPosition = new Vector2((float)(Index - (numberOfFlowers - 1) / 2.0) * 100, 0);
    }
    public int GetNumberOfFlowers()
    {
        return numberOfFlowers;
    }

    public void ChangeNumberOfFlowers(int index, int amount)
    {
        Flowers[index].Count += amount;
        var textComponent = FlowersNumbers[index].GetComponent<TextMeshProUGUI>();
        textComponent.text = Flowers[index].Count.ToString();
    }

    public void RecreateFlowersUI()
    {
        if (FlowersNumbers != null)
        {
            for (var i = 0; i < numberOfFlowers; i++)
            {
                if (FlowersNumbers[i] != null) Destroy(FlowersNumbers[i]);
                if (FlowersImages[i] != null) Destroy(FlowersImages[i]);
                if (FlowersBackgrounds[i] != null) Destroy(FlowersBackgrounds[i]);
            }
        }
        FlowersNumbers = new GameObject[numberOfFlowers];
        FlowersImages = new GameObject[numberOfFlowers];
        FlowersBackgrounds = new GameObject[numberOfFlowers];

        for (var i = 0; i < numberOfFlowers; i++)
        {
            var xLocation = (float)(i - (numberOfFlowers - 1) / 2.0) * 100;

            //фон
            var backObj = CreateBasicObject(new Vector2(70, 70), new Vector2(xLocation, 0));
            var backImage = backObj.AddComponent<Image>();
            backImage.sprite = backSprite;

            FlowersBackgrounds[i] = backObj;


            //картинка
            var imageObj = CreateBasicObject(new Vector2(60, 60), new Vector2(xLocation, 0));
            var imageImage = imageObj.AddComponent<Image>();
            imageImage.sprite = Flowers[i].Sprite;

            FlowersImages[i] = imageObj;


            //текст
            var textObj = CreateBasicObject(new Vector2(60, 40), new Vector2(xLocation, -50));

            var text = textObj.AddComponent<TextMeshProUGUI>();
            if (Flowers[i].Count >= 0)
                text.text = Flowers[i].Count.ToString();
            text.alignment = TextAlignmentOptions.Center;

            FlowersNumbers[i] = textObj;

        }
    }

    private GameObject CreateBasicObject(Vector2 size, Vector2 position)
    {
        GameObject obj = new GameObject();
        obj.transform.SetParent(Bar.transform, false);

        var transform = obj.AddComponent<RectTransform>();
        transform.sizeDelta = size;
        transform.anchoredPosition = position;
        obj.layer = LayerMask.NameToLayer("UI");

        return obj;
    }
}

[System.Serializable]
public class Flower
{
    public string Name;
    public GameObject Object;
    public Sprite Sprite;
    public int Count;

}