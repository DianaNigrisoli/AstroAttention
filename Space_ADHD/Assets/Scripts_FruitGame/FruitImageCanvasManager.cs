using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;
using Random1 = System.Random;
using Random2 = UnityEngine.Random;

public class FruitImageCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject FruitCanvas;
    [SerializeField] private GameObject IndicatorCanvas;
    [SerializeField] private GameObject TextCanvas;
    private GameObject triangle1;
    
    void Awake()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
        
    }
    void OnDestroy()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
    } 
    // Start is called before the first frame update
    
    
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
    {
        if (state == MiniGameStateFruit.WaitForNext || state == MiniGameStateFruit.Intro || state == MiniGameStateFruit.Instructions)
        {
            FruitCanvas.SetActive(false);
            IndicatorCanvas.SetActive(false);
            TextCanvas.SetActive(false);
        }
        else
        {
            FruitCanvas.SetActive(true);
            IndicatorCanvas.SetActive(true);
            TextCanvas.SetActive(true);
        }
        
        // To have the CanvasIndicators not active at the beginning of each phase
        if (state == MiniGameStateFruit.ZeroScene || state == MiniGameStateFruit.OneScene ||
            state == MiniGameStateFruit.TwoScene || state == MiniGameStateFruit.ThreeScene || 
            state == MiniGameStateFruit.ZeroTutorial || state == MiniGameStateFruit.OneTutorial ||
            state == MiniGameStateFruit.TwoTutorial || state == MiniGameStateFruit.ThreeTutorial)
        {
            IndicatorCanvas.SetActive(false);
        }
        
    }
    
}
