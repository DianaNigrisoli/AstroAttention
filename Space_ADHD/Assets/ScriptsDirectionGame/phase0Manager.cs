using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using Assets.ScriptsDirectionGame;
using UnityEngine;
using TMPro;

public class phase0Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool touch = true;
    public GameObject shootingStar;
	public GameObject canvas;
    public static int cases = 1000;
    public static bool phaseZero = true;
    private bool endgame = false;
    private int count = 0;
	public TextMeshProUGUI textObject;
    
    private NebulaBehaviour nebulaBehaviour;
    [SerializeField] private GameObject shootingStarExplosionPrefab;
    private GameObject explosion;

    void Start()
    {
        shootingStar = GameObject.Find("notShootingStar");
        canvas = GameObject.Find("CanvasIntro");
		textObject = canvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        nebulaBehaviour = GameObject.Find("Nebula Aqua-Pink").GetComponent<NebulaBehaviour>();
    }

    // Update is called once per frame
    void shootingStarSpawn()
    {
        cases  = Random.Range(0, 4);
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
        shootingStar.transform.position += Vector3.right * 10.0f;
    }
    
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
                count += 1;
                if (count == 11)
                {
                    endgame = true;
                }
            }
        }
        else
        {
			textObject.text = "Select the direction of the comet";
            Destroy(shootingStar);
            cases = 1000;
            phaseZero = false;
            Destroy(this);
            MiniGameManager.instance.UpdateMiniGameState(MiniGameState.WaitForNext);
        }
    }
}
