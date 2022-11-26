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


public class ChangePortalColour_phase1 : MonoBehaviour
{
    [SerializeField] Sprite[] fruitImages;
    [SerializeField] private Image fruitImage;
    public LoadFruits.Fruit currentFruit;
    int randomImage;
    private int index;
    
    private List<Color> ListColour_portal = new List<Color>();
    private List<Color> ListColour_fruit = new List<Color>();
    private FunctionTimer functionTimer;
    
    static public int rightPortal1;
    static public int rightPortal2;
    static public int rightPortal3;
    
    public static bool phase1 = false; 
    
    
    // Start is called before the first frame update
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
        if (phase1)
        {
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            selectRandomImage();
            CustomPalette();
            selectFruitColor();
            selectRandomColour();
            //print("phase bool: "+ phase0);
        }
    }
    
    
    
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState state)
    {
        if (state == MiniGameState.One)
        {
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            selectRandomImage();
            CustomPalette();
            selectFruitColor();
            selectRandomColour();
            //Destroy(this);
            phase1 = true;
            //print("Index: " + index);
        }
        if (state != MiniGameState.One)
        {
            phase1 = false;
        }
    }
    
    void selectRandomImage()
        {
            randomImage = Random2.Range(0, 6);
            fruitImage.sprite = fruitImages[randomImage];
            currentFruit = LoadFruits.myFruitList.fruit[randomImage];
    
        }
    
    void selectFruitColor()
        {
            index = Random2.Range(0, 3);
            fruitImage.GetComponent<Image>().color = ListColour_fruit[index];
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
        }
    
    void selectRandomColour()
        {
            Color currentColor = ListColour_portal[index];
            List<Color> tempColourList = ListColour_portal;
            tempColourList.RemoveAt(index);
    
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


