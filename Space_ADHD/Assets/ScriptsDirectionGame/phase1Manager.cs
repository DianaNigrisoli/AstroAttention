using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts_A_General;

public class phase1Manager : MonoBehaviour
{
    public static bool touch = true;
    public GameObject shootingStar;
    public static int cases = 1000;
    public static bool phaseOne = true;
    private bool endgame = false;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        shootingStar = GameObject.Find("shootingStar");
    }
    
    void shootingStarSpawn()
    {
        cases  = Random.Range(0, 4);
        Vector3 spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
        int rotZ = 0;
        switch (cases)
        {
            case 0:
            {
				spawnPosition = new Vector3(2.25f, 0.45f, 16.4f);
                rotZ = 45;
                break;
            }
            case 1:
            {
				spawnPosition = new Vector3(2.25f, 4.7f, 16.4f);
                rotZ = 135;
                break;
            }
            case 2:
            {
				spawnPosition = new Vector3(-2.25f, 0.45f, 16.4f);
                rotZ = -45;
                break;
            }
            case 3:
            {
				spawnPosition = new Vector3(-2.25f, 4.7f, 16.4f);
                rotZ = -135;
                break;
            }
            default:
            {
				spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
                rotZ = 0;
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
			Destroy(shootingStar);
            cases = 1000;
            Destroy(this);
        }
    }
}