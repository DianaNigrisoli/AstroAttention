using System;
using UnityEngine;
using System.Collections;
using Assets.ScriptsDirectionGame;
using Assets.Scripts_A_General;
using System.Linq;

namespace Assets.ScriptsDirectionGame
{
    public class CannonBehavior : MonoBehaviour {
    
    	public GameObject m_shotPrefab;
    	public static Boolean upRightShot;
    	public static Boolean upLeftShot;
    	public static Boolean downRightShot;
    	public static Boolean downLeftShot;
    	public GameObject target;
        public GameObject shootingStar;
        public static double kidScoreDirectionG = 0.0d;
		public static double[] reactionTimeMean = new double[4];
		public static double[] reactionTimeStd = new double[4];
		public static int[] errorsDirectionG = new int[4];
        private MiniGameState miniGameState;
    
    	void Start()
    	{
    		upRightShot = false;
    		upLeftShot = false;
    		downLeftShot = false;
    		downRightShot = false;
    		shootingStar = GameObject.Find("smallShootingStar");
    	}
    
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
			switch (miniGameState)
        	{
                case MiniGameState.One:
					reactionTimeMean[0] = laserButtons.reactionTimeMeanStd[3];
					reactionTimeStd[0] = laserButtons.reactionTimeMeanStd[4];
					errorsDirectionG[0] = laserButtons.errorDirectionG;
					laserButtons.reactionTimeMeanStd = new double[] {0, 0, 1, 0, 0};
					laserButtons.errorDirectionG = 0;
					break;
                case MiniGameState.Two:
					reactionTimeMean[1] = laserButtons.reactionTimeMeanStd[3];
					reactionTimeStd[1] = laserButtons.reactionTimeMeanStd[4];
					errorsDirectionG[1] = laserButtons.errorDirectionG;
					laserButtons.reactionTimeMeanStd = new double[] {0, 0, 1, 0, 0};
					laserButtons.errorDirectionG = 0;
					break;
				case MiniGameState.Three:
					reactionTimeMean[2] = laserButtons.reactionTimeMeanStd[3];
					reactionTimeStd[2] = laserButtons.reactionTimeMeanStd[4];
					errorsDirectionG[2] = laserButtons.errorDirectionG;
					laserButtons.reactionTimeMeanStd = new double[] {0, 0, 1, 0, 0};
					laserButtons.errorDirectionG = 0;
                    break;
				case MiniGameState.End:
					reactionTimeMean[3] = laserButtons.reactionTimeMeanStd[3];
					reactionTimeStd[3] = laserButtons.reactionTimeMeanStd[4];
					errorsDirectionG[3] = laserButtons.errorDirectionG;
					laserButtons.reactionTimeMeanStd = new double[] {0, 0, 1, 0, 0};
					laserButtons.errorDirectionG = 0;
                    break;
                default:
                    break;
            }
        }
            
    	// Update is called once per frame
    	void Update ()
    	{
    		if (miniGameState == MiniGameState.Zero || miniGameState == MiniGameState.One || miniGameState == MiniGameState.Intro)
    		{
    			if (upRightShot)
    			{
    				Shoot(Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.right * 0.7f);
    				upRightShot = false;
    				kidScoreDirectionG = kidScoreDirectionG + (1.0f * (5-laserButtons.reactionTime)) + 1;
					Debug.Log(CannonBehavior.kidScoreDirectionG);
    			}
    			if (upLeftShot)
    			{
    				Shoot(Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.left * 0.7f);
    				upLeftShot = false;
    				kidScoreDirectionG = kidScoreDirectionG + (1.0f * (5-laserButtons.reactionTime)) + 1;
					Debug.Log(CannonBehavior.kidScoreDirectionG);
    			}
    			if (downLeftShot)
    			{
    				Shoot(Vector3.forward * 5f + Vector3.down * 0.4f + Vector3.left * 0.7f);
    				downLeftShot = false;
    				kidScoreDirectionG = kidScoreDirectionG + (1.0f * (5-laserButtons.reactionTime)) + 1;
					Debug.Log(CannonBehavior.kidScoreDirectionG);
    			}
    			if (downRightShot)
    			{
    				Shoot(Vector3.forward * 5f + Vector3.down * 0.4f + Vector3.right * 0.7f);
    				downRightShot = false;
    				kidScoreDirectionG = kidScoreDirectionG + (1.0f * (5-laserButtons.reactionTime)) + 1;
					Debug.Log(CannonBehavior.kidScoreDirectionG);
    			}
    		}
    		else if (miniGameState == MiniGameState.Two || miniGameState == MiniGameState.Three)
    		{
    			if (shootingStar.transform. position == new Vector3(2.25f, 4.7f, 16.4f) && (upRightShot || upLeftShot || downRightShot || downLeftShot))
    			{
    				Shoot(Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.right * 0.7f);
    				upRightShot = false;
    				upLeftShot = false;
    				downLeftShot = false;
    				downRightShot = false;
    				kidScoreDirectionG = kidScoreDirectionG + (2.0f * (5-laserButtons.reactionTime)) + 1;
					Debug.Log(CannonBehavior.kidScoreDirectionG);
    			}
    			if (shootingStar.transform. position == new Vector3(-2.25f, 4.7f, 16.4f) && (upRightShot || upLeftShot || downRightShot || downLeftShot))
    			{
    				Shoot(Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.left * 0.7f);
    				upRightShot = false;
    				upLeftShot = false;
    				downLeftShot = false;
    				downRightShot = false;
    				kidScoreDirectionG = kidScoreDirectionG + (2.0f * (5-laserButtons.reactionTime)) + 1;
					Debug.Log(CannonBehavior.kidScoreDirectionG);
    			}
    			if (shootingStar.transform. position == new Vector3(-2.25f, 0.45f, 16.4f) && (upRightShot || upLeftShot || downRightShot || downLeftShot))
    			{
    				Shoot(Vector3.forward * 5f + Vector3.down * 0.5f + Vector3.left * 0.7f);
    				upRightShot = false;
    				upLeftShot = false;
    				downLeftShot = false;
    				downRightShot = false;
    				kidScoreDirectionG = kidScoreDirectionG + (2.0f * (5-laserButtons.reactionTime)) + 1;
					Debug.Log(CannonBehavior.kidScoreDirectionG);
    			}
    			if (shootingStar.transform. position == new Vector3(2.25f, 0.45f, 16.4f) && (upRightShot || upLeftShot || downRightShot || downLeftShot))
    			{
    				Shoot(Vector3.forward * 5f + Vector3.down * 0.5f + Vector3.right * 0.7f);
    				upRightShot = false;
    				upLeftShot = false;
    				downLeftShot = false;
    				downRightShot = false;
    				kidScoreDirectionG = kidScoreDirectionG + (2.0f * (5-laserButtons.reactionTime)) + 1;
					Debug.Log(CannonBehavior.kidScoreDirectionG);
    			}
    		}
    	}
    
    	private void Shoot(Vector3 direction)
    	{
    		laserButtons.enabled = false;
    		GameObject go = GameObject.Instantiate(m_shotPrefab, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find("All").transform) as GameObject;
    		go.GetComponent<Rigidbody>().velocity = direction * 50.0f;
    		GameObject.Destroy(go, 1.5f);
    	}
    }
}