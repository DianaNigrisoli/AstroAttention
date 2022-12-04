using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts_A_General;
using Assets.ScriptsDirectionGame;
using TMPro;

public class phase2Manager : MonoBehaviour
{
    public static bool touch = true;
    public GameObject shootingStar;
    public GameObject canvas;
    public static int ROTcases = 1000;
    public static int SPTcases = 1000;
    public static bool phaseTwo = true;
    private bool endgame = false;
    private int count = 0;
    public TextMeshProUGUI textObject;
    private GameObject hlines;
    private GameObject vlines;
    
    private NebulaBehaviour nebulaBehaviour;
    [SerializeField] private GameObject shootingStarExplosionPrefab;
    private GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        shootingStar = GameObject.Find("smallShootingStar");
        canvas = GameObject.Find("CanvasIntro");
        textObject = canvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        hlines = GameObject.Find("HorizontalLines");
        vlines = GameObject.Find("VerticalLines");
        hlines.transform.position =
            new Vector3(hlines.transform.position.x, hlines.transform.position.y, 0.0f);
        vlines.transform.position =
            new Vector3(vlines.transform.position.x, vlines.transform.position.y, 0.0f);
        
        nebulaBehaviour = GameObject.Find("Nebula Aqua-Pink").GetComponent<NebulaBehaviour>();
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
        shootingStar.transform.rotation=Quaternion.Euler(new Vector3(0, 0, rotZ));
        laserButtons.enabled = true;
    }
    
    void gameInstance()
    {
        shootingStarSpawn();
        Destroy(explosion);
        laserButtons.timeDG=0.0f;
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.23f);
        gameInstance();
    }
    
    IEnumerator HitShootingStar()
    {
        yield return new WaitForSeconds(0.5f);
        var spawnPosition = shootingStar.transform.position;
        explosion = Instantiate(shootingStarExplosionPrefab, spawnPosition, Quaternion.identity, GameObject.Find("All").transform);
        StartCoroutine(nebulaBehaviour.Shake(0.7f, 0.3f));
        shootingStar.transform.position += Vector3.right * 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        laserButtons.timeDG += Time.deltaTime;
        if (!endgame)
        {
            if (touch)
            {
                if (count > 0)
                {
                    StartCoroutine(HitShootingStar());
                }
                
                StartCoroutine(Delay());
                touch = false;
                count = count + 1;
                if (count == 11)
                {
                    endgame = true;
                }
            }
        }
        else
        {
            MiniGameManager.instance.UpdateMiniGameState(MiniGameState.WaitForNext);
            textObject.text = "Select the sector with the comet";
            ROTcases = 1000;
            SPTcases = 1000;
            phaseTwo = false;
            Destroy(this);
        }
    }
}
