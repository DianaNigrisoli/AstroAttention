using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;
using UnityEngine;
using UnityEngine.UI;
using Random1 = System.Random;
using Random2 = UnityEngine.Random;

public class ChangePortalColour_phase3Tutorial : MonoBehaviour
{
      [SerializeField] Sprite[] fruitImages;

    [SerializeField] private Image fruitImage;
    [SerializeField] SpriteRenderer MiniFruitRender1;
    [SerializeField] SpriteRenderer MiniFruitRender2;
    [SerializeField] SpriteRenderer MiniFruitRender3;
    [SerializeField] GameObject myportal1;
    [SerializeField] GameObject myportal2;
    [SerializeField] GameObject myportal3;
    [SerializeField] SpriteRenderer triangle1;
    [SerializeField] SpriteRenderer triangle2;
    [SerializeField] SpriteRenderer triangle3;
    [SerializeField] Sprite triangle;
    
    private List<Color> ListColour_portal = new List<Color>();
    private List<Color> ListColour_fruit = new List<Color>();
    public static bool phase3Tut = false; 
    int randomImageCanvas;
    int miniFruitCorrectColour;
    int randomImagePortal2;
    int randomImagePortal3;
    private int indexColourCanvas;
    public int index_visCol;
    private Color visibleColor;
    public LoadFruits.Fruit currentFruit;
    public LoadFruits.Fruit currentMiniFruit;
    List<int> miniFruitVector = new List<int>();
    
    static public int rightPortal1;
    static public int rightPortal2;
    static public int rightPortal3;
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
        if (phase3Tut)
        {
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            selectRandomImage();
            CustomPalette();
            selectFruitColor();
            selectMiniImage();
            selectRandomColour();
            selectTriangle();
        }
        
    }
    
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
    {
        if (state == MiniGameStateFruit.ThreeTutorial)
        {
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            selectRandomImage();
            CustomPalette();
            selectFruitColor();
            selectMiniImage();
            selectRandomColour();
            selectTriangle();
            phase3Tut = true;
        }
        else
        {
            rightPortal1 = 0;
            rightPortal2 = 0;
            rightPortal3 = 0;
            phase3Tut = false;
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
        randomImageCanvas = Random2.Range(0, 10);
        fruitImage.sprite = fruitImages[randomImageCanvas];
        currentFruit = LoadFruits.myFruitList.fruit[randomImageCanvas];

    }
    
    void CustomPalette()
    {
        ListColour_portal.Add(new Color((LoadFruits.myFruitList.fruit[1].R) / 255,
            (LoadFruits.myFruitList.fruit[1].G) / 255,
            (LoadFruits.myFruitList.fruit[1].B) / 255, 0.9f));
        ListColour_portal.Add(new Color((LoadFruits.myFruitList.fruit[0].R) / 255,
            (LoadFruits.myFruitList.fruit[0].G) / 255,
            (LoadFruits.myFruitList.fruit[0].B) / 255, 0.9f));
        ListColour_portal.Add(new Color((LoadFruits.myFruitList.fruit[3].R) / 255,
            (LoadFruits.myFruitList.fruit[3].G) / 255,
            (LoadFruits.myFruitList.fruit[3].B) / 255, 0.9f));
        ListColour_portal.Add(new Color((LoadFruits.myFruitList.fruit[5].R) / 255,
            (LoadFruits.myFruitList.fruit[5].G) / 255,
            (LoadFruits.myFruitList.fruit[5].B) / 255, 0.9f));
        ListColour_portal.Add(new Color((LoadFruits.myFruitList.fruit[7].R)/255, 
            (LoadFruits.myFruitList.fruit[7].G)/255,
            (LoadFruits.myFruitList.fruit[7].B)/255, 0.9f));
            
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
    void selectFruitColor()
    {
        //// In this phase the fruit MUST NOT have its semantic colour
        // -> same for phase 1,2,3
        
        //List<Color> tempColourList_fruit = ListColour_fruit;
            
        // semantic colour removed
        //tempColourList_fruit.RemoveAt(currentFruit.ID);  // tempColourList has 4 elements now
        
        // selection of a random color between those remained
        //index_visCol = Random2.Range(0, 4);
        //visibleColor = tempColourList_fruit[index_visCol];
        //fruitImage.GetComponent<Image>().color = visibleColor;
        // restore the index of visible color
        //if (currentFruit.ID < index_visCol)
        //{
        //    index_visCol += 1;
        //}
        
         List<Color> tempColourList_fruit = ListColour_fruit;
         //tempColourList_fruit.RemoveAt(currentFruit.ID);
         indexColourCanvas = Random2.Range(0, 5);
         while (indexColourCanvas == currentFruit.ID)
         {
                     indexColourCanvas = Random2.Range(0, 5);
         }
         visibleColor = tempColourList_fruit[indexColourCanvas];
         fruitImage.GetComponent<Image>().color = visibleColor;

    }
    void selectMiniImage()
    {
       /////// CODICE PROVA LAURA ///////
        // divion of fruit: creation of 2 list of indexis that indicate the fruits
        // List<int> fruit_visCol;
        // List<int> fruit_diffCol;
        
        // for (int i = 0; i < 9; i++)
        // {
        //     if (LoadFruits.myFruitList.fruit[i].ID == index_visCol) // se il colore della frutta è uguale a quello visibile 
        //     {
        //         fruit_visCol.Add(i);
        //     }
        //     else
        //     {
        //         fruit_diffCol.Add((i));
        //     }
        

         //// CODICE CHE FUNZIA ///
         //// in this phase the images shown must be:
         //      1. a fruit which semantic color is the visible one without that colour 
         //      2. the fruit visible in the canvas with a DIFFERENT colour of the canvas
         //      3. a fruit which semantic color IS NOT the visible one with a random color
         
         miniFruitCorrectColour = Random2.Range(0, 9);
         randomImagePortal2 = randomImageCanvas; //Random2.Range(0, 9), same fruit in the canvas
         randomImagePortal3 = Random2.Range(0, 9);
         
         // first fruit selected 
         while ((miniFruitCorrectColour == randomImageCanvas) ||  
                (indexColourCanvas != LoadFruits.myFruitList.fruit[miniFruitCorrectColour].ID ) )
         {
             miniFruitCorrectColour = Random2.Range(0, 9); //image that corresponds to the colour of the canvas
         }
        
         // while ((randomImagePortal2 == randomImagePortal1) || 
         //        (randomImagePortal2 == randomImageCanvas) || 
         //        (indexColourCanvas == LoadFruits.myFruitList.fruit[randomImagePortal2].ID ) )
         // {
         //     randomImagePortal2 = Random2.Range(0, 9);
         // }
        
         while ((randomImagePortal3 == miniFruitCorrectColour) || 
                (randomImagePortal3 == randomImagePortal2) || 
                (randomImagePortal3 == randomImageCanvas) || 
                (indexColourCanvas == LoadFruits.myFruitList.fruit[randomImagePortal3].ID ))
         {
             randomImagePortal3 = Random2.Range(0, 9);
         }
         miniFruitVector.Add(miniFruitCorrectColour);
         miniFruitVector.Add(randomImagePortal2);
         miniFruitVector.Add(randomImagePortal3);
         var rnd = new Random1();
         miniFruitVector = miniFruitVector.OrderBy(item => rnd.Next()).ToList();
        
         // minifruit1
         MiniFruitRender1.sprite= fruitImages[miniFruitVector[0]];
         if (miniFruitVector[0] == miniFruitCorrectColour)
             rightPortal1 = 1;
         else rightPortal1 = 0;
         
         // minifruit2
         MiniFruitRender2.sprite= fruitImages[miniFruitVector[1]];
         if (miniFruitVector[1] == miniFruitCorrectColour)
             rightPortal2 = 1;
         else rightPortal2 = 0;
         
         // minifruit3
         MiniFruitRender3.sprite= fruitImages[miniFruitVector[2]];
         if (miniFruitVector[2] == miniFruitCorrectColour)
             rightPortal3 = 1;
         else rightPortal3 = 0;
         


        //currentMiniFruit = LoadFruits.myFruitList.fruit[randomImagePortal1];
        // print(" Portale1: "+ LoadFruits.myFruitList.fruit[randomImagePortal1].name);
        // print(" Portale2: "+ LoadFruits.myFruitList.fruit[randomImagePortal2].name);
        // print(" Portale3: "+ LoadFruits.myFruitList.fruit[randomImagePortal3].name);
        
    }

    void selectRandomColour()
    {
        //List<Color> tempColourMiniFruit = ListColour_fruit;
        int indexColourMinifruit1 = Random2.Range(0, 5);
        int indexColourMinifruit2 = Random2.Range(0, 5);
        int indexColourMinifruit3 = Random2.Range(0, 5);
        
        if ((miniFruitVector[0]== miniFruitCorrectColour) || (miniFruitVector[0] == randomImageCanvas))
        {
            while ((indexColourMinifruit1 == indexColourCanvas) || 
                   (indexColourMinifruit1 == indexColourMinifruit2) || 
                   (indexColourMinifruit1 == indexColourMinifruit3))
            {
                indexColourMinifruit1 = Random2.Range(0, 5);
            }
        }
        else
        {
            while ((indexColourMinifruit1 == indexColourMinifruit2) || (indexColourMinifruit1 == indexColourMinifruit3))
            {
                indexColourMinifruit1 = Random2.Range(0, 5);
            }
        }
      
        if ((miniFruitVector[1]== miniFruitCorrectColour) || (miniFruitVector[1] == randomImageCanvas))
        {
            while ((indexColourMinifruit2 == indexColourCanvas) || 
                   (indexColourMinifruit2 == indexColourMinifruit1) || 
                   (indexColourMinifruit2 == indexColourMinifruit3))
            {
                indexColourMinifruit2 = Random2.Range(0, 5);
            }
        }
        else
        {
            while ((indexColourMinifruit2 == indexColourMinifruit1) || (indexColourMinifruit2 == indexColourMinifruit3))
            {
                indexColourMinifruit2 = Random2.Range(0, 5);
            }
        }

        if ((miniFruitVector[2]== miniFruitCorrectColour) || (miniFruitVector[2] == randomImageCanvas))
        {
            while ((indexColourMinifruit3 == indexColourCanvas) ||
                   (indexColourMinifruit3 == indexColourMinifruit1) || 
                   (indexColourMinifruit3 == indexColourMinifruit2))
            {
                indexColourMinifruit3 = Random2.Range(0, 5);
            }  
        }
        else
        {
            while ((indexColourMinifruit3 == indexColourMinifruit1) || (indexColourMinifruit3 == indexColourMinifruit2))
            {
                indexColourMinifruit3 = Random2.Range(0, 5);
            }
        }

        MiniFruitRender1.color = ListColour_fruit[indexColourMinifruit1];
        MiniFruitRender2.color = ListColour_fruit[indexColourMinifruit2];
        MiniFruitRender3.color = ListColour_fruit[indexColourMinifruit3];
        // myportal1.GetComponent<Renderer>().material.color = ListColour_portal[indexColourMinifruit1];
        // myportal2.GetComponent<Renderer>().material.color = ListColour_portal[indexColourMinifruit2];
        // myportal3.GetComponent<Renderer>().material.color = ListColour_portal[indexColourMinifruit3];
        myportal1.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0f);
        myportal2.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0f);
        myportal3.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0f);
    }
}

