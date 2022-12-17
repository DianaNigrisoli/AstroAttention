using System.Collections;
using Assets.Scripts_A_General;
using UnityEngine;

namespace Assets.ScriptsDirectionGame
{
    public class phase1Manager : MonoBehaviour
    {
        public static bool touch = true;
        public GameObject shootingStar;
        public static int cases = 1000;
        public static bool phaseOne = true;
        private bool endgame = false;
        private int count = 0;
        private GameObject hlines;
        private GameObject vlines;
    
        private NebulaBehaviour nebulaBehaviour;
        [SerializeField] private GameObject shootingStarExplosionPrefab;
        private GameObject explosion;

        // Start is called before the first frame update
        void Start()
        {
            touch = true;
            cases = 1000;
            phaseOne = true;
            endgame = false;
            count = 0;
            shootingStar = GameObject.Find("shootingStar");
            hlines = GameObject.Find("HorizontalLines");
            vlines = GameObject.Find("VerticalLines");
            hlines.transform.position =
                new Vector3(hlines.transform.position.x, hlines.transform.position.y, -1000.0f);
            vlines.transform.position =
                new Vector3(vlines.transform.position.x, vlines.transform.position.y, -1000.0f);
            nebulaBehaviour = GameObject.Find("Nebula Aqua-Pink").GetComponent<NebulaBehaviour>();
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
            laserButtons.enabled = true;
            shootingStar.transform.rotation=Quaternion.Euler(new Vector3(0, 0, rotZ));
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

        IEnumerator seconds()
        {
            yield return new WaitForSeconds(1.22f);
            endgame = true;
        }

        // Update is called once per frame
    
        void Update()
        {
            laserButtons.timeDG += Time.deltaTime;
            if (!endgame)
            {
                if (touch)
                {
                    if (count < 11 && count>0)
                    {
                        StartCoroutine(HitShootingStar());
                    }
                
                    StartCoroutine(Delay());
                    touch = false;
                    count = count + 1;
                    if (count == 11)
                    {
                        StartCoroutine(seconds());
                    }
                }
            }
            else
            {
                Destroy(shootingStar);
                cases = 1000;
                phaseOne = false;
                Destroy(this);
                MiniGameManager.instance.UpdateMiniGameState(MiniGameState.WaitForNext); //TODO: go to WaitForNext
            }
        }
    }
}