using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePortalColour : MonoBehaviour
{        
    private List<Color> ListColour = new List<Color>();
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
        
    }

    void Update()
    {

    }
    void selectRandomColour()
    {
        var currentIndexColour = RandomFruit.currentFruit.ID;
        Color currentColor = ListColour[currentIndexColour];
        print(currentColor);
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(4);
        ListColour.Add(new Color(LoadFruits.myFruitList.fruit[1].R, LoadFruits.myFruitList.fruit[1].G,
            LoadFruits.myFruitList.fruit[1].B, LoadFruits.myFruitList.fruit[1].A));
        ListColour.Add(new Color(LoadFruits.myFruitList.fruit[0].R, LoadFruits.myFruitList.fruit[0].G,
            LoadFruits.myFruitList.fruit[0].B, LoadFruits.myFruitList.fruit[0].A));
        ListColour.Add(new Color(LoadFruits.myFruitList.fruit[3].R, LoadFruits.myFruitList.fruit[3].G,
            LoadFruits.myFruitList.fruit[3].B, LoadFruits.myFruitList.fruit[3].A));
        ListColour.Add(new Color(LoadFruits.myFruitList.fruit[5].R, LoadFruits.myFruitList.fruit[5].G,
            LoadFruits.myFruitList.fruit[5].B, LoadFruits.myFruitList.fruit[5].A));
        selectRandomColour();
    }

}
