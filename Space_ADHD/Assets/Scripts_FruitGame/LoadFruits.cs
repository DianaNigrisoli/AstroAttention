using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public class LoadFruits : MonoBehaviour
{
    public TextAsset testAssetData;
    
    public class Fruit
    {
        public string name;
        public string colour;
        public float R; 
        public float G;
        public float B;
        public float A;
        public string hex; 
    }

    public class FruitList
    {
        public Fruit[] fruit;
    }

    public FruitList myFruitList = new FruitList(); 
    
    void Start()
    {
        readCSV();
    }
    
   
    // Start is called before the first frame update
    void readCSV()
    {
        int n_col = 7; 
        string[] data = testAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / n_col - 1;
        myFruitList.fruit = new Fruit[tableSize]; 
        

        // TextAsset fruitdata = Resources.Load<TextAsset>("Fruits");
        // string[] data = fruitdata.text.Split(new char[] { '\n' });
        //
        for (int i = 0; i < tableSize; i++)
        {
            myFruitList.fruit[i] = new Fruit();
            
            myFruitList.fruit[i].name = data(n_col*(1+i));
            myFruitList.fruit[i].colour = data(n_col*(1+i)+1);
            myFruitList.fruit[i].R= float.Parse(data(n_col*(1+i)+1));
            myFruitList.fruit[i].G= float.Parse(data(n_col*(1+i)+1));
            myFruitList.fruit[i].B= float.Parse(data(n_col*(1+i)+1));
            myFruitList.fruit[i].A= float.Parse(data(n_col*(1+i)+1));
            myFruitList.fruit[i].hex = data(n_col*(1+i)+1);
            
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
