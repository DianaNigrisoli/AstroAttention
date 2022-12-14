using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingStarEffect : MonoBehaviour
{
    public GameObject bkgShootingStar;
    public Vector3 targetPosition = new Vector3(-10000, -10000, -10000);   
    // Start is called before the first frame update
    private float speed = 0.02f;
    void Start()
    {
        bkgShootingStar = GameObject.Find("shootingStar");
        targetPosition = new Vector3(-10000, -10000, -10000);
    }
    // Update is called once per frame
    void Update()
    {
        var step = speed;
        //public GameObject shootingStar;

        if (Time.frameCount % 500 == 0)
        {
            //shootingStar.GetComponent<Rigidbody>().velocity = Vector3.zero;
            float spawnY = Random.Range(-7.5f, 9.5f) * (Random.Range(0, 2) * 2 - 1);
            float targetY = - spawnY + Random.Range(-7.0f, 7.0f);
            float spawnX = Random.Range(-5.0f, 7.0f) * (Random.Range(0, 2) * 2 - 1);
            float targetX = - spawnX + Random.Range(-7.0f, 7.0f);
            float spawnZ = Random.Range(0.0f, 2.0f);
            float targetZ = Random.Range(-30.0f, -3.0f);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);
            targetPosition = new Vector3(targetX, targetY, targetZ);
            bkgShootingStar.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bkgShootingStar.transform.position = spawnPosition;
        }
        bkgShootingStar.transform.position = Vector3.MoveTowards(bkgShootingStar.transform.position, targetPosition, step);
        //shootingStar.transform.position = targetPosition;
    }
}