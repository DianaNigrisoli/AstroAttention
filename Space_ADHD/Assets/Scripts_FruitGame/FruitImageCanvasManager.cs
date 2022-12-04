using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts_A_General;
using Random1 = System.Random;
using Random2 = UnityEngine.Random;

public class FruitImageCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject FruitCanvas;
    [SerializeField] private GameObject IndicatorCanvas;
    void Awake()
    {
        MiniGameManager.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
    }
    void OnDestroy()
    {
        MiniGameManager.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
    } 
    // Start is called before the first frame update
    
    
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState state)
    {
        if (state == MiniGameState.WaitForNext || state == MiniGameState.Intro)
        {
            FruitCanvas.SetActive(false);
            IndicatorCanvas.SetActive(false);
        }
        else
        {
            FruitCanvas.SetActive(true);
            IndicatorCanvas.SetActive(true);
        }
        
        // To have the CanvasIndicators not active at the beginning of each phase
        if (state == MiniGameState.Zero || state == MiniGameState.One ||
            state == MiniGameState.Two || state == MiniGameState.Three)
        {
            IndicatorCanvas.SetActive(false);
        }
        
    }
    
}
