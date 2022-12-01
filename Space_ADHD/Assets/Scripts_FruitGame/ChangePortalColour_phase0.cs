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
using Random1 = System.Random;
using Random2 = UnityEngine.Random;


public class ChangePortalColour_phase0 : MonoBehaviour
{
    [SerializeField] Sprite[] fruitImages;

    [SerializeField] private Image fruitImage;
    [SerializeField] SpriteRenderer MiniFruitRender;
    
    public LoadFruits.Fruit currentFruit;
    public LoadFruits.Fruit currentMiniFruit;
    int randomImage;

    private List<Color> ListColour = new List<Color>(); 
    private FunctionTimer functionTimer;

    static public int rightPortal1;
    static public int rightPortal2;
    static public int rightPortal3;

    public GameObject ParentGameObject;
    public static bool phase0 = false; 
    
    
    void Awake()
    {
        MiniGameManager.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
        Portals.OnPortalSpawn += PortalSpawnerOnPortalSpawn;
    }
    void OnDestroy()
    {
        MiniGameManager.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
        Portals.OnPortalSpawn -= PortalSpawnerOnPortalSpawn;
    }
    
    private void PortalSpawnerOnPortalSpawn(int obj)
    {
        if (phase0)
        {
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            //MiniFruitRender = GameObject.Find("MiniFruit").GetComponent<SpriteRenderer>();
            selectRandomImage();
            //selectMiniImage();
            CustomPalette();
            selectRandomColour();
            selectFruitColor();
            //print("phase bool: "+ phase0);
        }
        
        //bisogna fare else destroy??
    }

    
    // Start is called before the first frame update
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState state)
    {
        if (state == MiniGameState.Zero)
        {
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            //MiniFruitRender = GameObject.Find("MiniFruit").GetComponent<SpriteRenderer>();
            selectRandomImage();
            //selectMiniImage();
            CustomPalette();
            selectRandomColour();
            selectFruitColor();
            phase0 = true;
        }
        else
        {
            phase0 = false;
        }
    }
    

    void selectRandomImage()
    {
        randomImage = Random2.Range(0, 6);
        fruitImage.sprite = fruitImages[randomImage];
        currentFruit = LoadFruits.myFruitList.fruit[randomImage];

    }


    // void selectMiniImage()
    // {
    //    randomImage = Random2.Range(0, 6);
    //    MiniFruitRender.sprite= fruitImages[randomImage];
    //    currentMiniFruit = LoadFruits.myFruitList.fruit[randomImage];
    //    print(currentMiniFruit);
    //
    // }

    void selectFruitColor()
    {
        fruitImage.material.color = Color.white;
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
        
       
        List<Color> PortalColour = tempColourList;
        PortalColour.Add(currentColor);
        
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

