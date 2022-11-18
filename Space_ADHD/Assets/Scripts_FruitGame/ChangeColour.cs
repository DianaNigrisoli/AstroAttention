using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(waiter());

    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(1);
        var thisFruit = RandomFruit.currentFruit.name;
        print(thisFruit);
    }
}
