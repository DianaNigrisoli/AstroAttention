using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using Assets.ScriptsDirectionGame;
using UnityEngine;

public class phase0Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool touch = true;
    public GameObject shootingStar;
    public static int cases = 1000;
    public static bool phaseZero = true;
    private bool endgame = false;
    private int count = 0;
    void Start()
    {
        shootingStar = GameObject.Find("notShootingStar");
    }

    // Update is called once per frame
    void shootingStarSpawn()
    {
        cases  = Random.Range(0, 4);
        Debug.Log(cases);
        Debug.Log("Yeah");
        Vector3 spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
        switch (cases)
        {
            case 0:
            {
                spawnPosition = new Vector3(-2.25f, 4.7f, 16.4f);
                break;
            }
            case 1:
            {
                spawnPosition = new Vector3(-2.25f, 0.45f, 16.4f);
                break;
            }
            case 2:
            {
                spawnPosition = new Vector3(2.25f, 4.7f, 16.4f);
                break;
            }
            case 3:
            {
                spawnPosition = new Vector3(2.25f, 0.45f, 16.4f);
                break;
            }
            default:
            {
                spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            }
        }

        shootingStar.transform.position = spawnPosition;
        laserButtons.enabled = true;
    }

    void gameInstance()
    {
        shootingStarSpawn();
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        gameInstance();
    }
    
    void Update()
    {
        if (!endgame)
        {
            if (touch)
            {
                StartCoroutine(Delay());
                touch = false;
                count = count + 1;
                if (count == 10)
                {
                    endgame = true;
                }
            }
        }
        else
        {
            MiniGameManager.instance.UpdateMiniGameState(MiniGameState.WaitForNext); //TODO: go to WaitForNext
            Destroy(shootingStar);
            cases = 1000;
            phaseZero = false;
            Destroy(this);
        }
    }
}
