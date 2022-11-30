using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomFruit : MonoBehaviour
{
    // DA CANCELLARE SE NON LO USIAMO PIU
    [SerializeField] Sprite[] fruitImages;

   //[SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Image MiniFruit;
    public static LoadFruits.Fruit currentFruit;
    int randomImage;

    void selectRandomImage()
    {
        randomImage = Random.Range(0, 6);
        //spriteRenderer.sprite = fruitImages[randomImage];
        MiniFruit.sprite = fruitImages[randomImage];
        print(randomImage);
        currentFruit = LoadFruits.myFruitList.fruit[randomImage];

    }
    // Start is called before the first frame update
    void Start()
    {
        MiniFruit = GameObject.Find("MiniFruit").GetComponent<Image>();
        selectRandomImage();
    }

    private void Update()
    {
    }
}
