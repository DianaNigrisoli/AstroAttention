using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public Color myColor;
    public float rFloat; 
    public float gFloat;
    public float bFloat;
    public float aFloat;
    // the values go from 0 to 1 

    public Renderer myRenderer; 
    
    void Start()
    {
        aFloat = 1;
        myRenderer = gameObject.GetComponent<Renderer>();
        var fruit = new LoadFruits.Fruit().name;
        Debug.Log(fruit);

    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // if (aFloat < 1)
            // {
            //     aFloat += 0.01f;
            // }
            // else
            // {
            //     aFloat = 0;
            // }
        }
        
        if (Input.GetKey(KeyCode.R))
        {  
                rFloat = 1;
                gFloat = 0;
                bFloat = 0;
            
        }
        
        if (Input.GetKey(KeyCode.G))
        {  
                rFloat = 0;
                gFloat = 1;
                bFloat = 0;
            
        }
        
        if (Input.GetKey(KeyCode.B))
        {   
                rFloat = 0;
                gFloat = 0;
                bFloat = 1;
            
        }

        myColor = new(rFloat, gFloat, bFloat, aFloat);
        myRenderer.material.color = myColor;

    }
}
