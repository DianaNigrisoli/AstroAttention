using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFruit : MonoBehaviour
{
    [SerializeField] Sprite[] fruitImages;

    [SerializeField] SpriteRenderer spriteRenderer;

    int randomImage;
    // Start is called before the first frame update
    void Start()
    {
        selectRandomImage();
    }

    void selectRandomImage()
    {
        randomImage = Random.Range(1, 7);
        spriteRenderer.sprite = fruitImages[randomImage];
        print(randomImage);
    }
}
