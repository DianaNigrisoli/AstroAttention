using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomFruit : MonoBehaviour
{
    [SerializeField] Sprite[] fruitImages;

   //[SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] private Image fruitImage;

    int randomImage;
    // Start is called before the first frame update
    void Start()
    {
        fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
        selectRandomImage();
    }

    void selectRandomImage()
    {
        randomImage = Random.Range(1, 7);
        //spriteRenderer.sprite = fruitImages[randomImage];
        fruitImage.sprite = fruitImages[randomImage];
        print(randomImage);
        //var fruit = LoadFruits.myFruitList.fruit[randomImage].name;
        //Debug.Log(fruit);

    }
}
