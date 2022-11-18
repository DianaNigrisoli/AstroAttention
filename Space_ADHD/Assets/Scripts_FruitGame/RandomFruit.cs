using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomFruit : MonoBehaviour
{
    [SerializeField] Sprite[] fruitImages;

   //[SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Image fruitImage;
    public static LoadFruits.Fruit currentFruit;
    int randomImage;

    void selectRandomImage()
    {
        randomImage = Random.Range(0, 6);
        //spriteRenderer.sprite = fruitImages[randomImage];
        fruitImage.sprite = fruitImages[randomImage];
        print(randomImage);
        currentFruit = LoadFruits.myFruitList.fruit[randomImage];

    }
    // Start is called before the first frame update
    void Start()
    {
        fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
        selectRandomImage();
    }

    private void Update()
    {
    }
}
