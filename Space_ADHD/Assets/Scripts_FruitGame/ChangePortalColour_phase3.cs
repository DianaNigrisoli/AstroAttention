using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts_A_General;
using UnityEngine;
using UnityEngine.UI;
using Random1 = System.Random;
using Random2 = UnityEngine.Random;

public class ChangePortalColour_phase3 : MonoBehaviour
{
    [SerializeField] Sprite[] fruitImages;

    [SerializeField] private Image fruitImage;
    [SerializeField] SpriteRenderer MiniFruitRender1;
    [SerializeField] SpriteRenderer MiniFruitRender2;
    [SerializeField] SpriteRenderer MiniFruitRender3;
    private List<Color> ListColour_portal = new List<Color>();
    private List<Color> ListColour_fruit = new List<Color>();
    public static bool phase3 = false; 
    int randomImageCanvas;
    int randomImagePortal1;
    int randomImagePortal2;
    int randomImagePortal3;
    private int indexColourCanvas;
    private Color visibleColor;
    public LoadFruits.Fruit currentFruit;
    public LoadFruits.Fruit currentMiniFruit;
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
        if (phase3)
        {
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            selectRandomImage();
            CustomPalette();
            //selectRandomColour();
            selectFruitColor();
            selectMiniImage();
        }
        
    }
    
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState state)
    {
        if (state == MiniGameState.Three)
        {
            fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
            selectRandomImage();
            CustomPalette();
            //selectRandomColour();
            selectFruitColor();
            selectMiniImage();
            phase3 = true;
        }
        else
        {
            phase3 = false;
        }
    }
    void selectRandomImage()
    {
        randomImageCanvas = Random2.Range(0, 8);
        fruitImage.sprite = fruitImages[randomImageCanvas];
        currentFruit = LoadFruits.myFruitList.fruit[randomImageCanvas];

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
    void selectFruitColor()
    {
        List<Color> tempColourList_fruit = ListColour_fruit;
        //tempColourList_fruit.RemoveAt(currentFruit.ID);
        indexColourCanvas = Random2.Range(0, 4);
        while (indexColourCanvas == currentFruit.ID)
        {
                    indexColourCanvas = Random2.Range(0, 4);
        }
        visibleColor = tempColourList_fruit[indexColourCanvas];
        fruitImage.GetComponent<Image>().color = visibleColor;

    }
    void selectMiniImage()
    {
        randomImagePortal1 = Random2.Range(0, 8);
        randomImagePortal2 = Random2.Range(0, 8);
        randomImagePortal3 = Random2.Range(0, 8);
        while ((randomImagePortal1 == randomImageCanvas) || 
               (indexColourCanvas != LoadFruits.myFruitList.fruit[randomImagePortal1].ID ) )
        {
            randomImagePortal1 = Random2.Range(0, 8);
        }
        
        while ((randomImagePortal2 == randomImagePortal1) || 
               (randomImagePortal2 == randomImageCanvas) || 
               (indexColourCanvas == LoadFruits.myFruitList.fruit[randomImagePortal2].ID ) )
        {
            randomImagePortal2 = Random2.Range(0, 8);
        }
        
        while ((randomImagePortal3 == randomImagePortal1) || 
               (randomImagePortal3 == randomImagePortal2) || 
               (randomImagePortal3 == randomImageCanvas) || 
               (indexColourCanvas == LoadFruits.myFruitList.fruit[randomImagePortal3].ID ))
        {
            randomImagePortal3 = Random2.Range(0, 8);
        }

        List<int> miniFruitVector = new List<int>();
        miniFruitVector.Add(randomImagePortal1);
        miniFruitVector.Add(randomImagePortal2);
        miniFruitVector.Add(randomImagePortal3);
        var rnd = new Random1();
        miniFruitVector = miniFruitVector.OrderBy(item => rnd.Next()).ToList();
        
        MiniFruitRender1.sprite= fruitImages[miniFruitVector[0]];
        MiniFruitRender2.sprite= fruitImages[miniFruitVector[1]];
        MiniFruitRender3.sprite= fruitImages[miniFruitVector[2]];
        //currentMiniFruit = LoadFruits.myFruitList.fruit[randomImagePortal1];
        // print(" Portale1: "+ LoadFruits.myFruitList.fruit[randomImagePortal1].name);
        // print(" Portale2: "+ LoadFruits.myFruitList.fruit[randomImagePortal2].name);
        // print(" Portale3: "+ LoadFruits.myFruitList.fruit[randomImagePortal3].name);
        
    }
}
