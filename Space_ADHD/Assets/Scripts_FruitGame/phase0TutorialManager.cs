using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;
using UnityEngine;
using TMPro;

public class phase0TutorialManager : MonoBehaviour
{
    private int NumScore = 2;
    public TextMeshProUGUI textObject;
    void Start()
    {
        textObject = GameObject.Find("TextIndication").GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        textObject.text = "Select the real colour";
        if(PlayerMovement.score == NumScore)
        { 
            StartCoroutine(waiter());
        }
    }
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
        PlayerMovement.PortalCounter = 0;
        PlayerMovement.score = 0;
        Destroy(this);
    }
}
