using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChangePortalColour : MonoBehaviour
{   
    [SerializeField] Sprite[] fruitImages;
    
    [SerializeField] private Image fruitImage;
    public LoadFruits.Fruit currentFruit;
    int randomImage;
    private List<Color> ListColour = new List<Color>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
        selectRandomImage();
        CustomPalette();
        selectRandomColour();

    }
    
    void selectRandomImage()
    {
        randomImage = Random.Range(0, 6);
        fruitImage.sprite = fruitImages[randomImage];
        //print(randomImage);
        currentFruit = LoadFruits.myFruitList.fruit[randomImage];

    }

    void CustomPalette()
    {
        ListColour.Add(new Color(LoadFruits.myFruitList.fruit[1].R, LoadFruits.myFruitList.fruit[1].G,
            LoadFruits.myFruitList.fruit[1].B, LoadFruits.myFruitList.fruit[1].A));
        ListColour.Add(new Color(LoadFruits.myFruitList.fruit[0].R, LoadFruits.myFruitList.fruit[0].G,
            LoadFruits.myFruitList.fruit[0].B, LoadFruits.myFruitList.fruit[0].A));
        ListColour.Add(new Color(LoadFruits.myFruitList.fruit[3].R, LoadFruits.myFruitList.fruit[3].G,
            LoadFruits.myFruitList.fruit[3].B, LoadFruits.myFruitList.fruit[3].A));
        ListColour.Add(new Color(LoadFruits.myFruitList.fruit[5].R, LoadFruits.myFruitList.fruit[5].G,
            LoadFruits.myFruitList.fruit[5].B, LoadFruits.myFruitList.fruit[5].A));
    }
    void selectRandomColour()
    {
        Color currentColor = ListColour[currentFruit.ID];
        print(currentColor);
        //GameObject.Find("Portal3").GetComponent<Renderer>().material.color = Color.red;
        // var objects =Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Portal3");
        // foreach (GameObject j in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Portal3"))
        // {
        //     j.GetComponent<Renderer>().material.color = new Color(0, 0, 255, 255);
        // }

        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "Portal3")
            {
                gameObj.GetComponent<Renderer>().material.color = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f));

            }

            if (gameObj.name == "Portal2")
            {
                gameObj.GetComponent<Renderer>().material.color = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f));
            }

            if (gameObj.name == "Portal1")
            {
                gameObj.GetComponent<Renderer>().material.color = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f));
            }
        }

        selectRandomPortals();
        

    }

    void selectRandomPortals()
    {
        int randomPortal = Random.Range(1, 3);
    }
    
}
