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
    public int index_visCol;
    
    private List<Color> ListColour_portal = new List<Color>();
    private List<Color> ListColour_fruit = new List<Color>();
    private Color visibleColor;
    private Color semanticColor;
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
        
        //bisognare fare else -> destroy? 
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
        }
        else
        {
            phase1 = false;
            rightPortal1 = 0;
            rightPortal2 = 0;
            rightPortal3 = 0;
        }
    }
    
    void selectRandomImage()
        {
            randomImage = Random2.Range(0, 9);
            fruitImage.sprite = fruitImages[randomImage];
            currentFruit = LoadFruits.myFruitList.fruit[randomImage];
    
        }
    
    void selectFruitColor()
        {
            //// In this phase the fruit in the canvas MUST NOT have its semantic colour ////
            // -> same for phase 1,2,3
            
            List<Color> tempColourList_fruit = ListColour_fruit;
            
            // semantic colour removed
            tempColourList_fruit.RemoveAt(currentFruit.ID);  // tempColourList has 4 elements now
        
            // selection of a random color between those remained
            index_visCol = Random2.Range(0, 4);
            visibleColor = tempColourList_fruit[index_visCol];
            fruitImage.GetComponent<Image>().color = visibleColor;
            // restore the index of visible color
            if (currentFruit.ID < index_visCol)
            {
                index_visCol += 1;
            }
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
            ListColour_portal.Add(new Color((LoadFruits.myFruitList.fruit[7].R)/255, (LoadFruits.myFruitList.fruit[7].G)/255,
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
            ListColour_fruit.Add(new Color((LoadFruits.myFruitList.fruit[7].R)/255, (LoadFruits.myFruitList.fruit[7].G)/255,
                (LoadFruits.myFruitList.fruit[7].B)/255, 1f));
        }
    
    void selectRandomColour()
        {
            ////// In this phase the visible colour of the fruit in the canvas MUST BE PRESENT in one portal ///////
            //// the semantic color of the fruit MUST NOT BE PRESENT in the portal /// 

            //print("vis:" + index_visCol);
           // print("sem:" + currentFruit.ID);
            
            semanticColor = ListColour_portal[currentFruit.ID];
            Color currentColor = new Color(visibleColor.r, visibleColor.g, visibleColor.b, (visibleColor.a - 0.5f)); //visible color on fruit
            List<Color> tempColourList = ListColour_portal;
            
            // first color removed: visible color 
            tempColourList.RemoveAt(index_visCol); // tempColourList has 4 elements now
            
            // second color removed: semantic color: the position depend also on the values of index_visCol

            if (index_visCol < currentFruit.ID)
            {
                tempColourList.RemoveAt(currentFruit.ID-1); // tempColourList has 3 elements now
            }

            else 
            {
                tempColourList.RemoveAt((currentFruit.ID)); // tempColourList has 3 elements now
            }
        
            // third color removed: 
            int tempindex = Random2.Range(0, 3);
            tempColourList.RemoveAt(tempindex); // tempColourList has 2 elements now
            
            List<Color> PortalColour = tempColourList; 
            
            // visible color   added 
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
    
    // codice vecchio 
    //semanticColor = ListColour_portal[currentFruit.ID];
    //Color currentColor = new Color(visibleColor.r, visibleColor.g, visibleColor.b, (visibleColor.a - 0.5f)); //visible color on fruit
    //List<Color> tempColourList = ListColour_portal;
            
    // remove current color
    //tempColourList.RemoveAll(t => t == semanticColor);
        
    // remove 4th color: 3 remain
    //int tempindex = Random2.Range(0, 3);
    //tempColourList.RemoveAt(tempindex);

    //List<Color> PortalColour = tempColourList;

}


