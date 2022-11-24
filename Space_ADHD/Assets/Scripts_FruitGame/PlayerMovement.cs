using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
        private Rigidbody controller;
    public float increment;
    public float speedForward;
    public float speedLateral;
    public Vector3 targetPos;
    //public int score;
    
    void Awake()
    {
        increment = 3;
        speedForward = 10;
        speedLateral = 5;
        targetPos = transform.position;
    }

    private void Start()
    {
        controller = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        controller.velocity = transform.forward * speedForward;
        targetPos.z = controller.position.z;
        var step = speedLateral * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

    }
    
    public void MoveRight()
    {   FunctionTimer.leftLine = false;
        if (targetPos.x < increment)
        {
            targetPos += new Vector3(increment, 0, 0);
            
        }
        else
        {
            FunctionTimer.rightLine = true;
        }
        if (targetPos.x == 0)
        {
            FunctionTimer.leftLine = false;
            FunctionTimer.rightLine = false;
        }
    }
    public void MoveLeft()
    {
        FunctionTimer.rightLine = false;
        if (targetPos.x > -increment)
        {
            targetPos -= new Vector3(increment, 0, 0);
        }
        else
        {
            FunctionTimer.leftLine = true;
        }
        if (targetPos.x == 0)
        {
            FunctionTimer.leftLine = false;
            FunctionTimer.rightLine = false;
        }
    }
    
    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.collider.CompareTag("Pumpkin"))
    //     {
    //         score++;
    //         AudioManager.instance.PlayCorrect();
    //         Destroy(other.gameObject);
    //     }
    //     else if (other.collider.CompareTag("FinishLine"))
    //     {
    //         Win();
    //     }
    // }

    // private void Win()
    // {
    //     GameManager.instance.score = this.score;
    //     SceneManager.LoadScene("FinalScene");
    // }
}
