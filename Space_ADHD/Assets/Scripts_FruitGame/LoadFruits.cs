using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using System.ComponentModel;
using UnityEngine.UI;

public class LoadFruits : MonoBehaviour
{
    public TextAsset testAssetData;
  
    [System.Serializable]
    public class Fruit
    {
        public string name;
        public string colour;
        public float R; 
        public float G;
        public float B;
        public float A;
        public string hex; 
        public Sprite icon; 
    }
    [System.Serializable]
    public class FruitList
    {
        public Fruit[] fruit;
    }

    public FruitList myFruitList = new FruitList(); 
    
    void Start()
    {
        readCSV();
        //Debug.Log(myFruitList.fruit[0].name);
    }
    
    // Start is called before the first frame update
    void readCSV()
    {
        int n_col = 7; 
        string[] data = testAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / n_col - 1;
        myFruitList.fruit = new Fruit[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            myFruitList.fruit[i] = new Fruit();
            
            myFruitList.fruit[i].name = data[n_col*(i+1)];
            myFruitList.fruit[i].colour = data[n_col*(i+1)+1];
            myFruitList.fruit[i].R = float.Parse(data[n_col*(i+1)+2]);
            myFruitList.fruit[i].G = float.Parse(data[n_col*(i+1)+3]);
            myFruitList.fruit[i].B = float.Parse(data[n_col*(i+1)+4]);
            myFruitList.fruit[i].A = float.Parse(data[n_col*(i+1)+5]);
            myFruitList.fruit[i].hex = data[n_col*(i+1)+6];
            // myFruitList.fruit[1].icon = Resources.Load("Fruits/Banana.png") as Sprite;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
