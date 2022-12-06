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


public class ChangePortalColour_phase2Tutorial : MonoBehaviour
{
    [SerializeField] Sprite[] fruitImages;
    [SerializeField] private Image fruitImage;
    [SerializeField] SpriteRenderer triangle1;
    [SerializeField] SpriteRenderer triangle2;
    [SerializeField] SpriteRenderer triangle3;
    [SerializeField] Sprite triangle;
    
    public LoadFruits.Fruit currentFruit;
    int randomImage;
    private int index_visCol;
    
    private List<Color> ListColour_portal = new List<Color>();
    private List<Color> ListColour_fruit = new List<Color>();
    private Color visibleColor;
    private Color semanticColor;
    private FunctionTimer functionTimer;
    
    static public int rightPortal1;
    static public int rightPortal2;
    static public int rightPortal3;
    
    
    
    public static bool phase2Tut = false; 
    
    
    // Start is called before the first frame update
    void Awake()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
        Portals.OnPortalSpawn += PortalSpawnerOnPortalSpawn;
    }
    void OnDestroy()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
        Portals.OnPortalSpawn -= PortalSpawnerOnPortalSpawn;
    } 
    
    private void PortalSpawnerOnPortalSpawn(int obj)
    {
        if (phase2Tut)
        {
            CustomPalette();
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            selectRandomImage();
            selectFruitColor();
            selectRandomColour();
            selectTriangle();
            //print("phase bool: "+ phase0);
        }
        
        //bisognare fare else -> destroy? 
    }
    
    
    
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
    {
        if (state == MiniGameStateFruit.TwoTutorial  )
        {
            CustomPalette();
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            selectRandomImage();
            selectFruitColor();
            selectRandomColour();
            selectTriangle();
            phase2Tut = true;
            //Destroy(this);
            //print("Index: " + index);
        }
        else
        {   
            rightPortal1 = 0;
            rightPortal2 = 0;
            rightPortal3 = 0;
            phase2Tut = false;
        }
    }
    void selectTriangle()
    {
        triangle1.enabled = true;
        triangle2.enabled = true;
        triangle3.enabled = true;
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
        //// In this phase the fruit in the canvas MUST NOT have its semantic colour /////
        // -> same for phase 1,2,3
        
        List<Color> tempColourList_fruit = ListColour_fruit;
            
        // semantic colour removed
        tempColourList_fruit.RemoveAt(currentFruit.ID);  // tempColourList has 4 elements now
        
        // selection of a random color between those remained
        index_visCol = Random2.Range(0, 4);
        visibleColor = tempColourList_fruit[index_visCol];
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
        ///// in this phase the semantic color of the fruit in the canvas MUST BE present in one portal ///////
        /// and also the visible color must be present in another portal
        
        semanticColor = ListColour_portal[currentFruit.ID]; //semantic color of fruit
        Color currentColor = new Color(visibleColor.r, visibleColor.g, visibleColor.b, (visibleColor.a - 0.5f)); //visible color on fruit
        List<Color> tempColourList = ListColour_portal;
        // Removed first 2 colors
        tempColourList.RemoveAll(t => t == semanticColor || t == currentColor); // now 3 color
        
        // third color removed
        int tempindex1 = Random2.Range(0, 3);
        tempColourList.RemoveAt(tempindex1);
        
        // fourth color removed
        int tempindex2 = Random2.Range(0, 2);
        tempColourList.RemoveAt(tempindex2);
        
        //Nel caso in cui aggiungiamo un altro colore o si fa un altro remove oppure si cambia metodo

        // added semantic and visible color
        tempColourList.Add(currentColor);
        tempColourList.Add(semanticColor);

        // shuffle the 3 elements list
        var rnd = new Random1();
        List<Color> ShufflePortalColour = tempColourList.OrderBy(item => rnd.Next()).ToList();
        

        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "Portal1")
            {
                // Check if the color is the right one
                if (ShufflePortalColour[0] == semanticColor)
                    rightPortal1 = 1;
                else rightPortal1 = 0;
                
                gameObj.GetComponent<Renderer>().material.color = ShufflePortalColour[0];
    
            }
    
            if (gameObj.name == "Portal2")
            {
                if (ShufflePortalColour[1] == semanticColor)
                    rightPortal2 = 1;
                else rightPortal2 = 0;
                
                gameObj.GetComponent<Renderer>().material.color = ShufflePortalColour[1];
            }
    
            if (gameObj.name == "Portal3")
            {
                if (ShufflePortalColour[2] == semanticColor)
                    rightPortal3 = 1;
                else rightPortal3 = 0;
                
                gameObj.GetComponent<Renderer>().material.color = ShufflePortalColour[2];
            }
        }
    
    }
    
    // vecchio codice
    // Color semanticColor = ListColour_portal[currentFruit.ID]; //semantic color of fruit
    // semanticColor = ListColour_portal[currentFruit.ID];
    // Color currentColor = new Color(visibleColor.r, visibleColor.g, visibleColor.b, (visibleColor.a - 0.5f)); //visible color on fruit
    // List<Color> tempColourList = ListColour_portal;

    // tempColourList.RemoveAll(t => t == semanticColor || t == currentColor);
        
    // int tempindex = Random2.Range(0, 2);
    // tempColourList.RemoveAt(tempindex);
        
    // List<Color> PortalColour = tempColourList;
    // PortalColour.Add(currentColor);
    // PortalColour.Add(semanticColor);
        
    // var rnd = new Random1();
    // List<Color> ShufflePortalColour = PortalColour.OrderBy(item => rnd.Next()).ToList();

    
    
    
    // first color selected: current color (SEMANTIC COLOR)
    // Color currentColor = ListColour_portal[currentFruit.ID];
    // List<Color> tempColourList = ListColour_portal;
    // tempColourList.RemoveAt(currentFruit.ID); // tempColourList has 4 elements now
        
    // // second color removed
    // int tempindex = Random2.Range(0, 4); 
    // tempColourList.RemoveAt(tempindex); //tempColourList has 3 elements now
    //
    // // third color removed
    // int tempindex2 = Random2.Range(0, 3); 
    // tempColourList.RemoveAt(tempindex2); //tempColourList has 2 elements now
    // // corrent color (SEMANTIC COLOR) added 
    //    PortalColour.Add(currentColor); 
    //

}
