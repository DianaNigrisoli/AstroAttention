using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonAction : MonoBehaviour
{
    public void buttonMethod(){
        Debug.Log("Done SIUIIIII");
        SceneManager.LoadScene("Scenes/SampleScene");
    }
}
