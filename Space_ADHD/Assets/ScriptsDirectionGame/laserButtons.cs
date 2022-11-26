using UnityEngine;
using System.Collections;
using Assets.Scripts_A_General;

namespace Assets.ScriptsDirectionGame
{
    public class laserButtons : MonoBehaviour
    {
        public GameObject button;
        private bool selected;
        private AudioSource audioData;
        public GameObject buzzButton;
        private MiniGameState miniGameState;
        
        
        void Awake()
        {
            MiniGameManager.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
        }

        void OnDestroy()
        {
            MiniGameManager.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
        }
        
        private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState newState)
        {
            miniGameState = newState;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            button = this.gameObject;
            this.selected = false;
            audioData = button.GetComponent<AudioSource>();
        }
        
        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Input.GetMouseButtonDown(0) && !selected){
                if(Physics.Raycast(ray,out hit) && hit.collider.gameObject == gameObject)
                {
                    switch(this.name)
                    {
                        case "UpperRightButtonQuad":
                            if (phase0Manager.cases == 2 || phase1Manager.cases == 1 || (phase2Manager.ROTcases == 1) ||
                                (phase3Manager.SPTcases == 2))
                            {
                                CannonBehavior.upRightShot = true;
                                phase0Manager.touch = true;
                                phase1Manager.touch = true;
                                phase2Manager.touch = true;
                                phase3Manager.touch = true;
                            }
                            else
                            {
                                GameObject buzz = GameObject.Instantiate(buzzButton, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
                                GameObject.Destroy(buzz, 1f);
                            }
                            break;
                        case "UpperLeftButtonQuad":
                            if (phase0Manager.cases==0 || phase1Manager.cases == 3 || (phase2Manager.ROTcases == 3) || (phase3Manager.SPTcases == 0) || miniGameState == MiniGameState.Intro)
                            {
                                CannonBehavior.upLeftShot = true;
                                IntroManager.touch = true;
                                phase0Manager.touch = true;
                                phase1Manager.touch = true;
                                phase2Manager.touch = true;
                                phase3Manager.touch = true;
                            }
                            else
                            {
                                GameObject buzz = GameObject.Instantiate(buzzButton, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
                                GameObject.Destroy(buzz, 1f);
                            }
                            break;
                        case "LowerRightButtonQuad":
                            if (phase0Manager.cases == 3 || phase1Manager.cases == 0 || (phase2Manager.ROTcases == 0) || (phase3Manager.SPTcases == 3))
                            {
                                CannonBehavior.downRightShot = true;
                                phase0Manager.touch = true;
                                phase1Manager.touch = true;
                                phase2Manager.touch = true;
                                phase3Manager.touch = true;
                            }
                            else
                            {
                                GameObject buzz = GameObject.Instantiate(buzzButton, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
                                GameObject.Destroy(buzz, 1f);
                            }
                            break;
                        case "LowerLeftButtonQuad":
                            if (phase0Manager.cases == 1 || phase1Manager.cases == 2 || (phase2Manager.ROTcases == 2) || (phase3Manager.SPTcases == 1))
                            {
                                CannonBehavior.downLeftShot = true;
                                phase0Manager.touch = true;
                                phase1Manager.touch = true;
                                phase2Manager.touch = true;
                                phase3Manager.touch = true;
                            }
                            else
                            {
                                GameObject buzz = GameObject.Instantiate(buzzButton, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
                                GameObject.Destroy(buzz, 1f);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
