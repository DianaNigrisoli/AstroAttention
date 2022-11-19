using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public GameObject m_shotPrefab;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			GameObject go = GameObject.Instantiate(m_shotPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			go.GetComponent<Rigidbody>().velocity = Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.right * 0.7f;
			GameObject.Destroy(go, 3f);
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			GameObject go = GameObject.Instantiate(m_shotPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			go.GetComponent<Rigidbody>().velocity = Vector3.forward * 5f + Vector3.up * 0.99f + Vector3.left * 0.7f;
			GameObject.Destroy(go, 3f);
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			GameObject go = GameObject.Instantiate(m_shotPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			go.GetComponent<Rigidbody>().velocity = Vector3.forward * 5f + Vector3.down * 0.5f + Vector3.left * 0.7f;
			GameObject.Destroy(go, 3f);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			GameObject go = GameObject.Instantiate(m_shotPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			go.GetComponent<Rigidbody>().velocity = Vector3.forward * 5f + Vector3.down * 0.5f + Vector3.right * 0.7f;
			GameObject.Destroy(go, 3f);
		}
	}
}
