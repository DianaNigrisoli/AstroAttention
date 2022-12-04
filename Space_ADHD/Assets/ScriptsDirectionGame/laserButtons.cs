using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts_A_General;
using System.Collections.Generic;

namespace Assets.ScriptsDirectionGame
{
    public class laserButtons : MonoBehaviour
    {
        public GameObject button;
        private bool selected; //TODO: do we need this?
        private AudioSource audioData;
        public GameObject buzzButton;
        private MiniGameState miniGameState; //TODO: useless since I added tutorialPhase... But can be more clear to
                                             //add that we are in the intro phase in my opinion. Davi if you agree
                                             //delete this comment;
        private TutorialPhase tutorialPhase;
        public static Boolean enabled;
        public static float FinalScore;
		public static double reactionTime=5.0d;
		public static double[] MAreactionTime = new double[] {0, 0, 1, 0, 0};
		public static float timeDG;
		public static int errorDirectionG = 0;
        
        void Awake()
        {
            MiniGameManager.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
            IntroManager.OnTutorialPhaseChanged += IntroManagerOnOnTutorialPhaseChanged;
        }

        void OnDestroy()
        {
            MiniGameManager.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
            IntroManager.OnTutorialPhaseChanged += IntroManagerOnOnTutorialPhaseChanged;
        }
        
        private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState newState)
        {
            miniGameState = newState;
        }
        
        private void IntroManagerOnOnTutorialPhaseChanged(TutorialPhase newPhase)
        {
            tutorialPhase = newPhase;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            button = this.gameObject;
            this.selected = false;
            enabled = false;
            audioData = button.GetComponent<AudioSource>();
        }
        
		double[] computeMovingAverage(double newValue, double oldValue, double count, double s1, double s2)
		{
			double[] output = new double[5]; //[s1, s2, count, mean, std]
			output[0] = s1 + newValue;
			output[1] = s2 + (newValue*newValue);
			output[3] = ((oldValue * (count-1))+ newValue) / count;
			output[4] = Math.Sqrt(((count*output[1]) - (output[0]*output[0]))/(count * (count-1)));
			output[2] = count + 1;
			return output;
		}

        // Update is called once per frame
        void Update()
        {
            if (!enabled) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Input.GetMouseButtonDown(0) && !selected){
                if(Physics.Raycast(ray,out hit) && hit.collider.gameObject == gameObject)
                {
                    switch(this.name)
                    {
                        case "UpperRightButtonQuad":
                            if (phase0Manager.cases == 2 || phase1Manager.cases == 1 || (phase2Manager.ROTcases == 1) ||
                                (phase3Manager.SPTcases == 2) || (miniGameState == MiniGameState.Intro && tutorialPhase == TutorialPhase.Seven))
                            {
								if (tutorialPhase == TutorialPhase.Seven){reactionTime = 5.0d;}
								else
								{
									reactionTime = (double)timeDG;
									MAreactionTime = computeMovingAverage(timeDG, MAreactionTime[3], MAreactionTime[2], MAreactionTime[0], MAreactionTime[1]);
								}
								if (reactionTime>5){reactionTime=5;}
                                CannonBehavior.upRightShot = true;
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
								errorDirectionG += 1;
                            }
                            break;
                        case "UpperLeftButtonQuad":
                            if (phase0Manager.cases==0 || phase1Manager.cases == 3 || (phase2Manager.ROTcases == 3) || (phase3Manager.SPTcases == 0) || (miniGameState == MiniGameState.Intro && tutorialPhase == TutorialPhase.Five))
                            {
								if (tutorialPhase == TutorialPhase.Five){reactionTime = 5.0d;}
								else
								{
									reactionTime = (double)timeDG;
									MAreactionTime = computeMovingAverage(timeDG, MAreactionTime[3], MAreactionTime[2], MAreactionTime[0], MAreactionTime[1]);
								}						
								if (reactionTime>5){reactionTime=5;}
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
								errorDirectionG += 1;
                            }
                            break;
                        case "LowerRightButtonQuad":
                            if (phase0Manager.cases == 3 || phase1Manager.cases == 0 || (phase2Manager.ROTcases == 0) || (phase3Manager.SPTcases == 3))
                            {
								reactionTime = (double)timeDG;
								MAreactionTime = computeMovingAverage(timeDG, MAreactionTime[3], MAreactionTime[2], MAreactionTime[0], MAreactionTime[1]);
								if (reactionTime>5){reactionTime=5;}
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
								errorDirectionG += 1;
                            }
                            break;
                        case "LowerLeftButtonQuad":
                            if (phase0Manager.cases == 1 || phase1Manager.cases == 2 || (phase2Manager.ROTcases == 2) || (phase3Manager.SPTcases == 1))
                            {
								reactionTime = (double)timeDG;
								MAreactionTime = computeMovingAverage(timeDG, MAreactionTime[3], MAreactionTime[2], MAreactionTime[0], MAreactionTime[1]);
								if (reactionTime>5){reactionTime=5;}
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
								errorDirectionG += 1;
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
