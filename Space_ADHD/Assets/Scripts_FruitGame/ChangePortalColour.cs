using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Random1 = System.Random;
using Random2 = UnityEngine.Random;


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
        randomImage = Random2.Range(0, 6);
        fruitImage.sprite = fruitImages[randomImage];
        //print(randomImage);
        currentFruit = LoadFruits.myFruitList.fruit[randomImage];

    }

    void CustomPalette()
    {
        ListColour.Add(new Color((LoadFruits.myFruitList.fruit[1].R) / 255, (LoadFruits.myFruitList.fruit[1].G) / 255,
            (LoadFruits.myFruitList.fruit[1].B) / 255, (LoadFruits.myFruitList.fruit[1].A) / 255));
        ListColour.Add(new Color((LoadFruits.myFruitList.fruit[0].R)/255, (LoadFruits.myFruitList.fruit[0].G)/255,
            (LoadFruits.myFruitList.fruit[0].B)/255, (LoadFruits.myFruitList.fruit[0].A)/255));
        ListColour.Add(new Color((LoadFruits.myFruitList.fruit[3].R)/255, (LoadFruits.myFruitList.fruit[3].G)/255,    
            (LoadFruits.myFruitList.fruit[3].B)/255, (LoadFruits.myFruitList.fruit[3].A)/255));
        ListColour.Add(new Color((LoadFruits.myFruitList.fruit[5].R)/255, (LoadFruits.myFruitList.fruit[5].G)/255,
            (LoadFruits.myFruitList.fruit[5].B)/255, (LoadFruits.myFruitList.fruit[5].A)/255));
    }
    
    void selectRandomColour()
    {   
        // step1: creazione lista dei 3 colori DIVERSI per i 3 portali
            //[current, random1, random2] con random1 diverso da random2 
            //Lista di appoggio uguale a quella con tutti i nostri colori (CustomPalette), da questa togliamo il colore 
            //uguale al current color (usando il currentFruit.ID). 
            //estrazione senza reimmissione di un colore tra quelli rimasti 
            //i due colori rimanenti sono quelli per le due porte
            
        // step2:  Shuffle della lista dei 3 colori 
            //il primo si assegna al portale 1, il secondo al portale 2 ecc
        
        //NB: dividi per 255!!!! 
        
        //STEP 1:
        Color currentColor = ListColour[currentFruit.ID];
        List<Color> tempColourList = ListColour;
        tempColourList.RemoveAt(currentFruit.ID);
        
        int tempindex = Random2.Range(0, 2); 
        tempColourList.RemoveAt(tempindex);
        
        print("Size of tempColour: "+ tempColourList.Count);
        print("POS 0: " + tempColourList[0]);
        print("POS 1: " + tempColourList[1]);

        List<Color> PortalColour = tempColourList;
        PortalColour.Add(currentColor);
        
        print("Size of Final Portal Colour: "+ PortalColour.Count);
        print("PortalColour[0]: " +PortalColour[0]);
        //STEP 2:
        var rnd = new Random1();
        List<Color> ShufflePortalColour = PortalColour.OrderBy(item => rnd.Next()).ToList();

        print("PortalColour[0] after Shuffle: " + ShufflePortalColour[0]);
        
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "Portal1")
            {
                gameObj.GetComponent<Renderer>().material.color = ShufflePortalColour[0];
            }

            if (gameObj.name == "Portal2")
            {
                gameObj.GetComponent<Renderer>().material.color = ShufflePortalColour[1];
            }

            if (gameObj.name == "Portal3")
            {
                gameObj.GetComponent<Renderer>().material.color = ShufflePortalColour[2];
            }
        }
    }
}

