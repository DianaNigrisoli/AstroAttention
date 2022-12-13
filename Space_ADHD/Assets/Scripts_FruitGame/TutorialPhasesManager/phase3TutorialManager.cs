using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;
using UnityEngine;
using TMPro;

public class phase3TutorialManager : MonoBehaviour
{
    private int NumScore = 3;
    public TextMeshProUGUI textObject;
    
    private string ing_text = "Choose the fruit that has its real color like the one on top";
    private string ita_text = "Scegli la frutta il cui colore reale è uguale al colore in alto";

  
    void Start()
    {
        textObject = GameObject.Find("TextIndication").GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Language == "ENG") textObject.text = ing_text;
        else textObject.text = ita_text;
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
