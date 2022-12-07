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
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;
using Random1 = System.Random;
using Random2 = UnityEngine.Random;


public class ChangePortalColour_phase0Tutorial : MonoBehaviour
{
    [SerializeField] Sprite[] fruitImages;

    [SerializeField] private Image fruitImage;
    [SerializeField] SpriteRenderer triangle1;
    [SerializeField] SpriteRenderer triangle2;
    [SerializeField] SpriteRenderer triangle3;
    [SerializeField] Sprite triangle;
    

    public LoadFruits.Fruit currentFruit;
    int randomImage;

    private List<Color> ListColour_portal = new List<Color>(); 
    private List<Color> ListColour_fruit = new List<Color>();
    private Color visibleColor;
    
    private FunctionTimer functionTimer;

    static public int rightPortal1;
    static public int rightPortal2;
    static public int rightPortal3;

  
    public static bool phase0 = false; 
    public static bool phase0Tut = false;


    void Awake()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
        Portals.OnPortalSpawn += PortalSpawnerOnPortalSpawn;
    }
    
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(0.02f);
        selectTriangle();   
        phase0Tut = true;
    }

    void OnDestroy()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
        Portals.OnPortalSpawn -= PortalSpawnerOnPortalSpawn;
    }
    
    private void PortalSpawnerOnPortalSpawn(int obj)
    {
        if(phase0Tut)
        {
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            selectRandomImage();
            CustomPalette();
            selectFruitColor();
            selectRandomColour();
            selectTriangle();
        }
        //bisogna fare else destroy??
    }

    
    // Start is called before the first frame update
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
    {
        if (state == MiniGameStateFruit.ZeroTutorial)
        {
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            selectRandomImage();
            CustomPalette();
            selectFruitColor();
            selectRandomColour();
            StartCoroutine(waiter());
            
        }
       
        else
        {
            triangle1.enabled = false;
            triangle2.enabled = false;
            triangle3.enabled = false;
            phase0Tut = false;
            rightPortal1 = 0;
            rightPortal2 = 0;
            rightPortal3 = 0;
        }
    }

    void selectTriangle()
    {
        triangle1.enabled = true;
        triangle2.enabled = true;
        triangle3.enabled = true;
        print("portals: "+ rightPortal1+ rightPortal2 + rightPortal3);
        if (rightPortal1 == 1)
            triangle1.sprite = triangle;
        if (rightPortal2 == 1)
            triangle2.sprite = triangle;
        if (rightPortal3 == 1)
            triangle3.sprite = triangle;
    }
    

   
    void selectRandomImage()
    {
        randomImage = Random2.Range(0, 10);
        fruitImage.sprite = fruitImages[randomImage];
        currentFruit = LoadFruits.myFruitList.fruit[randomImage];

    }


    void selectFruitColor()
    {
        visibleColor = ListColour_fruit[currentFruit.ID];
        fruitImage.GetComponent<Image>().color = visibleColor;
    }
    
    
    void CustomPalette()
    {
        ListColour_portal.Add(new Color((LoadFruits.myFruitList.fruit[1].R) / 255,
                (LoadFruits.myFruitList.fruit[1].G) / 255,
                (LoadFruits.myFruitList.fruit[1].B) / 255, 0.5f));
        ListColour_portal.Add(new Color((LoadFruits.myFruitList.fruit[0].R) / 255,
                (LoadFruits.myFruitList.fruit[0].G) / 255,
                (LoadFruits.myFruitList.fruit[0].B) / 255, 0.5f));
        ListColour_portal.Add(new Color((LoadFruits.myFruitList.fruit[3].R) / 255,
            (LoadFruits.myFruitList.fruit[3].G) / 255,
            (LoadFruits.myFruitList.fruit[3].B) / 255, 0.5f));
        ListColour_portal.Add(new Color((LoadFruits.myFruitList.fruit[5].R) / 255,
            (LoadFruits.myFruitList.fruit[5].G) / 255,
            (LoadFruits.myFruitList.fruit[5].B) / 255, 0.5f));
        ListColour_portal.Add(new Color((LoadFruits.myFruitList.fruit[7].R)/255, 
            (LoadFruits.myFruitList.fruit[7].G)/255,
            (LoadFruits.myFruitList.fruit[7].B)/255, 0.5f));
        
        ListColour_fruit.Add(new Color((LoadFruits.myFruitList.fruit[1].R) / 255,
            (LoadFruits.myFruitList.fruit[1].G) / 255,
            (LoadFruits.myFruitList.fruit[1].B) / 255, 1f));
        ListColour_fruit.Add(new Color((LoadFruits.myFruitList.fruit[0].R) / 255,
            (LoadFruits.myFruitList.fruit[0].G) / 255,
            (LoadFruits.myFruitList.fruit[0].B) / 255, 1f));
        ListColour_fruit.Add(new Color((LoadFruits.myFruitList.fruit[3].R) / 255,
            (LoadFruits.myFruitList.fruit[3].G) / 255,
            (LoadFruits.myFruitList.fruit[3].B) / 255, 1f));
        ListColour_fruit.Add(new Color((LoadFruits.myFruitList.fruit[5].R) / 255,
            (LoadFruits.myFruitList.fruit[5].G) / 255,
            (LoadFruits.myFruitList.fruit[5].B) / 255, 1f));
        ListColour_fruit.Add(new Color((LoadFruits.myFruitList.fruit[7].R)/255, 
            (LoadFruits.myFruitList.fruit[7].G)/255,
            (LoadFruits.myFruitList.fruit[7].B)/255, 1f));
    }
    
    void selectRandomColour()
    {   
        /////// In this phase the semantic color of the fruit in the canvas MUST BE present in one of the portal /////
        
        // first color selected: current color is the SEMANTIC COLOR
        Color currentColor = ListColour_portal[currentFruit.ID];
        List<Color> tempColourList = ListColour_portal;
        tempColourList.RemoveAt(currentFruit.ID); // tempColourList has 4 elements now
        
        // second color removed
        int tempindex = Random2.Range(0, 4); 
        tempColourList.RemoveAt(tempindex); //tempColourList has 3 elements now
        
        // third color removed
        int tempindex2 = Random2.Range(0, 3); 
        tempColourList.RemoveAt(tempindex2); //tempColourList has 2 elements now
       
        List<Color> PortalColour = tempColourList; // 2 elements 
        
        // corrent color (SEMANTIC COLOR)  added 
        PortalColour.Add(currentColor); 
        
        // shuffle the 3 elements list
        var rnd = new Random1();
        List<Color> ShufflePortalColour = PortalColour.OrderBy(item => rnd.Next()).ToList();
        
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

