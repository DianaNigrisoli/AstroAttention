using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Timers;
using Random1 = System.Random;
using Random2 = UnityEngine.Random;


public class ChangePortalColour : MonoBehaviour
{
    [SerializeField] Sprite[] fruitImages;

    [SerializeField] private Image fruitImage;
    public LoadFruits.Fruit currentFruit;
    int randomImage;

    private List<Color> ListColour = new List<Color>(); 
    private FunctionTimer functionTimer;

    static public int rightPortal1;
    static public int rightPortal2;
    static public int rightPortal3;
    
    // ora ho provato a fare così per le fasi
    public int phase = 0;

    // Start is called before the first frame update
    void Start()
    {   
        
        fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
        selectRandomImage();
        CustomPalette();
        selectRandomColour();
        selectFruitColor();
    }
    

    void selectRandomImage()
    {
        randomImage = Random2.Range(0, 6);
        fruitImage.sprite = fruitImages[randomImage];
        currentFruit = LoadFruits.myFruitList.fruit[randomImage];

    }
    void selectFruitColor()
    {
        if (phase == 0)
        {
            fruitImage.material.color = Color.white;
        }

        if (phase == 1)
        {
            int index = Random2.Range(0, 3);
            fruitImage.material.color = ListColour[index];
        }
      
    }

    void CustomPalette()
    {
        ListColour.Add(new Color((LoadFruits.myFruitList.fruit[1].R) / 255, (LoadFruits.myFruitList.fruit[1].G) / 255,
            (LoadFruits.myFruitList.fruit[1].B) / 255, 0.5f));
        ListColour.Add(new Color((LoadFruits.myFruitList.fruit[0].R)/255, (LoadFruits.myFruitList.fruit[0].G)/255,
            (LoadFruits.myFruitList.fruit[0].B)/255, 0.5f));
        ListColour.Add(new Color((LoadFruits.myFruitList.fruit[3].R)/255, (LoadFruits.myFruitList.fruit[3].G)/255,    
            (LoadFruits.myFruitList.fruit[3].B)/255, 0.5f));
        ListColour.Add(new Color((LoadFruits.myFruitList.fruit[5].R)/255, (LoadFruits.myFruitList.fruit[5].G)/255,
            (LoadFruits.myFruitList.fruit[5].B)/255, 0.5f));
    }
    
    void selectRandomColour()
    {   
        Color currentColor = ListColour[currentFruit.ID];
        List<Color> tempColourList = ListColour;
        tempColourList.RemoveAt(currentFruit.ID);
        
        int tempindex = Random2.Range(0, 2); 
        tempColourList.RemoveAt(tempindex);
        
        //print("Size of tempColour: "+ tempColourList.Count);
        //print("POS 0: " + tempColourList[0]);
        //print("POS 1: " + tempColourList[1]);

        List<Color> PortalColour = tempColourList;
        PortalColour.Add(currentColor);
        
        //print("Size of Final Portal Colour: "+ PortalColour.Count);
        //print("PortalColour[0]: " +PortalColour[0]);
        //STEP 2:
        var rnd = new Random1();
        List<Color> ShufflePortalColour = PortalColour.OrderBy(item => rnd.Next()).ToList();

        //print("PortalColour[0] after Shuffle: " + ShufflePortalColour[0]);
        
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "Portal1")
            {   
                // Check if the color is the right one
                if (ShufflePortalColour[0] == currentColor)
                    rightPortal1 = 1;
                else rightPortal1 = 0;
                
                gameObj.GetComponent<Renderer>().material.color = ShufflePortalColour[0];
                
            }

            if (gameObj.name == "Portal2")
            {
                if (ShufflePortalColour[1] == currentColor)
                    rightPortal2 = 1;
                else rightPortal2 = 0;
                gameObj.GetComponent<Renderer>().material.color = ShufflePortalColour[1];
            }

            if (gameObj.name == "Portal3")
            {
                if (ShufflePortalColour[2] == currentColor)
                    rightPortal3 = 1;
                else rightPortal3 = 0;
                gameObj.GetComponent<Renderer>().material.color = ShufflePortalColour[2];
            }
        }
        
    }

  
    
    
}

