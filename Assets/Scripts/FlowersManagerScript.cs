using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FlowersManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] FlowersObjects;
    private Plant[] Plants;
    [SerializeField] private int[] FlowersCount;
    [SerializeField] private Sprite scrollSprite;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private int Index = 0;
    [SerializeField] private int numberOfFlowers;
    [SerializeField] private GameObject Bar;

    private GameObject[] FlowersNumbers;
    private GameObject[] FlowersImages;
    private GameObject[] FlowersBackgrounds;
    private RectTransform scrollTransform;

    private void Start()
    {
        Plants = new Plant[FlowersObjects.Length];
        for (var i = 0; i < Plants.Length; i++)
        {
            if (FlowersObjects[i].TryGetComponent<Plant>(out var plant))
            {
                Plants[i] = plant;
                Debug.Log(plant);
            }
            else
            {
                throw new InvalidOperationException("Попытка добавить в массив растений не растение");
            }
        }
        RecreateFlowersUI();

        var scrollObj = CreateBasicObject(new Vector2(50, 50),new Vector2((float)(Index - (numberOfFlowers - 1) / 2.0)*100, 0));
        var spriteImage = scrollObj.AddComponent<Image>();
        spriteImage.sprite = scrollSprite;

        scrollTransform = scrollObj.GetComponent<RectTransform>();
    }
    public void PlaceFlower(Vector2 position)
    {
        if(Index >= numberOfFlowers)
        {
            Debug.Log("Цветка под таким индексом нет");
            return;
        }
        if(FlowersCount[Index] == 0)
        {
            Debug.Log("Закончились");
            return;
        }
        if (Plants[Index].IsAreaAvailable(position))
        {
            TutorialScript.FinishTutorial("place");
            FlowersCount[Index]--;
            if(FlowersCount[Index] >= 0)
            {
                var textComponent = FlowersNumbers[Index].GetComponent<TextMeshProUGUI>();
                textComponent.text = FlowersCount[Index].ToString();
            }
            Plants[Index].Place(position);
        }
    }
    public void SetIndex(int index)
    {
        index--;
        Debug.Log($"индекс нажат:{index}");
        if(index < numberOfFlowers)
        {
            TutorialScript.FinishTutorial("change");
            Debug.Log($"Новый индекс:{index}");
            Index = index;
            scrollTransform.anchoredPosition = new Vector2((float)(Index - (numberOfFlowers - 1) / 2.0)*100, 0);
        }
    }

    public void SetNumberOfFlowers(int number)
    {
        numberOfFlowers = Math.Min(number,FlowersObjects.Length);
        RecreateFlowersUI();
        scrollTransform.anchoredPosition = new Vector2((float)(Index - (numberOfFlowers - 1) / 2.0)*100, 0);
    }
    public int GetNumberOfFlowers()
    {
        return numberOfFlowers;
    }

    public void ChangeNumberOfFlowers(int index)
    {
        FlowersCount[index]++;
        var textComponent = FlowersNumbers[index].GetComponent<TextMeshProUGUI>();
        textComponent.text = FlowersCount[index].ToString();
    }

    public void RecreateFlowersUI()
    {
        if(FlowersNumbers!=null)
        {
            for (var i = 0; i < numberOfFlowers; i++)
            {
                if(FlowersNumbers[i]!=null) Destroy(FlowersNumbers[i]);
                if(FlowersImages[i]!=null) Destroy(FlowersImages[i]);
                if(FlowersBackgrounds[i]!=null) Destroy(FlowersBackgrounds[i]);
            }
        }
        FlowersNumbers = new GameObject[numberOfFlowers];
        FlowersImages = new GameObject[numberOfFlowers];
        FlowersBackgrounds = new GameObject[numberOfFlowers];

        for(var i = 0; i < numberOfFlowers; i++)
        {
            var xLocation = (float)(i - (numberOfFlowers - 1) / 2.0)*100;

            //фон
            var backObj = CreateBasicObject(new Vector2(60, 60),new Vector2(xLocation, 0));
            var backImage = backObj.AddComponent<Image>();
            backImage.sprite = backSprite;

            FlowersBackgrounds[i] = backObj;


            //картинка
            var imageObj = CreateBasicObject(new Vector2(40, 40),new Vector2(xLocation, 0));
            var imageImage = imageObj.AddComponent<Image>();
            imageImage.sprite = FlowersObjects[i].GetComponent<SpriteRenderer>().sprite;

            FlowersImages[i] = imageObj;


            //текст
            var textObj = CreateBasicObject(new Vector2(60, 40),new Vector2(xLocation, -50));

            TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
            if(FlowersCount[i]>=0)
                text.text = FlowersCount[i].ToString();
            text.alignment = TextAlignmentOptions.Center;

            FlowersNumbers[i]=textObj;

        }
    }

    private GameObject CreateBasicObject(Vector2 size, Vector2 position)
    {
        GameObject obj = new GameObject();
        obj.transform.SetParent(Bar.transform,false);

        RectTransform transform = obj.AddComponent<RectTransform>();
        transform.sizeDelta = size;
        transform.anchoredPosition = position;
        obj.layer = LayerMask.NameToLayer("UI");

        return obj;
    }
}
