using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingStarEffect : MonoBehaviour
{
    public GameObject shootingStar;
    public Vector3 targetPosition = new Vector3(0, 0, 0);   
    // Start is called before the first frame update
    private float speed = 0.2f;
    void Start()
    {
        shootingStar = GameObject.Find("shootingStar");
    }
    // Update is called once per frame
    void Update()
    {
        var step = speed;
        //public GameObject shootingStar;

        if (Time.frameCount % 1000 == 0)
        {
            //shootingStar.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log(Time.timeAsDouble);
            float spawnY = Random.Range(-6.0f, 6.0f);
            float targetY = Random.Range(-30.0f, 30.0f);
            float spawnX = Random.Range(-4.0f, 4.0f);
            float targetX = Random.Range(-20.0f, 20.0f);
            float spawnZ = Random.Range(0.0f, 2.0f);
            float targetZ = Random.Range(0.0f, 2.0f);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);
            targetPosition = new Vector3(targetX, targetY, targetZ);

            shootingStar.transform.position = spawnPosition;
            Debug.Log(spawnPosition);
            Debug.Log(targetPosition);
        }
        shootingStar.transform.position = Vector3.MoveTowards(shootingStar.transform.position, targetPosition, step);
        //shootingStar.transform.position = targetPosition;
    }
}