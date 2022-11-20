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
			GameObject go = GameObject.Instantiate(m_shotPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			go.GetComponent<Rigidbody>().velocity = Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.right * 0.7f;
			GameObject.Destroy(go, 3f);
			upRightShot = false;
		}
		if (upLeftShot)
		{
			GameObject go = GameObject.Instantiate(m_shotPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			go.GetComponent<Rigidbody>().velocity = Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.left * 0.7f;
			GameObject.Destroy(go, 3f);
			upLeftShot = false;
		}
		if (downLeftShot)
		{
			GameObject go = GameObject.Instantiate(m_shotPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			go.GetComponent<Rigidbody>().velocity = Vector3.forward * 5f + Vector3.down * 0.5f + Vector3.left * 0.7f;
			GameObject.Destroy(go, 3f);
			downLeftShot = false;
		}
		if (downRightShot)
		{
			GameObject go = GameObject.Instantiate(m_shotPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			go.GetComponent<Rigidbody>().velocity = Vector3.forward * 5f + Vector3.down * 0.5f + Vector3.right * 0.7f;
			GameObject.Destroy(go, 3f);
			downRightShot = false;
		}
	}
}
