using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FlowersManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] FlowersObjects;
    [SerializeField] private int[] FlowersCount;
    [SerializeField] private Sprite scrollSprite;
    [SerializeField] private int Index = 0;
    [SerializeField] private int numberOfFlowers;
    [SerializeField] private GameObject Bar;

    private GameObject[] FlowersNumbers;
    private RectTransform scrollTransform;

    private void Start()
    {

        GameObject scrollObject = new GameObject("scroll");
        
        scrollObject.transform.SetParent(Bar.transform,false);

        scrollTransform = scrollObject.AddComponent<RectTransform>();
        scrollTransform.sizeDelta = new Vector2(50, 50);
        scrollTransform.anchoredPosition = new Vector2((float)(Index - (numberOfFlowers - 1) / 2.0)*100, 0);

        Image scrollImage = scrollObject.AddComponent<Image>();
        scrollImage.sprite = scrollSprite;
        scrollObject.layer = LayerMask.NameToLayer("UI");

        RecreateFlowersUI();
    }
    public void PlaceFlower(Vector2 position)
    {
        if(Index >= numberOfFlowers || FlowersObjects[Index]==null)
        {
            Debug.Log("Цветка под таким индексом нет");
            return;
        }
        if(FlowersCount[Index] == 0)
        {
            Debug.Log("Закончились");
            return;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);

        bool canPlace = true;
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("PlayerMinion"))
            {
                canPlace = false;
                break;
            }
        }

        if (canPlace)
        {
            FlowersCount[Index]--;
            if(FlowersCount[Index] >= 0)
            {
                var textComponent = FlowersNumbers[Index].GetComponent<TextMeshProUGUI>();
                textComponent.text = FlowersCount[Index].ToString();
            }
            GameObject minion = (GameObject)Instantiate(FlowersObjects[Index], position, Quaternion.Euler(0, 0, 0));
            minion.tag = "PlayerMinion";
            if (minion.TryGetComponent<Plant>(out var plant))
            {
                plant.isSample = false;
            }
        }
    }
    public void SetIndex(int index)
    {
        index--;
        if(index < numberOfFlowers && FlowersObjects[index]!=null)
        {
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

    public void RecreateFlowersUI()
    {
        if(FlowersNumbers!=null)
        {
            foreach (var number in FlowersNumbers)
            {
                if (number!=null) Destroy(number);
            }
        }
        FlowersNumbers = new GameObject[numberOfFlowers];

        for(var i = 0; i < numberOfFlowers; i++)
        {
            var xLocation = (float)(i - (numberOfFlowers - 1) / 2.0)*100;

            //картинка
            GameObject imageObject = new GameObject($"NewImage{i}");
            imageObject.transform.SetParent(Bar.transform,false);

            RectTransform imageTransform = imageObject.AddComponent<RectTransform>();
            imageTransform.sizeDelta = new Vector2(40, 40);
            imageTransform.anchoredPosition = new Vector2(xLocation, 0);

            Image image = imageObject.AddComponent<Image>();
            if(FlowersObjects[i]!=null)
                image.sprite = FlowersObjects[i].GetComponent<SpriteRenderer>().sprite;
            imageObject.layer = LayerMask.NameToLayer("UI");




            //текст
            GameObject textObject = new GameObject($"NewText{i}");
            textObject.transform.SetParent(Bar.transform,false);

            RectTransform textTransform = textObject.AddComponent<RectTransform>();
            textTransform.sizeDelta = new Vector2(60, 40);
            textTransform.anchoredPosition = new Vector2(xLocation, -50);

            TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
            if(FlowersCount[i]>=0)
                text.text = FlowersCount[i].ToString();
            text.alignment = TextAlignmentOptions.Center;
            textObject.layer = LayerMask.NameToLayer("UI");

            FlowersNumbers[i]=textObject;

        }
    }
}
