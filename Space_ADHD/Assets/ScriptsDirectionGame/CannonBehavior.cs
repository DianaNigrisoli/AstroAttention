using System;
using UnityEngine;
using System.Collections;
using Assets.ScriptsDirectionGame;

public class CannonBehavior : MonoBehaviour {

	public GameObject m_shotPrefab;
	public static Boolean upRightShot;
	public static Boolean upLeftShot;
	public static Boolean downRightShot;
	public static Boolean downLeftShot;
	public GameObject target;
    public GameObject shootingStar;

	void Start()
	{
		upRightShot = false;
		upLeftShot = false;
		downLeftShot = false;
		downRightShot = false;
		
		shootingStar = GameObject.Find("smallShootingStar");
	}

	// Update is called once per frame
	void Update () 
	{
		if (phase0Manager.phaseZero || phase1Manager.phaseOne)
		{		
			if (upRightShot)
			{
				Shoot(Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.right * 0.7f);
				upRightShot = false;
			}
			if (upLeftShot)
			{
				Shoot(Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.left * 0.7f);
				upLeftShot = false;
			}
			if (downLeftShot)
			{
				Shoot(Vector3.forward * 5f + Vector3.down * 0.5f + Vector3.left * 0.7f);
				downLeftShot = false;
			}
			if (downRightShot)
			{
				Shoot(Vector3.forward * 5f + Vector3.down * 0.5f + Vector3.right * 0.7f);
				downRightShot = false;
			}
		}
		else if (phase2Manager.phaseTwo || phase3Manager.phaseThree)
		{
			if (shootingStar.transform. position == new Vector3(2.25f, 4.7f, 16.4f) && (upRightShot || upLeftShot || downRightShot || downLeftShot))
			{
				Shoot(Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.right * 0.7f);
				upRightShot = false;
				upLeftShot = false;
				downLeftShot = false;
				downRightShot = false;
			}
			if (shootingStar.transform. position == new Vector3(-2.25f, 4.7f, 16.4f) && (upRightShot || upLeftShot || downRightShot || downLeftShot))
			{
				Shoot(Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.left * 0.7f);
				upRightShot = false;
				upLeftShot = false;
				downLeftShot = false;
				downRightShot = false;
			}
			if (shootingStar.transform. position == new Vector3(-2.25f, 0.45f, 16.4f) && (upRightShot || upLeftShot || downRightShot || downLeftShot))
			{
				Shoot(Vector3.forward * 5f + Vector3.down * 0.5f + Vector3.left * 0.7f);
				upRightShot = false;
				upLeftShot = false;
				downLeftShot = false;
				downRightShot = false;
			}
			if (shootingStar.transform. position == new Vector3(2.25f, 0.45f, 16.4f) && (upRightShot || upLeftShot || downRightShot || downLeftShot))
			{
				Shoot(Vector3.forward * 5f + Vector3.down * 0.5f + Vector3.right * 0.7f);
				upRightShot = false;
				upLeftShot = false;
				downLeftShot = false;
				downRightShot = false;
			}
		}
	}

	private void Shoot(Vector3 direction)
	{
		laserButtons.enabled = false;
		GameObject go = GameObject.Instantiate(m_shotPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		go.GetComponent<Rigidbody>().velocity = direction * 50.0f;
		GameObject.Destroy(go, 1.5f);
	}
}
