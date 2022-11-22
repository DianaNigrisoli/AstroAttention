using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phase1Manager : MonoBehaviour
{
    public static bool touch = true;
    public GameObject shootingStar;
    public static int cases;
    public static bool phaseOne = false;
    // Start is called before the first frame update
    void Start()
    {
        shootingStar = GameObject.Find("ShootingStar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
