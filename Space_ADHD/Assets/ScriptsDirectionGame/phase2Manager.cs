using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts_A_General;

public class phase2Manager : MonoBehaviour
{
    public static bool touch = true;
    public GameObject shootingStar;
    public static int ROTcases = 1000;
    public static int SPTcases = 1000;
    public static bool phaseTwo = true;
    private bool endgame = false;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        shootingStar = GameObject.Find("smallShootingStar");
    }
    void shootingStarSpawn()
    {
        ROTcases = Random.Range(0, 4);
        SPTcases = Random.Range(0, 4);
        Vector3 spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
        int rotZ = 0;
        switch (ROTcases)
        {
            case 0:
            {
                rotZ = 45;
                break;
            }
            case 1:
            {
                rotZ = 135;
                break;
            }
            case 2:
            {
                rotZ = -45;
                break;
            }
            case 3:
            {
                rotZ = -135;
                break;
            }
            default:
            {
                rotZ = 0;
                break;
            }
        }
        switch (SPTcases)
        {
            case 0:
            {
                spawnPosition = new Vector3(-2.75f, 5.3f, 16.4f);
                break;
            }
            case 1:
            {
                spawnPosition = new Vector3(-2.75f, -0.05f, 16.4f);
                break;
            }
            case 2:
            {
                spawnPosition = new Vector3(2.75f, 5.3f, 16.4f);
                break;
            }
            case 3:
            {
                spawnPosition = new Vector3(2.75f, -0.05f, 16.4f);
                break;
            }
            default:
            {
                spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            }
        }
        shootingStar.transform.position = spawnPosition;
        shootingStar.transform.rotation=Quaternion.Euler(new Vector3(0, 0, rotZ));
    }
    
    void gameInstance()
    {
        shootingStarSpawn();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!endgame)
        {
            if (touch)
            {
                gameInstance();
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
            ROTcases = 1000;
            SPTcases = 1000;
            phaseTwo = false;
            Destroy(this);
        }
    }
}