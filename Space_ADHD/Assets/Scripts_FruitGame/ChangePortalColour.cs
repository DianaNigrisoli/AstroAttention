using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random1 = System.Random;
using Random2 = UnityEngine.Random;

public class ChangePortalColour : MonoBehaviour
{
    [SerializeField] Sprite[] fruitImages;

    [SerializeField] private Image fruitImage;
    public LoadFruits.Fruit currentFruit;
    int randomImage;
    private List<float>[] ListColour = new List<float>[4];
    //private Random rng = new Random();  


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
        randomImage = Random2.Range(0, 6);
        fruitImage.sprite = fruitImages[randomImage];
        //print(randomImage);
        currentFruit = LoadFruits.myFruitList.fruit[randomImage];

    }

    void CustomPalette()
    {
        ListColour[0] = new List<float>
        {
            LoadFruits.myFruitList.fruit[1].R, LoadFruits.myFruitList.fruit[1].G,
            LoadFruits.myFruitList.fruit[1].B, LoadFruits.myFruitList.fruit[1].A
        };

        ListColour[1] = new List<float>
        {
            LoadFruits.myFruitList.fruit[0].R, LoadFruits.myFruitList.fruit[0].G,
            LoadFruits.myFruitList.fruit[0].B, LoadFruits.myFruitList.fruit[0].A
        };

        ListColour[2] = new List<float>
        {
            LoadFruits.myFruitList.fruit[3].R, LoadFruits.myFruitList.fruit[3].G,
            LoadFruits.myFruitList.fruit[3].B, LoadFruits.myFruitList.fruit[3].A
        };

        ListColour[3] = new List<float>
        {
            LoadFruits.myFruitList.fruit[5].R, LoadFruits.myFruitList.fruit[5].G,
            LoadFruits.myFruitList.fruit[5].B, LoadFruits.myFruitList.fruit[5].A
        };
    }

    void selectRandomColour()
    {   
        // step1: creazione lista dei 3 colori DIVERSI per i 3 portali
            //[current, random1, random2] con random1 diverso da random2 
            //Lista di appoggio uguale a quella con tutti i nostri colori (CostumPalette), da questa togliamo il colore 
            //uguale al current color (usando il currentFruit.ID). 
            //estrazione senza reimmissione di un colore tra quelli rimasti 
            //i due colori rimanenti sono quelli per le due porte
            
        // step2:  Shuffle della lista dei 3 colori 
            //il primo si assegna al portale 1, il secondo al portale 2 ecc
        
        //NB: dividi per 255!!!! 
        
        List<float> currentColor = ListColour[currentFruit.ID];
        

        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "Portal3")
            {
                Color myColor = new Color(currentColor[0] / 255, currentColor[1] / 255, currentColor[2] / 255,
                    currentColor[3] / 255);
                gameObj.GetComponent<Renderer>().material.color = myColor;
            }

            if (gameObj.name == "Portal2")
            {
                gameObj.GetComponent<Renderer>().material.color = new Color(
                    Random2.Range(0f, 1f),
                    Random2.Range(0f, 1f),
                    Random2.Range(0f, 1f));
            }

            if (gameObj.name == "Portal1")
            {
                gameObj.GetComponent<Renderer>().material.color = new Color(
                    Random2.Range(0f, 1f),
                    Random2.Range(0f, 1f),
                    Random2.Range(0f, 1f));
            }
        }

        //selectRandomPortals();
        
    }

    void selectRandomPortals()
    {
        int randomPortal = Random2.Range(1, 3);
    }


    
    public static void ShuffleMe<T>(IList<T> list)  
    {  
        Random1 random = new Random1();  
        int n = list.Count;  

        for(int i= list.Count - 1; i > 1; i--)
        {
            int rnd = random.Next(i + 1);  

            T value = list[rnd];  
            list[rnd] = list[i];  
            list[i] = value;
        }
    }


    
    
}
