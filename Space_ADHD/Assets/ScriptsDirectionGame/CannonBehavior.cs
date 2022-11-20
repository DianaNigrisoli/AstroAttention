using System;
using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public GameObject m_shotPrefab;
	public static Boolean upRightShot;
	public static Boolean upLeftShot;
	public static Boolean downRightShot;
	public static Boolean downLeftShot;

	void Start()
	{
		upRightShot = false;
		upLeftShot = false;
		downLeftShot = false;
		downRightShot = false;
	}

	// Update is called once per frame
	void Update () 
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

	private void Shoot(Vector3 direction)
	{
		GameObject go = GameObject.Instantiate(m_shotPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		go.GetComponent<Rigidbody>().velocity = direction;
		GameObject.Destroy(go, 3f);
	}
}
