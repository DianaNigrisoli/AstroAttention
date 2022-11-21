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

    // Start is called before the first frame update
    void Start()
    {
        fruitImage = GameObject.Find("FruitImage").GetComponent<Image>();
        selectRandomImage();
        CustomPalette();
        selectRandomColour();
        //Timer_try();
        //functionTimer= new FunctionTimer(TestingAction, 3f);

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

    private void TestingAction()
    {
        Debug.Log("Testing");
    }
    
    // Funzione per calcolo tempo
    // 1. creare Timer(durata minore di uno spostamento)
    // 2. capire posizione in quell'istante e fare check con posizione del timer precendente(si utilizzano 2 variabili)
    // creare funzione evento: quando finisce il timer 
    // nello start salvare poszione attuale 
    //public void Timer_try()
    //{
      //  System.Timers.Timer aTimer = new System.Timers.Timer();
      //   aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        //aTimer.Interval = 1000;
        //aTimer.Enabled = true;
        //aTimer.Stop();
        
        
        
        //Console.WriteLine("Press \'q\' to quit the sample.");
        // while(Console.Read() != 'q');
   // }

    // Specify what you want to happen when the Elapsed event is raised.
    //private void OnTimedEvent(object source, ElapsedEventArgs e)
    //{
        //Console.WriteLine("Hello World!");
       // print("hello NOe");
       
        
    //}
}

